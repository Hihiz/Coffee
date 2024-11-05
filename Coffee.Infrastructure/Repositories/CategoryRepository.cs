using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using Coffee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Infrastructure.Repositories
{
    public class CategoryRepository : IBaseRepository<Category>
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) => _db = db;

        public async Task<List<Category>> GetAllAsync() => await _db.Categories.ToListAsync();

        public Task<Category> GetByIdAsync(int id) => throw new NotImplementedException();

        public void CreateAsync(Category entity) => _db.Categories.Add(entity);

        public void UpdateAsync(Category entity) => throw new NotImplementedException();

        public void Delete(Category entity) => throw new NotImplementedException();

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}