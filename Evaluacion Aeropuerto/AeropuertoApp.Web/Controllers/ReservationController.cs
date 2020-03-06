using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AeropuertoApp.Web.Controllers
{
    public class ReservationController : Controller
    {
        private Models.Reservation.GetAvailableFlghtsResponse GenerateGetAvailableFlights(DateTime? FechaInicio, DateTime? FechaFinal)
        {
            var database = new EntityFramework.Database.DatabaseEntityFramework();
            var useCase = new UseCases.Interactors.GetVuelosInteractor(database, this._mapper);
            var request = new UseCases.Messages.GetVuelosDisponibles.GetVuelosDisponiblesRequestMessage();

            if (FechaInicio.HasValue && FechaInicio != DateTime.MinValue)
            {
                request.FechaInicio = FechaInicio;
            }
            if (FechaFinal.HasValue && FechaFinal != DateTime.MinValue)
            {
                request.FechaFinal = FechaFinal;
            }

            try
            {
                database.Open();
                var response = useCase.Handle(request);
                var vuelosResponse = this._mapper.Map<UseCases.Messages.GetVuelosDisponibles.Vuelo[], Models.Reservation.Vuelo[]>(response.Vuelos);

                var responseWeb = new Models.Reservation.GetAvailableFlghtsResponse();
                responseWeb.Request = null;
                responseWeb.Vuelos = vuelosResponse;
                return responseWeb;
            }
            finally
            {
                database.Close();
            }
        }
        private IMapper _mapper;
        public ReservationController(IMapper mapper)
        {
            this._mapper = mapper;
        }

        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAvailableFlights(Models.Reservation.GetAvailableFlghtsResponse requestWeb)
        {
            if(requestWeb != null && requestWeb.Request != null)
                return View(GenerateGetAvailableFlights(requestWeb.Request.FechaInicio, requestWeb.Request.FechaFinal));

            return View(GenerateGetAvailableFlights(null, null));
        }

        [HttpPost]
        public ActionResult GetFilterAvailableFlights(Models.Reservation.GetAvailableFlghtsResponse requestWeb)
        {
            return View(GenerateGetAvailableFlights(requestWeb.Request.FechaInicio, requestWeb.Request.FechaFinal));
        }

        [HttpGet]
        public ActionResult DoReservation(int? vueloId)
        {
            var database = new EntityFramework.Database.DatabaseEntityFramework();
            var useCase = new UseCases.Interactors.FindVueloParaReservarInteractor(database, this._mapper);
            var request = new UseCases.Messages.FindVueloParaReservar.FindVueloParaReservarRequestMessage();

            request.VueloId = vueloId.Value;

            try
            {
                database.Open();
                var response = useCase.Handle(request);
                // Mapear las propiedades
                var vueloReservacionresponse = this._mapper.Map<UseCases.Messages.FindVueloParaReservar.Vuelo, Models.Reservation.DoReservation> (response.Vuelos);

                var listaAsientos = new List<Models.Reservation.TicketAvailable>();
                for (var i = 1; i <= response.Vuelos.CapacidadTotal; i++)
                {
                    if (response.Vuelos.AsientosOcupados.Where(q => q == i).Count() == 0)
                    {
                        listaAsientos.Add(new Models.Reservation.TicketAvailable()
                        {
                            NumeroAsiento = i,
                            TipoPasajero = Models.Enums.ETipoPasajero.Economico
                        });
                    }
                }
                vueloReservacionresponse.Tickets = listaAsientos;

                return View(vueloReservacionresponse);
            }
            finally
            {
                database.Close();
            }
        }

        [HttpPost]
        public ActionResult DoReservation(Models.Reservation.DoReservation requestWeb)
        {
            int ClienteId = 1;

            var database = new EntityFramework.Database.DatabaseEntityFramework();
            var useCase = new UseCases.Interactors.CreateReservacionVueloInteractor(database, this._mapper);
            var request = new UseCases.Messages.CreateReservacionVuelo.CreateReservacionVueloRequestMessage();
            request.VueloId = requestWeb.VueloId;
            request.ClienteId = ClienteId;
            var reservados = new List<UseCases.Messages.CreateReservacionVuelo.AsientoReservado>();
            foreach (var reservacion in requestWeb.Tickets)
            {
                if (reservacion.Selected)
                {
                    reservados.Add(new UseCases.Messages.CreateReservacionVuelo.AsientoReservado()
                    {
                        NumeroAsiento = reservacion.NumeroAsiento,
                        TipoPasajero = (int)reservacion.TipoPasajero
                    });
                }
            }
            request.Asientos = reservados.ToArray();
            try
            {
                database.Open();
                var response = useCase.Handle(request);
                if (response.ValidationResult.IsValid)
                {
                    database.Commit();
                } else
                {
                    Vereyon.Web.FlashMessage.Danger("No se pudo generar la reservación", response.ValidationResult.Error);
                    database.Rollback();
                    return View(requestWeb);
                }
            } catch
            {
                database.Rollback();
                throw;
            }
            finally
            {
                database.Close();
            }

            return RedirectToAction("GetAvailableFlights");
        }
    }
}