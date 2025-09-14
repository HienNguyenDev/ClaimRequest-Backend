using ClaimRequest.Domain.Reasons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;



public class ReasonConfiguration : IEntityTypeConfiguration<Reason>
{
    public void Configure(EntityTypeBuilder<Reason> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .Property(x => x.Name)
            .HasColumnType("varchar")
            .IsRequired()
            .HasMaxLength(255);
        
        builder
            .Property(x => x.IsOther)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder
            .Property(x => x.IsSoftDeleted)
            .IsRequired()
            .HasDefaultValue(false);
    }
}