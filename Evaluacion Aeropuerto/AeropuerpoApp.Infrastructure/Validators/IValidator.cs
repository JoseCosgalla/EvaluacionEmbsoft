namespace AeropuertoApp.Infrastructure.Validators
{
    public interface IValidator<in T>
    {
        void ValidateAndThrowException(T request, string ruleSet);
        void ValidateAndThrowException(T request); 
    }
}