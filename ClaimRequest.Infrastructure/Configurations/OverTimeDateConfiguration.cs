using ClaimRequest.Domain.ClaimOverTime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class OverTimeDateConfiguration : IEntityTypeConfiguration<OverTimeDate>
{
    public void Configure(EntityTypeBuilder<OverTimeDate> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Date).IsRequired();
        builder
            .HasOne(x => x.OverTimeRequest)
            .WithMany(x => x.OverTimeDates).HasForeignKey(x => x.OverTimeRequestId)
            .IsRequired();
    }
}