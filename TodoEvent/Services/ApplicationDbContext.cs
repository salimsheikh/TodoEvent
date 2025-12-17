using Microsoft.EntityFrameworkCore;
using TodoEvent.Models;

namespace TodoEvent.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options){}
        public required DbSet<Event> Event { get; set; }
    }
}
