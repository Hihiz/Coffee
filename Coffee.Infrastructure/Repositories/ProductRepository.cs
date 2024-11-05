using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using Coffee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Infrastructure.Repositories
{
    public class ProductRepository : IBaseRepository<Product>
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) => _db = db;

        public async Task<List<Product>> GetAllAsync() => await _db.Products.Include(p => p.Category).ToListAsync();

        public async Task<Product> GetByIdAsync(int id) => await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        public void CreateAsync(Product entity)
        {
            _db.Add(entity);
            _db.Entry(entity).Reference(p => p.Category).Load();
        }

        public void UpdateAsync(Product entity)
        {
            _db.Update(entity);
            _db.Entry(entity).Reference(p => p.Category).Load();
        }

        public void Delete(Product entity) => _db.Remove(entity);

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
