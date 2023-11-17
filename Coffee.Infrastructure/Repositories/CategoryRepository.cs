using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using Coffee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Infrastructure.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) => _db = db;

        public async Task<List<Category>> GetAll() => await _db.Categories.ToListAsync();

        public Task<Category> GetById(int id) => throw new NotImplementedException();

        public void Create(Category entity) => _db.Categories.Add(entity);

        public void Update(Category entity) => throw new NotImplementedException();

        public void Delete(Category entity) => throw new NotImplementedException();

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}