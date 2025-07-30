using Xunit;
using Respawn;
using Respawn.Graph;
using Tests.TestData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Repositories.EntityFramework.Data;

namespace Tests.Integration.Infrastructure {
    public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>, IAsyncLifetime {
        protected readonly IServiceScope scope;
        protected readonly ApplicationDbContext dbContext;
        protected readonly TestDataSeeder dataSeeder; 
        
        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory) {
            scope = factory.Services.CreateScope();
            dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dataSeeder = new TestDataSeeder(dbContext);

            if (dbContext.Database.GetPendingMigrations().Any()) {
                dbContext.Database.Migrate();
            }
        }

        private async Task ResetDatabase() {
            var connection = dbContext.Database.GetDbConnection();
            await connection.OpenAsync();

            var respawner = await Respawner.CreateAsync(connection, new RespawnerOptions {
                DbAdapter = DbAdapter.SqlServer,
                WithReseed = true,
                TablesToIgnore = [
                    new Table("__EFMigrationsHistory")
                ]
            });

            await respawner.ResetAsync(connection);
            await connection.CloseAsync();
        }

        public async Task InitializeAsync() {
            await ResetDatabase();
            await dataSeeder.Seed();
        }

        public Task DisposeAsync() {
            return Task.CompletedTask;
        }
    }
}
