using System;

namespace AeropuertoApp.UseCases.Messages.Base
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string Error { get; set; }

        public static ValidationResult CreateValidResult()
        {
            return new ValidationResult()
            {
                IsValid = true
            };
        }
    }
}
