using BLL.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace API.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddCustomValidation(this IServiceCollection services)
    {
        return services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining<OfficeValidator>();
    }
}