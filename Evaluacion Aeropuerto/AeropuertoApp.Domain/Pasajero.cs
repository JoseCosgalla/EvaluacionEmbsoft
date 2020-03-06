using System.Collections.Generic;

namespace AeropuertoApp.Domain
{
    public class Pasajero
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public int NumeroAsiento { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public int VueloId { get; set; }
        public int OperacionId { get; set; }
        public TipoPasajero TipoPasajero { get; set; }
        public bool Disponible { get; set; }

        public Vuelo Vuelo { get; set; }
        public Operacion Operacion { get; set; }
    }
}
