using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using STM.Domain.Entities;


namespace STM.Infrastructure.Persistence
{
    public class STMDBContext : IdentityDbContext<ApplicationUser>
    {
        public STMDBContext(DbContextOptions<STMDBContext> options) : base(options)
        {
        }

        //DB SETS
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
