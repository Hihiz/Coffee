using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using Coffee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Infrastructure.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) => _db = db;

        public async Task<List<Product>> GetAll() => await _db.Products.Include(p => p.Category).ToListAsync();

        public async Task<Product> GetById(int id) => await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        public void Create(Product entity)
        {
            _db.Add(entity);
            _db.Entry(entity).Reference(p => p.Category).Load();
        }

        public void Update(Product entity)
        {
            _db.Update(entity);
            _db.Entry(entity).Reference(p => p.Category).Load();
        }

        public void Delete(Product entity) => _db.Remove(entity);

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
