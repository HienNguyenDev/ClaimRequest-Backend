using ClaimRequest.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(p => p.Code) 
            .IsUnique();

        builder.Property(p => p.StartDate)
            .IsRequired();

        builder.Property(p => p.EndDate)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnType("text");

        builder.Property(p => p.IsSoftDelete).IsRequired().HasDefaultValue(false);

        builder.Property(p => p.Status)
            .HasConversion<byte>()
            .HasDefaultValue(ProjectStatus.InProgress);

        builder.HasMany(p => p.Users).WithMany(p => p.Projects)
            .UsingEntity<ProjectMember>(
                bd =>
                {
                    bd.HasKey(pm => pm.Id);
                    bd.Property(pm => pm.RoleInProject).HasConversion<byte>().IsRequired();
                }
            );
        
        builder
            .HasOne(p => p.Department)
            .WithMany(d => d.Projects)
            .HasForeignKey(p => p.DepartmentId)
            .IsRequired();
    }

}
