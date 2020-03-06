
using System.Collections.Generic;

namespace AeropuertoApp.Domain
{
    public class Aeropuerto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }

        public virtual ICollection<Vuelo> VuelosOrigen { get; set; }
        public virtual ICollection<Vuelo> VuelosDestino { get; set; }
    }
}
