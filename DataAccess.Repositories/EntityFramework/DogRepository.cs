using Shared.Filters;
using DataAccess.Entities;
using DataAccess.Contracts.Interfaces;
using DataAccess.Repositories.EntityFramework.Data;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.EntityFramework {
    internal class DogRepository : IDogRepository {
        private readonly ApplicationDbContext _context;

        public DogRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<DogEntity> Add(DogEntity entity) {
            await _context.Dogs.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DogEntity?> GetById(int id) {
            return await _context.Dogs.FindAsync(id);
        }

        public async Task<DogEntity?> GetByName(string name) {
            return await _context.Dogs.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<DogEntity>> GetAll(DogFilter filter) {
            var query = _context.Dogs.AsQueryable();
            query = ApplyFilter(query, filter);
            var result = await GetPaginatedResult(query, filter);
            return result;
        }

        private static IQueryable<DogEntity> ApplyFilter(IQueryable<DogEntity> query, DogFilter filter) {
            switch (filter.AttributeSort) {
                case DogAttributeSort.Name:
                    query = filter.SortOrder == SortOrder.Descending
                        ? query.OrderByDescending(x => x.Name)
                        : query.OrderBy(x => x.Name);
                    break;
                case DogAttributeSort.Color:
                    query = filter.SortOrder == SortOrder.Descending
                        ? query.OrderByDescending(x => x.Color)
                        : query.OrderBy(x => x.Color);
                    break;
                case DogAttributeSort.TailLength:
                    query = filter.SortOrder == SortOrder.Descending
                        ? query.OrderByDescending(x => x.TailLength)
                        : query.OrderBy(x => x.TailLength);
                    break;
                case DogAttributeSort.Weight:
                    query = filter.SortOrder == SortOrder.Descending
                        ? query.OrderByDescending(x => x.Weigth)
                        : query.OrderBy(x => x.Weigth);
                    break;
                case null:
                    query = filter.SortOrder == SortOrder.Descending
                        ? query.OrderByDescending(x => x.Id)
                        : query.OrderBy(x => x.Id);
                    break;
            }
            return query;
        }

        private static async Task<IEnumerable<DogEntity>> GetPaginatedResult(IQueryable<DogEntity> query, DogFilter filter) {
            return await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
        }
    }
}
