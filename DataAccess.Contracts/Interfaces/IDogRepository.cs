using Shared.Filters;
using DataAccess.Entities;

namespace DataAccess.Contracts.Interfaces {
    public interface IDogRepository {
        Task<DogEntity?> Get(int id);
        Task<IEnumerable<DogEntity>> GetAll(DogFilter filter);
        Task<DogEntity> Add(DogEntity entity);
    }
}
