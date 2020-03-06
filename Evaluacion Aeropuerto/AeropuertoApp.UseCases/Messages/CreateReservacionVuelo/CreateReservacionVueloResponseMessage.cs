using System.Collections.Generic;

namespace AeropuertoApp.UseCases.Messages.CreateReservacionVuelo
{
    public class CreateReservacionVueloResponseMessage
    {
        public Base.ValidationResult ValidationResult { get; set; }

        public CreateReservacionVueloResponseMessage(Base.ValidationResult validationResult)
        {
            this.ValidationResult = validationResult;
        }
    }
}
