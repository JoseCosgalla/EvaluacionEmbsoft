using System;

namespace AeropuertoApp.UseCases.Messages.GetVuelosDisponibles
{
    /// <summary>
    /// Obtiene una lista de vuelos disponibles para viajar
    /// </summary>
    public class GetVuelosDisponiblesRequestMessage
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; }
        public int? AeropuertoOrigen { get; set; }
        public int? AeropuertoDestino { get; set; }
    }
}
