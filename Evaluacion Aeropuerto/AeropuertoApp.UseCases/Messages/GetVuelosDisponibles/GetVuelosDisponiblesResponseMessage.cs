using System.Collections.Generic;

namespace AeropuertoApp.UseCases.Messages.GetVuelosDisponibles
{
    public class GetVuelosDisponiblesResponseMessage
    {
        public Base.ValidationResult ValidationResult { get; set; }
        public Vuelo[] Vuelos { get; set; }

        public GetVuelosDisponiblesResponseMessage(Base.ValidationResult validationResult, Vuelo[] vuelos)
        {
            this.ValidationResult = validationResult;
            this.Vuelos = vuelos;
        }
    }
}
