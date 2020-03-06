using System;
using System.Collections.Generic;
using System.Text;

namespace AeropuertoApp.Domain
{
    public class Vuelo
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public int AvionId { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaLlegada { get; set; }
        public int AeropuertoOrigenId { get; set; }
        public int AeropuertoDestinoId { get; set; }
        public decimal Costo { get; set; }
        public decimal PorcExtraNormal { get; set; }
        public decimal PorcExtraEjecutivo { get; set; }

        public Aeropuerto AeropuertoOrigen { get; set; }
        public Aeropuerto AeropuertoDestino { get; set; }
        public Avion Avion { get; set; }
        public virtual ICollection<Pasajero> Pasajeros { get; set; }
        public virtual ICollection<DetalleOperacion> DetallesOperacion { get; set; }
    }
}
