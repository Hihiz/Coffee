using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using Coffee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Infrastructure.Repositories
{
    public class EventRepository : IBaseRepository<Event>
    {
        private readonly ApplicationDbContext _db;

        public EventRepository(ApplicationDbContext db) => _db = db;

        public async Task<List<Event>> GetAllAsync() => await _db.Events.OrderByDescending(e => e.CreateDate).ToListAsync();

        public async Task<Event> GetByIdAsync(int id) => await _db.Events.FirstOrDefaultAsync(p => p.Id == id);

        public void CreateAsync(Event entity) => _db.Events.Add(entity);

        public void UpdateAsync(Event entity) => _db.Update(entity);

        public void Delete(Event entity) => _db.Remove(entity);

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}