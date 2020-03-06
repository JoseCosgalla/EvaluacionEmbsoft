using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AeropuertoApp.Web.Models.Reservation
{
    public class DoReservation
    {
        public int VueloId { get; set; }
        [Display(Name = "Origen")]
        public string AeropuertoOrigen { get; set; }
        [Display(Name = "# Vuelo")]
        public string NumeroVuelo { get; set; }
        [Display(Name = "Destino")]
        public string AeropuertoDestino { get; set; }
        [Display(Name = "Costo económico")]
        public decimal CostoEconomico { get; set; }
        [Display(Name = "Costo normal")]
        public decimal CostoNormal { get; set; }
        [Display(Name = "Costo ejecutivo")]
        public decimal CostoEjecutivo { get; set; }
        public List<TicketAvailable> Tickets { get; set; }
        public SelectList ListaTiposPasajeros { get; set; }

        public DoReservation()
        {
            this.Tickets = new List<TicketAvailable>();
        }
    }

    public class TicketAvailable
    {
        public bool Selected { get; set; }
        public int Id { get; set; }
        public int NumeroAsiento { get; set; }
        public int TipoPasajero { get; set; }
    }
}