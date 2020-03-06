using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AeropuertoApp.Web.Models.Reservation
{
    public class GetAvailableFlightsRequest
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int AeropuertoOrigen { get; set; }
        public int AeropuertoDestino { get; set; }
    }
}