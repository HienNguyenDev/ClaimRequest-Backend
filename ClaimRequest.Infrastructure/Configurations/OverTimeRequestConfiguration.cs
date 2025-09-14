using ClaimRequest.Domain.ClaimOverTime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class OverTimeRequestConfiguration : IEntityTypeConfiguration<OverTimeRequest>
{
    public void Configure(EntityTypeBuilder<OverTimeRequest> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id);
        
        builder
            .HasOne(x => x.Approver)
            .WithMany(x => x.ForApproveOverTimeRequests)
            .HasForeignKey(x => x.ApproverId);
        
        builder.HasOne(x => x.Project)
            .WithMany(x => x.OvertimeRequests)
            .HasForeignKey(x => x.ProjectId);
        
        builder
            .HasOne(x => x.CreatedByUser)
            .WithMany(x => x.ForConfirmOverTimeRequests)
            .HasForeignKey(x => x.ProjectManagerId);
        
        builder
            .Property(x => x.CreatedAt);
        
        builder
            .Property(x => x.Status)
            .HasConversion<byte>();
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.HasOne(x => x.Room).WithMany().HasForeignKey(x => x.RoomId).IsRequired();   
        builder.Property(x => x.HasWeekday).IsRequired();
        builder.Property(x => x.HasWeekend).IsRequired();
    }
}