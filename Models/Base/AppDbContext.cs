using Microsoft.EntityFrameworkCore;
using TestTaskDotnet.Models.RequestModels;
using TestTaskDotnet.Models.UserModels;

namespace TestTaskDotnet.Models.Base
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //}

        public DbSet<Request>? Requests { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<RequestHistory>? RequestsHistory { get; set; }
    }
}
