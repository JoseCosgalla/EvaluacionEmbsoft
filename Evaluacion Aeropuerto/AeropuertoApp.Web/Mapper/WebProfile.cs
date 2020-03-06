using AutoMapper;
using System;

namespace AeropuertoApp.Web.Mapper
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<AeropuertoApp.UseCases.Messages.GetVuelosDisponibles.Vuelo, Models.Reservation.Vuelo>();
            CreateMap<AeropuertoApp.UseCases.Messages.FindVueloParaReservar.Vuelo, Models.Reservation.DoReservation>()
                .ForMember(dest => dest.Tickets, act => act.Ignore());
        }
    }
}