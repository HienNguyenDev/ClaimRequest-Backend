using ClaimRequest.Domain.AbnormalCases;
using ClaimRequest.Domain.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class AbnormalCaseConfiguration : IEntityTypeConfiguration<AbnormalCase>
{
    public void Configure(EntityTypeBuilder<AbnormalCase> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.WorkDate)
            .IsRequired();
        builder
            .HasMany(a => a.ClaimDetails)
            .WithOne(c => c.AbnormalCase)
            .HasForeignKey(c => c.AbnormalId)
            .IsRequired(false);

        builder
            .HasOne(a => a.User)
            .WithMany(b => b.AbnormalCases)
            .HasForeignKey(a => a.UserId);
    }
}