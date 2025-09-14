using ClaimRequest.Domain.CompanySettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;


public class CompanySettingConfiguration : IEntityTypeConfiguration<CompanySetting>
{
    public void Configure(EntityTypeBuilder<CompanySetting> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.LimitDayOff)
            .HasDefaultValue(12)
            .IsRequired();

        builder.Property(e => e.WorkStartTime)
            .IsRequired();

        builder
            .Property(e => e.WorkEndTime)
            .IsRequired();
    }
}