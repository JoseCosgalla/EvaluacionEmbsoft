using System;
using System.Collections.Generic;
using System.Linq;
using AeropuertoApp.UseCases.Messages.FindVueloParaReservar;
using AutoMapper;

namespace AeropuertoApp.UseCases.Interactors
{
    public class FindVueloParaReservarInteractor : Base.IRequestHandler<FindVueloParaReservarRequestMessage, FindVueloParaReservarResponseMessage>
    {
        private readonly Domain.Queries.Base.IQuery<Domain.Vuelo> _vueloQuery;
        private readonly IMapper _mapper;

        public FindVueloParaReservarInteractor(EntityFramework.Database.IDataBaseSqlServerEntityFramework database, IMapper mapper)
        {
            this._vueloQuery = new EntityFramework.Queries.QueryBase<Domain.Vuelo>(database);
            this._mapper = mapper;
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
            var vueloMapper = this._mapper.Map<Domain.Vuelo, Messages.FindVueloParaReservar.Vuelo>(vuelo);
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
