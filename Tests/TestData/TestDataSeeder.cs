using Bogus;
using DataAccess.Entities;
using DataAccess.Repositories.EntityFramework.Data;

namespace Tests.TestData {
    public class TestDataSeeder {
        private readonly ApplicationDbContext _context;

        public readonly List<DogEntity> Dogs = new();

        public TestDataSeeder(ApplicationDbContext context) {
            _context = context;
        }

        public async Task Seed() {
            await SeedDogs(30);
        }

        private async Task SeedDogs(int amount) {
            var dogFaker = new Faker<DogEntity>()
                .RuleFor(d => d.Color, f => f.Commerce.Color())
                .RuleFor(d => d.Name, f => f.Person.FirstName)
                .RuleFor(d => d.TailLength, f => f.Random.Int(1, 100))
                .RuleFor(d => d.Weight, f => f.Random.Int(0, 50));

            Dogs.AddRange(dogFaker.Generate(amount));
            await _context.Dogs.AddRangeAsync(Dogs);
            await _context.SaveChangesAsync();
        }
    }
}
