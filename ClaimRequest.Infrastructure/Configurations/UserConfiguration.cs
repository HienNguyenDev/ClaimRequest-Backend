using ClaimRequest.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.FullName).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.DepartmentId).IsRequired();
        builder.Property(x => x.Role).HasConversion<byte>().IsRequired();
        builder.Property(x=> x.Rank).HasConversion<byte>().IsRequired();
        builder.Property(x => x.IsSoftDelete).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.Status).HasConversion<byte>().IsRequired().HasDefaultValue(UserStatus.Active);
        builder.HasMany(x => x.AuditLogs)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);
        builder.Property(x => x.IsVerified).IsRequired().HasDefaultValue(false);
   
    }
}