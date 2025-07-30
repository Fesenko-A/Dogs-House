using Xunit;
using Shared.Filters;
using FluentAssertions;
using Business.Mapping;
using Business.Contracts.Requests;
using Business.Contracts.Interfaces;
using Tests.Integration.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Integration {
    public class DogIntegrationTests : BaseIntegrationTest {
        private readonly IDogService _dogService;

        public DogIntegrationTests(IntegrationTestWebAppFactory factory) : base(factory) {
            _dogService = scope.ServiceProvider.GetRequiredService<IDogService>();
        }

		[Fact]
		public async Task GetAll_DogsNoFilters_ReturnsDogs() {
			// Arrange
            DogFilter filter = new DogFilter();

			// Act
            var result = await _dogService.GetAll(filter);

			// Assert
            var dogsFromDb = await dbContext.Dogs
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            result.Should().BeEquivalentTo(dogsFromDb.Select(DogMapper.ToDto));
        }

		[Fact]
		public async Task GetAll_DogsCustomPagination_ReturnsDogs() {
            // Arrange
            DogFilter filter = new DogFilter {
                PageSize = 3,
                PageNumber = 2
            };

            // Act
            var result = await _dogService.GetAll(filter);

            // Assert
            var dogsFromDb = await dbContext.Dogs
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            result.Should().BeEquivalentTo(dogsFromDb.Select(DogMapper.ToDto));
        }

        [Fact]
        public async Task GetAll_DogsOrderByNameDesc_ReturnsDogs() {
            // Arrange
            DogFilter filter = new DogFilter {
                SortOrder = SortOrder.Desc,
                AttributeSort = DogAttributeSort.Name,
            };

            // Act
            var result = await _dogService.GetAll(filter);

            // Assert
            var dogsFromDb = await dbContext.Dogs
                .OrderByDescending(x => x.Name)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            result.Should().BeEquivalentTo(dogsFromDb.Select(DogMapper.ToDto));
        }

        [Fact]
        public async Task GetAll_DogsOrderByWeightAscCustomPagination_ReturnsDogs() {
            // Arrange
            DogFilter filter = new DogFilter {
                SortOrder = SortOrder.Asc,
                AttributeSort = DogAttributeSort.Weight,
                PageNumber = 4,
                PageSize = 4
            };

            // Act
            var result = await _dogService.GetAll(filter);

            // Assert
            var dogsFromDb = await dbContext.Dogs
                .OrderBy(x => x.Weight)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            result.Should().BeEquivalentTo(dogsFromDb.Select(DogMapper.ToDto));
        }

        [Fact]
        public async Task Get_ExistingDog_ReturnsDog() {
            // Arrange
            int existingDogId = dataSeeder.Dogs.First().Id;

            // Act
            var result = await _dogService.Get(existingDogId);

            // Assert
            var dogFromDb = await dbContext.Dogs.FindAsync(existingDogId);
            result.Should().BeEquivalentTo(DogMapper.ToDto(dogFromDb!));
        }

        [Fact]
        public async Task Add_CorrectRequest_ReturnsDog() {
            // Arrange
            var request = new DogAddRequest("very unique name", "black", 10, 5);

            // Act
            var result = await _dogService.Add(request);

            // Assert
            var dogFromDb = await dbContext.Dogs.FirstAsync(d => d.Name == request.Name);

            result.Should().BeEquivalentTo(DogMapper.ToDto(dogFromDb));
        }
    }
}
