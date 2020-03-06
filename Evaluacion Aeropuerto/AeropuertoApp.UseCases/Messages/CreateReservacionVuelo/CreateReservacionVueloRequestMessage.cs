using System;

namespace AeropuertoApp.UseCases.Messages.CreateReservacionVuelo
{
    public class CreateReservacionVueloRequestMessage
    {
        public int VueloId { get; set; }
        public int ClienteId { get; set; }
        public string NumeroTarjeta { get; set; }
        public string ExpiracionTarjeta { get; set; }
        public string CVVTarjeta { get; set; }
        public AsientoReservado[] Asientos { get; set; }
    }

    public class AsientoReservado
    {
        public int NumeroAsiento { get; set; }
        public int TipoPasajero { get; set; }
    }
}
