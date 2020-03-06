using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AeropuertoApp.Web.App_Start
{
    public class AeropuertoAppMyProfile : Profile
    {
        public override string ProfileName
        {
            get { return "AeropuertoAppProfileName"; }
        }

        protected AeropuertoAppMyProfile()
        {
            CreateMap<AeropuertoApp.UseCases.Messages.GetVuelosDisponibles.Vuelo, Models.Reservation.Vuelo>();
            CreateMap<AeropuertoApp.UseCases.Messages.FindVueloParaReservar.Vuelo, Models.Reservation.DoReservation>()
                .ForMember(dest => dest.Tickets, act => act.Ignore())
                .ForMember(dest => dest.ListaTiposPasajeros, act => act.Ignore());
        }
    }
}