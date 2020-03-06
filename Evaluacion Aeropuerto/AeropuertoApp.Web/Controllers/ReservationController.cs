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
            var useCase = new UseCases.Interactors.GetVuelosInteractor(database);
            var request = new UseCases.Messages.GetVuelosDisponibles.GetVuelosDisponiblesRequestMessage();

            request.FechaInicio = DateTime.Now.AddDays(-1);
            request.FechaFinal = DateTime.Now;

            if (request != null && request.FechaInicio.HasValue)
            {
                request.FechaInicio = FechaInicio;
            }
            if (request != null && request.FechaFinal.HasValue)
            {
                request.FechaFinal = FechaFinal;
            }

            try
            {
                database.Open();
                var response = useCase.Handle(request);
                var vuelosResponse = AutoMapper.Mapper.Map<UseCases.Messages.GetVuelosDisponibles.Vuelo[], Models.Reservation.Vuelo[]>(response.Vuelos);

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
            var useCase = new UseCases.Interactors.FindVueloParaReservarInteractor(database);
            var request = new UseCases.Messages.FindVueloParaReservar.FindVueloParaReservarRequestMessage();

            request.VueloId = vueloId.Value;

            try
            {
                database.Open();
                var response = useCase.Handle(request);
                // Mapear las propiedades
                var vueloReservacionresponse = new Models.Reservation.DoReservation();
                vueloReservacionresponse.AeropuertoOrigen = response.Vuelos.AeropuertoOrigen;
                vueloReservacionresponse.AeropuertoDestino = response.Vuelos.AeropuertoDestino;
                vueloReservacionresponse.CostoEconomico = response.Vuelos.CostoEconomico;
                vueloReservacionresponse.CostoNormal = response.Vuelos.CostoNormal;
                vueloReservacionresponse.CostoEjecutivo = response.Vuelos.CostoEjecutivo;
                vueloReservacionresponse.VueloId = response.Vuelos.VueloId;
                vueloReservacionresponse.NumeroVuelo = response.Vuelos.NumeroVuelo;

                var listaAsientos = new List<Models.Reservation.TicketAvailable>();
                for (var i = 1; i <= response.Vuelos.CapacidadTotal; i++)
                {
                    if (response.Vuelos.AsientosOcupados.Where(q => q == i).Count() == 0)
                    {
                        listaAsientos.Add(new Models.Reservation.TicketAvailable()
                        {
                            NumeroAsiento = i,
                            TipoPasajero = 1
                        });
                    }
                }
                vueloReservacionresponse.Tickets = listaAsientos;

                var lista = new List<SelectListItem>();
                lista.Add(new SelectListItem()
                {
                    Value = "1",
                    Text = "Economico"
                });
                lista.Add(new SelectListItem()
                {
                    Value = "2",
                    Text = "Normal"
                });
                lista.Add(new SelectListItem()
                {
                    Value = "3",
                    Text = "Ejecutivo"
                });
                vueloReservacionresponse.ListaTiposPasajeros = new SelectList(lista, "Value", "Text", "1");

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
            var useCase = new UseCases.Interactors.CreateReservacionVueloInteractor(database);
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
                        TipoPasajero = reservacion.TipoPasajero
                    });
                }
            }
            request.Asientos = reservados.ToArray();
            try
            {
                database.Open();
                var response = useCase.Handle(request);
                database.Commit();
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