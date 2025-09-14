using ClaimRequest.Domain.AuditLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;


public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Action).HasColumnType("varchar").IsRequired().HasMaxLength(255);
        builder.Property(e => e.PerformedAt).IsRequired();
        builder.HasOne(u => u.User).WithMany(u => u.AuditLogs).HasForeignKey(f => f.UserId);
        builder.HasOne(u => u.Claim).WithMany(c => c.AuditLogs).HasForeignKey(f => f.ClaimId);
    }
}