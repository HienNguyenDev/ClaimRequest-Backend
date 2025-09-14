using ClaimRequest.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder
            .HasKey(e => e.Id);
        builder
            .Property(e => e.Name)
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(255);
        builder
            .Property(e => e.Code)
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(255);
        builder
            .Property(e => e.Description)
            .HasColumnType("text");
        builder
            .HasMany(e => e.Users)
            .WithOne(e => e.Departments)
            .HasForeignKey(e => e.DepartmentId);
    }
}