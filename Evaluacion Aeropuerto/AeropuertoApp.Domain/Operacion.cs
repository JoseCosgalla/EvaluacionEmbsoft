using System;
using System.Collections.Generic;

namespace AeropuertoApp.Domain
{
    public class Operacion
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Costo { get; set; }
        public bool Pagado { get; set; }
        public FormaPago FormaPago { get; set; }

        public Cliente Cliente { get; set; }
        public virtual ICollection<Pasajero> Pasajeros { get; set; }
        public virtual ICollection<DetalleOperacion> DetallesOperacion { get; set; }
    }
}
