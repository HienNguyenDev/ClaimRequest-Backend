using ClaimRequest.Domain.ClaimOverTime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class OverTimeMemberConfiguration : IEntityTypeConfiguration<OverTimeMember>
{
    public void Configure(EntityTypeBuilder<OverTimeMember> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User).WithMany(x => x.OverTimeMembers);
        builder.HasOne(x => x.Request).WithMany(x => x.OverTimeMembers);
    }
}