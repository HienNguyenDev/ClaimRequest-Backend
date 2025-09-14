using ClaimRequest.Domain.AuditLogs;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {

        builder.HasKey(c => c.Id);
        
        builder
            .HasOne(c => c.User)
            .WithMany(u => u.Claims)
            .HasForeignKey(p => p.UserId);
        
        builder
            .HasOne(c => c.Reason)
            .WithMany(r => r.Claims)
            .HasForeignKey(c => c.ReasonId);

        builder
            .Property(c => c.OtherReasonText)
            .HasColumnType("text").IsRequired(false);
        
        
        builder
            .Property(c => c.IsSoftDeleted)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder
            .Property(x => x.Status)
            .HasConversion<byte>()
            .IsRequired()
            .HasDefaultValue(ClaimStatus.Draft);
        
        builder
            .HasMany(c => c.ClaimDetails)
            .WithOne(c => c.Claim)
            .HasForeignKey(c => c.ClaimId)
            .IsRequired();
        
        builder
            .HasMany(c => c.AuditLogs)
            .WithOne(a => a.Claim)
            .HasForeignKey(a => a.ClaimId);

        builder
            .HasOne(c => c.Supervisor)
            .WithMany()
            .HasForeignKey(c => c.SupervisorId);
        builder
            .HasOne(c => c.Approver)
            .WithMany()
            .HasForeignKey(c => c.ApproverId);
        builder
            .Property(x => x.StartDate)
            .IsRequired();
        builder
            .Property(x => x.EndDate)
            .IsRequired();

        builder
            .Property(x => x.Partial)
            .HasConversion<byte>()
            .IsRequired();
        
        builder
            .Property(x => x.ExpectApproveDay)
            .IsRequired();
        builder.Property(c => c.ClaimFee).IsRequired(false);
    }
}   