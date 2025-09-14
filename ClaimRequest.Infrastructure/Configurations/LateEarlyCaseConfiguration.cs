using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.LateEarlyCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class LateEarlyCaseConfiguration : IEntityTypeConfiguration<LateEarlyCase>
{
    public void Configure(EntityTypeBuilder<LateEarlyCase> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.WorkDate)
            .IsRequired();
        builder
            .Property(x => x.CheckInTime)
            .IsRequired();
        builder
            .Property(x => x.CheckoutTime)
            .IsRequired();
        builder
            .Property(x => x.IsEarlyLeave)
            .IsRequired();
        builder
            .Property(x => x.IsLateCome)
            .IsRequired();
        builder
            .Property(x => x.EarlyTimeSpan)
            .IsRequired();
        builder
            .Property(x => x.LateTimeSpan)
            .IsRequired();
        /*builder
            .HasOne(x => x.ClaimDetail)
            .WithOne(x => x.LateEarlyCase)
            .HasForeignKey<ClaimDetail>(x => x.LateEarlyId)
            .IsRequired(false);*/

        builder.HasMany(x => x.ClaimDetails).WithOne(x => x.LateEarlyCase).HasForeignKey(x => x.LateEarlyId);
        builder
            .HasOne(a => a.User)
            .WithMany(b => b.LateEarlyCases)
            .HasForeignKey(a => a.UserId);
        
    }
}