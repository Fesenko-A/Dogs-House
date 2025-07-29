using Shared.Filters;
using Shared.Exceptions;
using Business.Mapping;
using Business.Entities;
using Business.Contracts.Dto;
using Business.Contracts.Requests;
using Business.Contracts.Interfaces;
using DataAccess.Contracts.Interfaces;

namespace Business.Services {
    public class DogService : IDogService {
        private readonly IDogRepository _repository;

        public DogService(IDogRepository repository) {
            _repository = repository;
        }

        public async Task<DogDto> Add(DogAddRequest request) {
            var existingDog = await _repository.GetByName(request.Name);
            if (existingDog != null)
                throw new AlreadyExistsException(typeof(Dog));

            Color color = Color.Create(request.Color);
            DogName name = DogName.Create(request.Name);
            Dog dog = Dog.Create(color, name, request.TailLength, request.Weight);

            var dogEntity = DogMapper.ToEntity(dog);
            var savedDog = await _repository.Add(dogEntity);
            return DogMapper.ToDto(savedDog);
        }

        public async Task<DogDto> Get(int id) {
            var dogEntity = await _repository.GetById(id);
            if (dogEntity == null)
                throw new NotFoundException(typeof(Dog));

            return DogMapper.ToDto(dogEntity);
        }

        public async Task<IEnumerable<DogDto>> GetAll(DogFilter filter) {
            var dogs = await _repository.GetAll(filter);
            return DogMapper.ToDtoList(dogs);
        }
    }
}
