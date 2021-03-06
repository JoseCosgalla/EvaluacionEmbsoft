﻿using System;
using System.Linq;
using AeropuertoApp.UseCases.Messages.GetVuelosDisponibles;
using AutoMapper;

namespace AeropuertoApp.UseCases.Interactors
{
    public class GetVuelosInteractor : Base.IRequestHandler<GetVuelosDisponiblesRequestMessage, GetVuelosDisponiblesResponseMessage>
    {
        private readonly Domain.Queries.Base.IQuery<Domain.Vuelo> _vueloQuery;
        private readonly IMapper _mapper;

        public GetVuelosInteractor(EntityFramework.Database.IDataBaseSqlServerEntityFramework database, IMapper mapper)
        {
            this._vueloQuery = new EntityFramework.Queries.QueryBase<Domain.Vuelo>(database);
            this._mapper = mapper;
        }

        public GetVuelosDisponiblesResponseMessage Handle(GetVuelosDisponiblesRequestMessage data)
        {
            _vueloQuery.Init();
            if (data.FechaInicio.HasValue)
            {
                DateTime fechaInicio = data.FechaInicio.Value.Date;
                _vueloQuery.AddWhere(q => q.FechaSalida >= fechaInicio);
            }
            if (data.FechaFinal.HasValue)
            {
                DateTime fechaFinal = data.FechaFinal.Value.Date.AddDays(1);
                _vueloQuery.AddWhere(q => q.FechaSalida <= fechaFinal);
            }
            if (data.AeropuertoOrigen.HasValue)
            {
                _vueloQuery.AddWhere(q => q.AeropuertoOrigenId == data.AeropuertoOrigen);
            }
            if (data.AeropuertoDestino.HasValue)
            {
                _vueloQuery.AddWhere(q => q.AeropuertoDestinoId == data.AeropuertoDestino);
            }

            _vueloQuery.IncludeObject(q => q.AeropuertoOrigen);
            _vueloQuery.IncludeObject(q => q.AeropuertoDestino);
            _vueloQuery.IncludeObject(q => q.Avion.Aerolinea);
            _vueloQuery.Sort("DESC", "FechaSalida");
            var vuelos = _vueloQuery.Execute();
            var vuelosMapper = this._mapper.Map<Domain.Vuelo[], Messages.GetVuelosDisponibles.Vuelo[]>(vuelos.ToArray());

            return new GetVuelosDisponiblesResponseMessage(Messages.Base.ValidationResult.CreateValidResult(), vuelosMapper);
        }
    }
}
