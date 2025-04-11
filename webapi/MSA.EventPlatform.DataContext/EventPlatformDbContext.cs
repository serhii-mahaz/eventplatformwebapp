using Microsoft.EntityFrameworkCore;
using MSA.EventPlatform.Domain.Events;
using MSA.EventPlatform.Domain.Users;

namespace MSA.EventPlatform.DataContext
{
    public class EventPlatformDbContext(DbContextOptions<EventPlatformDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EventItem> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<EventRegistration> EventRegistrations { get; set; }
    }
}
