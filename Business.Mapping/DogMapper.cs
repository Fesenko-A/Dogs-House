using Business.Entities;
using Business.Contracts.Dto;
using DataAccess.Entities;

namespace Business.Mapping {
    public static class DogMapper {
        public static DogDto ToDto(DogEntity entity) {
            return new DogDto(entity.Name, entity.Color, entity.TailLength, entity.Weight);
        }

        public static IEnumerable<DogDto> ToDtoList(IEnumerable<DogEntity> entities) {
            return entities.Select(dog => new DogDto(dog.Name, dog.Color, dog.TailLength, dog.Weight)); 
        }

        public static DogEntity ToEntity(Dog dog) {
            return new DogEntity {
                Name = dog.Name.ToString(),
                Color = dog.Color.ToString(),
                TailLength = (int)dog.TailLength,
                Weight = (int)dog.Weight
            };
        }
    }
}
