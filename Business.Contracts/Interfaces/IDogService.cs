using Shared.Filters;
using Business.Contracts.Dto;
using Business.Contracts.Requests;

namespace Business.Contracts.Interfaces {
    public interface IDogService {
        Task<DogDto> Get(int id);
        Task<IEnumerable<DogDto>> GetAll(DogFilter filter);
        Task<DogDto> Add(DogAddRequest request);
    }
}
