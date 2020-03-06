using System.Collections.Generic;

namespace AeropuertoApp.Domain
{
    public class Aerolinea
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Avion> Aviones { get; set; }
    }
}
