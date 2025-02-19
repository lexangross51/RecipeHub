using FluentResults;
using FluentValidation;
using MediatR;

namespace RecipeHub.Application.Common;

internal abstract class RequestHandler<TRequest>(IValidator<TRequest>? validator = default)
    : IRequestHandler<TRequest, Result> where TRequest : IRequest<Result>
{
    public async Task<Result> Handle(TRequest request, CancellationToken cancellationToken)
    {
        if (validator != null)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken)
                .ConfigureAwait(false);

            if (!validationResult.IsValid)
            {
                string message = $"Во время валидации {typeof(TRequest).Name} произошли ошибки: " +
                    $"{string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}";

                return new Result().WithError(message);
            }
        }

        return await HandleAsync(request, cancellationToken).ConfigureAwait(false);
    }

    protected abstract Task<Result> HandleAsync(TRequest request, CancellationToken cancellationToken);
}

internal abstract class RequestHandler<TRequest, TResponse>(IValidator<TRequest>? validator = default)
    : IRequestHandler<TRequest, Result<TResponse>> where TRequest : IRequest<Result<TResponse>>
{
    public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        if (validator != null)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken)
                .ConfigureAwait(false);

            if (!validationResult.IsValid)
            {
                string message = $"Во время валидации {typeof(TRequest).Name} произошли ошибки: " +
                    $"{string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))}";

                return new Result<TResponse>().WithError(message);
            }
        }

        return await HandleAsync(request, cancellationToken).ConfigureAwait(false);
    }

    protected abstract Task<Result<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken);
}