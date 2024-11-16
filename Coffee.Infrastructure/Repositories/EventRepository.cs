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

        public async Task<Event> CreateAsync(Event entity)
        {
            await _db.Events.AddAsync(entity);

            return entity;
        }

        public async Task<Event> UpdateAsync(Event entity)
        {
            _db.Update(entity);

            return entity;
        }

        public async Task Delete(Event entity) => _db.Remove(entity);

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}