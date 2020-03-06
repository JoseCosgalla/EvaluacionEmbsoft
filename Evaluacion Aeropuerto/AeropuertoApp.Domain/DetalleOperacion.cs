using System;
using System.Collections.Generic;

namespace AeropuertoApp.Domain
{
    public class DetalleOperacion
    {
        public int Id { get; set; }
        public int OperacionId { get; set; }
        public int VueloId { get; set; }
        public decimal Costo { get; set; }

        public Vuelo Vuelo { get; set; }
        public Operacion Operacion { get; set; }
    }
}
