using ClaimRequest.Domain.Reasons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class ReasonTypeConfiguration : IEntityTypeConfiguration<ReasonType>
{
    public void Configure(EntityTypeBuilder<ReasonType> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder
            .Property(x => x.Name)
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(255);
        
        builder
            .Property(x => x.IsSoftDeleted)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder
            .HasMany(x => x.Reasons)
            .WithOne(x => x.ReasonType)
            .HasForeignKey(x => x.RequestTypeId);    
    }
}