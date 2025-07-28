using Microsoft.EntityFrameworkCore;
using DataAccess.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Repositories.EntityFramework;
using DataAccess.Repositories.EntityFramework.Data;

namespace DataAccess.Configuration {
    public static class DataAccessServices {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString) {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IDogRepository, DogRepository>();
            return services;
        }
    }
}
