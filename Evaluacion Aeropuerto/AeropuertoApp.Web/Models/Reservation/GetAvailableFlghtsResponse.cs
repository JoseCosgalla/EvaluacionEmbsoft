using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AeropuertoApp.Web.Models.Reservation
{
    public class GetAvailableFlghtsResponse
    {
        public GetAvailableFlightsRequest Request { get; set; }
        public Vuelo[] Vuelos { get; set; }
    }

    public class Vuelo
    {
        public int VueloId { get; set; }
        [Display(Name = "# Vuelo")]
        public string NumeroVuelo { get; set; }
        [Display(Name = "Aerolinea")]
        public string Aerolinea { get; set; }
        [Display(Name = "Hora salida")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime FechaHoraSalida { get; set; }
        [Display(Name = "Hora llegada")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime FechaHoraLlegada { get; set; }
        [Display(Name = "Origen")]
        public string AeropuertoOrigen { get; set; }
        [Display(Name = "Destino")]
        public string AeropuertoDestino { get; set; }
        [Display(Name = "Duración")]
        public TimeSpan DuracionViaje { get; set; }
        [Display(Name = "Costo económico")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal CostoEconomico { get; set; }
        [Display(Name = "Costo normal")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal CostoNormal { get; set; }
        [Display(Name = "Costo ejecutivo")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal CostoEjecutivo { get; set; }
        [Display(Name = "# disponible")]
        public int AsientosDisponibles { get; set; }
    }
}