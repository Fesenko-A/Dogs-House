using Shared.Filters;
using DataAccess.Entities;

namespace DataAccess.Contracts.Interfaces {
    public interface IDogRepository {
        Task<DogEntity?> GetById(int id);
        Task<DogEntity?> GetByName(string name);
        Task<IEnumerable<DogEntity>> GetAll(DogFilter filter);
        Task<DogEntity> Add(DogEntity entity);
    }
}
