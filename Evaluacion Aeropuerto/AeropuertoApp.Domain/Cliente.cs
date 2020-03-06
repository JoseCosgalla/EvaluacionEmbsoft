using System;
using System.Collections.Generic;

namespace AeropuertoApp.Domain
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public TipoCliente TipoCliente { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Operacion> Operaciones { get; set; }
    }
}
