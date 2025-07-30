using Xunit;
using Testcontainers.MsSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Repositories.EntityFramework.Data;

namespace Tests.Integration.Infrastructure {
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime {
        private readonly MsSqlContainer _dbContainer = new MsSqlBuilder().Build(); 
        
        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            builder.ConfigureTestServices(services => {
                var descriptor = services
                    .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null) {
                    services.Remove(descriptor);
                }

                services.AddDbContext<ApplicationDbContext>(options => {
                    options.UseSqlServer(_dbContainer.GetConnectionString());
                });
            });
        }

        public Task InitializeAsync() {
            return _dbContainer.StartAsync();
        }

        public new Task DisposeAsync() {
            return _dbContainer.StopAsync();
        }
    }
}
