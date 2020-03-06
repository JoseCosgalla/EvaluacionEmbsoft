using System;
using System.Collections.Generic;
using System.Linq;
using AeropuertoApp.UseCases.Messages.FindVueloParaReservar;

namespace AeropuertoApp.UseCases.Interactors
{
    public class FindVueloParaReservarInteractor : Base.IRequestHandler<FindVueloParaReservarRequestMessage, FindVueloParaReservarResponseMessage>
    {
        private readonly Domain.Queries.Base.IQuery<Domain.Vuelo> _vueloQuery;

        public FindVueloParaReservarInteractor(EntityFramework.Database.IDataBaseSqlServerEntityFramework database)
        {
            this._vueloQuery = new EntityFramework.Queries.QueryBase<Domain.Vuelo>(database);
        }

        public FindVueloParaReservarResponseMessage Handle(FindVueloParaReservarRequestMessage data)
        {
            _vueloQuery.Init();
            _vueloQuery.AddWhere(q => q.Id == data.VueloId);

            _vueloQuery.IncludeObject(q => q.AeropuertoOrigen);
            _vueloQuery.IncludeObject(q => q.AeropuertoDestino);
            _vueloQuery.IncludeObject(q => q.Avion.Aerolinea);
            _vueloQuery.IncludeObject(q => q.Pasajeros);
            var vuelos = _vueloQuery.Execute();
            var vuelo = vuelos.FirstOrDefault();
            var vueloMapper = Mapper.ObjectMapper.Mapper.Map<Domain.Vuelo, Messages.FindVueloParaReservar.Vuelo>(vuelo);
            if(vuelo.Pasajeros != null)
            {
                List<int> asientosOcupados = new List<int>();
                foreach(var pasajero in vuelo.Pasajeros)
                {
                    asientosOcupados.Add(pasajero.NumeroAsiento);
                }
                vueloMapper.AsientosOcupados = asientosOcupados.ToArray();
            }
            return new FindVueloParaReservarResponseMessage(Messages.Base.ValidationResult.CreateValidResult(), vueloMapper);
        }
    }
}
