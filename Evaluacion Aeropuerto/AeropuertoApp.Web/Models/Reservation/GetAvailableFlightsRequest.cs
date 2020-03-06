using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AeropuertoApp.Web.Models.Reservation
{
    public class GetAvailableFlightsRequest
    {
        [Display(Name = "Fecha inicio")]
        public DateTime FechaInicio { get; set; }
        [Display(Name = "Fecha final")]
        public DateTime FechaFinal { get; set; }
        [Display(Name = "Origen")]
        public int AeropuertoOrigen { get; set; }
        [Display(Name = "Destino")]
        public int AeropuertoDestino { get; set; }
    }
}