using Xunit;
using NSubstitute;
using FluentAssertions;
using Shared.Exceptions;
using Business.Services;
using Business.Contracts.Requests;
using Business.Contracts.Interfaces;
using DataAccess.Entities;
using DataAccess.Contracts.Interfaces;

namespace Tests.Unit {
    public class DogUnitTests {
        private readonly IDogRepository _dogRepoMock;
        private readonly IDogService _dogService;

        public DogUnitTests() {
            _dogRepoMock = Substitute.For<IDogRepository>();
            _dogService = new DogService(_dogRepoMock);
        }


		[Fact]
		public async Task Add_DogAlreadyExists_ThrowsException() {
            // Arrange
            string existingDogsName = "Jack";
            DogEntity entity = new DogEntity {
                Color = "black",
                Name = existingDogsName,
                TailLength = 10,
                Weight = 5
            };
            DogAddRequest request = new DogAddRequest(existingDogsName, "white", 5, 3);
            _dogRepoMock.GetByName(existingDogsName).Returns(entity);

            // Act & Assert
            await FluentActions
                .Awaiting(() => _dogService.Add(request))
                .Should().ThrowAsync<AlreadyExistsException>()
                .Where(e => e.Message.StartsWith("Dog already exists"));
		}


        [Fact]
        public async Task Add_IncorrectColorValue_ThrowsException() {
            // Arrange
            DogAddRequest request = new DogAddRequest("Jane", "!white123", 10, 5);
            _dogRepoMock.GetByName(Arg.Any<string>()).Returns(Task.FromResult<DogEntity?>(null));

            // Act & Assert
            await FluentActions
                .Awaiting(() => _dogService.Add(request))
                .Should().ThrowAsync<ArgumentException>()
                .Where(e => e.Message.StartsWith("Color can only contain letters"));
        }

        [Fact]
        public async Task Add_EmptyName_ThrowsException() {
            // Arrange
            DogAddRequest request = new DogAddRequest("", "amber", 10, 5);
            _dogRepoMock.GetByName(Arg.Any<string>()).Returns(Task.FromResult<DogEntity?>(null));

            // Act & Assert
            await FluentActions
                .Awaiting(() => _dogService.Add(request))
                .Should().ThrowAsync<ArgumentException>()
                .Where(e => e.Message.StartsWith("Dog name cannot be empty"));
        }


        [Fact]
        public async Task Get_DogDoesNotExist_ThrowsException() {
            // Arrange
            _dogRepoMock.GetById(Arg.Any<int>()).Returns(Task.FromResult<DogEntity?>(null));

            // Act & Assert
            await FluentActions
                .Awaiting(() => _dogService.Get(1))
                .Should().ThrowAsync<NotFoundException>()
                .Where(e => e.Message.StartsWith("Dog was not found"));
        }
    }
}
