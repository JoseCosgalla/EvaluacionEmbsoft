using System.Collections.Generic;

namespace AeropuertoApp.UseCases.Messages.FindVueloParaReservar
{
    public class FindVueloParaReservarResponseMessage
    {
        public Base.ValidationResult ValidationResult { get; set; }
        public Vuelo Vuelos { get; set; }

        public FindVueloParaReservarResponseMessage(Base.ValidationResult validationResult, Vuelo vuelos)
        {
            this.ValidationResult = validationResult;
            this.Vuelos = vuelos;
        }
    }
}
