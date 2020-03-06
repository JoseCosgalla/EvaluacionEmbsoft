using System.Collections.Generic;

namespace AeropuertoApp.Domain
{
    public class Avion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CapacidadDisponible { get; set; }
        public int AerolineaId { get; set; }

        public Aerolinea Aerolinea { get; set; }
        public virtual ICollection<Vuelo> Vuelos { get; set; }
    }
}
