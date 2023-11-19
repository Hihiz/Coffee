using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using Coffee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Infrastructure.Repositories
{
    public class EventRepository : IRepository<Event>
    {
        private readonly ApplicationDbContext _db;

        public EventRepository(ApplicationDbContext db) => _db = db;

        public async Task<List<Event>> GetAll() => await _db.Events.OrderByDescending(e => e.CreateDate).ToListAsync();

        public async Task<Event> GetById(int id) => await _db.Events.FirstOrDefaultAsync(p => p.Id == id);

        public void Create(Event entity) => _db.Events.Add(entity);

        public void Update(Event entity) => _db.Update(entity);

        public void Delete(Event entity) => _db.Remove(entity);

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}