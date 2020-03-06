using System;
using System.Linq;
using AeropuertoApp.UseCases.Messages.CreateReservacionVuelo;
using AutoMapper;

namespace AeropuertoApp.UseCases.Interactors
{
    public class CreateReservacionVueloInteractor : Base.IRequestHandler<CreateReservacionVueloRequestMessage, CreateReservacionVueloResponseMessage>
    {
        private readonly Domain.Queries.Base.IQuery<Domain.Cliente> _clienteQuery;
        private readonly Domain.Queries.Base.IQuery<Domain.Vuelo> _vueloQuery;
        private readonly Domain.Repositories.Base.IRepository<Domain.Operacion> _operacionRepository;
        private readonly Domain.Repositories.Base.IRepository<Domain.Pasajero> _pasajeroRepository;
        private readonly IMapper _mapper;

        public CreateReservacionVueloInteractor(EntityFramework.Database.IDataBaseSqlServerEntityFramework database, IMapper mapper)
        {
            this._vueloQuery = new EntityFramework.Queries.QueryBase<Domain.Vuelo>(database);
            this._clienteQuery = new EntityFramework.Queries.QueryBase<Domain.Cliente>(database);
            this._pasajeroRepository = new EntityFramework.Repositories.Base.Repository<Domain.Pasajero>(database);
            this._operacionRepository = new EntityFramework.Repositories.Base.Repository<Domain.Operacion>(database);
            this._mapper = mapper;
        }

        public CreateReservacionVueloResponseMessage Handle(CreateReservacionVueloRequestMessage data)
        {
            // obtener la informacion del vuelo
            _vueloQuery.Init();
            _vueloQuery.AddWhere(q => q.Id == data.VueloId);

            _vueloQuery.IncludeObject(q => q.AeropuertoOrigen);
            _vueloQuery.IncludeObject(q => q.AeropuertoDestino);
            _vueloQuery.IncludeObject(q => q.Avion.Aerolinea);
            _vueloQuery.IncludeObject(q => q.Pasajeros);
            var vuelos = _vueloQuery.Execute();
            var vuelo = vuelos.FirstOrDefault();

            // Obtener la información del cliente
            _clienteQuery.Init();
            _clienteQuery.AddWhere(q => q.Id == data.ClienteId);

            var cliente = _clienteQuery.Execute().FirstOrDefault();
            if(vuelo == null)
            {
                return new CreateReservacionVueloResponseMessage(new Messages.Base.ValidationResult() { IsValid = false, Error = "No hay información del vuelo especificado" });
            }
            if (cliente == null)
            {
                return new CreateReservacionVueloResponseMessage(new Messages.Base.ValidationResult() { IsValid = false, Error = "No hay información del cliente especificado" });
            }
            if(data.Asientos == null || data.Asientos.Count() == 0)
            {
                return new CreateReservacionVueloResponseMessage(new Messages.Base.ValidationResult() { IsValid = false, Error = "No se selecciono ningún asiento" });
            }

            DateTime fechaActual = DateTime.Now;
            int hoursLimitBuy = 3;
            if((vuelo.FechaSalida - fechaActual).Hours <= hoursLimitBuy)
            {
                // verificar que la fecha de salida del vuelo este en el rango de horario de compra
                return new CreateReservacionVueloResponseMessage(new Messages.Base.ValidationResult() { IsValid = false, Error = $"No se puede reservar ya que el vuelo esta proximo a partir. Se necesita reservar en un máximo de {hoursLimitBuy} horas antes de la salida" });
            }

            var operacion = new Domain.Operacion();
            operacion.ClienteId = data.ClienteId;
            operacion.Fecha = DateTime.Now;
            // obtener la cantidad de asientos por tipo de pasajero
            var totEconomico = data.Asientos.Where(q => q.TipoPasajero == 1).Count();
            var totNormal = data.Asientos.Where(q => q.TipoPasajero == 2).Count();
            var totEjecutivo = data.Asientos.Where(q => q.TipoPasajero == 3).Count();
            // calcular el costo por cada boleto generado
            var costoNormal = vuelo.Costo * (1 + vuelo.PorcExtraNormal / 100);
            var costoEjecutivo = costoNormal * (1 + vuelo.PorcExtraEjecutivo / 100);
            operacion.Costo = totEconomico * vuelo.Costo + totNormal * costoNormal + totEjecutivo * costoEjecutivo;
            operacion.FormaPago = Domain.FormaPago.Efectivo;
            operacion.Pagado = true;
            _operacionRepository.Add(operacion);

            foreach (var asiento in data.Asientos)
            {
                var pasajero = new Domain.Pasajero();
                pasajero.NumeroAsiento = asiento.NumeroAsiento;
                pasajero.Folio = vuelo.Numero.PadLeft(3, '0') + "-" + asiento.NumeroAsiento.ToString().PadLeft(3, '0');
                pasajero.Nombre = cliente.Nombre;
                pasajero.PrimerApellido = cliente.PrimerApellido;
                pasajero.SegundoApellido = cliente.SegundoApellido;
                pasajero.TipoPasajero = (Domain.TipoPasajero)asiento.TipoPasajero;
                pasajero.Operacion = operacion;
                pasajero.VueloId = vuelo.Id;

                _pasajeroRepository.Add(pasajero);
            }
            
            return new CreateReservacionVueloResponseMessage(Messages.Base.ValidationResult.CreateValidResult());
        }
    }
}
