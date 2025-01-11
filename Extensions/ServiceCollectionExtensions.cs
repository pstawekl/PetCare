
using Microsoft.Extensions.DependencyInjection;
using PetCare;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVisitRepository, VisitRepository>();
        services.AddScoped<IReminderRepository, ReminderRepository>();

        return services;
    }
}