using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using STM.Domain.Entities;

namespace STM.Infrastructure.Persistence.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        void IEntityTypeConfiguration<Project>.Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects", "dbo");

            builder.HasKey(p => p.ProjectId);

            builder.Property(p => p.ProjectId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.ProjectName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(p => p.ProjectName)
                .IsUnique();
        }
    }
}
