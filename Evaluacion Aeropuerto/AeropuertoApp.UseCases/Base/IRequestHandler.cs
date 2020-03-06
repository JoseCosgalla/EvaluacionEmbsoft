using System;

namespace AeropuertoApp.UseCases.Interactors.Base
{
    internal interface IRequestHandler<in TRequest, out TResponse>
    {
        TResponse Handle(TRequest data);
    }
}
