using ClaimRequest.Domain.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class InformToConfiguration : IEntityTypeConfiguration<InformTo>
{
    public void Configure(EntityTypeBuilder<InformTo> builder)
    {
        builder.ToTable("InformTo");

        builder.HasKey(i => new { i.UserId, i.ClaimId });

        builder.HasOne(i => i.Claim)
            .WithMany(c => c.InformTos)
            .HasForeignKey(i => i.ClaimId);

        builder.HasOne(i => i.User)
            .WithMany()
            .HasForeignKey(i => i.UserId);
        
    }
}