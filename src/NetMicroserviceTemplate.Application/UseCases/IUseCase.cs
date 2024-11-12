namespace NetMicroserviceTemplate.Application.UseCases;

public interface IUseCaseBase { }

public interface IUseCase : IUseCaseBase
{
    Task Execute(CancellationToken cancellationToken = default);
}

public interface IUseCaseWithRequest<TRequest> : IUseCaseBase
{
    Task Execute(TRequest request, CancellationToken cancellationToken = default);
}

public interface IUseCaseWithResponse<TResponse> : IUseCaseBase
{
    Task<TResponse> Execute(CancellationToken cancellationToken = default);
}

public interface IUseCase<TRequest, TResponse> : IUseCaseBase
{
    Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken = default);
}
