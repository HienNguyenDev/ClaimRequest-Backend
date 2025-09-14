
using ClaimRequest.Domain.ClaimOverTime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class OverTimeEffortConfiguration : IEntityTypeConfiguration<OverTimeEffort>
{
    public void Configure(EntityTypeBuilder<OverTimeEffort> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DayHours).IsRequired();
        builder.Property(x => x.NightHours).IsRequired();
        
        builder.HasOne(e => e.OverTimeMember)
            .WithMany(e => e.OverTimeEfforts)
            .HasForeignKey(e => e.OverTimeMemberId);

        builder.HasOne(e => e.OverTimeDate)
            .WithMany(e => e.OverTimeEfforts)
            .HasForeignKey(e => e.OverTimeDateId);
        
        builder.Property(e => e.Status).HasConversion<byte>()
            .IsRequired(); 

        builder.HasIndex(e => new { e.OverTimeMemberId, e.OverTimeDateId })
            .IsUnique();  
        builder.Property(e => e.TaskDescription).IsRequired();
    }
}