using AutoMapper;
using System;

namespace AeropuertoApp.UseCases.Mapper
{
    public class UseCasesProfile : Profile
    {
        public UseCasesProfile()
        {
            CreateMap<Domain.Vuelo, Messages.GetVuelosDisponibles.Vuelo>()
                    .ForMember(dest => dest.Aerolinea, act => act.MapFrom(src => src.Avion.Aerolinea.Nombre))
                    .ForMember(dest => dest.FechaHoraSalida, act => act.MapFrom(src => src.FechaSalida))
                    .ForMember(dest => dest.FechaHoraLlegada, act => act.MapFrom(src => src.FechaLlegada))
                    .ForMember(dest => dest.AeropuertoOrigen, act => act.MapFrom(src => src.AeropuertoOrigen.Nombre))
                    .ForMember(dest => dest.AeropuertoDestino, act => act.MapFrom(src => src.AeropuertoDestino.Nombre))
                    .ForMember(dest => dest.CostoEconomico, act => act.MapFrom(src => src.Costo))
                    .ForMember(dest => dest.CostoNormal, act => act.MapFrom(src => src.Costo * (1 + src.PorcExtraNormal / 100)))
                    .ForMember(dest => dest.CostoEjecutivo, act => act.MapFrom(src => (src.Costo * (1 + src.PorcExtraNormal / 100)) * (1 + src.PorcExtraEjecutivo / 100)))
                    .ForMember(dest => dest.NumeroVuelo, act => act.MapFrom(src => src.Numero))
                    .ForMember(dest => dest.VueloId, act => act.MapFrom(src => src.Id))
                    .ForMember(dest => dest.DuracionViaje, act => act.MapFrom(src => src.FechaLlegada - src.FechaSalida));

            CreateMap<Domain.Vuelo, Messages.FindVueloParaReservar.Vuelo>()
                    .ForMember(dest => dest.Aerolinea, act => act.MapFrom(src => src.Avion.Aerolinea.Nombre))
                    .ForMember(dest => dest.FechaHoraSalida, act => act.MapFrom(src => src.FechaSalida))
                    .ForMember(dest => dest.FechaHoraSalida, act => act.MapFrom(src => src.FechaLlegada))
                    .ForMember(dest => dest.AeropuertoOrigen, act => act.MapFrom(src => src.AeropuertoOrigen.Nombre))
                    .ForMember(dest => dest.AeropuertoDestino, act => act.MapFrom(src => src.AeropuertoDestino.Nombre))
                    .ForMember(dest => dest.CostoEconomico, act => act.MapFrom(src => src.Costo))
                    .ForMember(dest => dest.CostoNormal, act => act.MapFrom(src => src.Costo * (1 + src.PorcExtraNormal / 100)))
                    .ForMember(dest => dest.CostoEjecutivo, act => act.MapFrom(src => (src.Costo * (1 + src.PorcExtraNormal / 100)) * (1 + src.PorcExtraEjecutivo / 100)))
                    .ForMember(dest => dest.NumeroVuelo, act => act.MapFrom(src => src.Numero))
                    .ForMember(dest => dest.VueloId, act => act.MapFrom(src => src.Id))
                    .ForMember(dest => dest.DuracionViaje, act => act.MapFrom(src => src.FechaLlegada - src.FechaSalida))
                    .ForMember(dest => dest.CapacidadTotal, act => act.MapFrom(src => src.Avion.CapacidadDisponible))
                    .ForMember(dest => dest.AsientosOcupados, act => act.Ignore());
        }
    }
}
