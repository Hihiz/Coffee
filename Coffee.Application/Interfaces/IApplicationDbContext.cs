using Coffee.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
