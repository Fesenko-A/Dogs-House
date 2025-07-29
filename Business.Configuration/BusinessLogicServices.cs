using Business.Services;
using Business.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Configuration {
    public static class BusinessLogicServices {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services) {
            services.AddScoped<IDogService, DogService>();
            return services;
        }
    }
}
