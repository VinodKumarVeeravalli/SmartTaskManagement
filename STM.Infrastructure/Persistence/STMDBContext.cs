using Microsoft.EntityFrameworkCore;
using STM.Domain.Entities;


namespace STM.Infrastructure.Persistence
{
    public class STMDBContext : DbContext
    {
        public STMDBContext(DbContextOptions<STMDBContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(STMDBContext).Assembly);
        }

    }
}
