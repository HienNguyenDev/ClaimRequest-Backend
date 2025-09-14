using ClaimRequest.Domain.AttendanceRecords;
using ClaimRequest.Domain.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;



public class AttendanceRecordConfiguration : IEntityTypeConfiguration<AttendanceRecord>
{
    public void Configure(EntityTypeBuilder<AttendanceRecord> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.User)
            .WithMany(e => e.AttendanceRecords)
            .HasForeignKey(e => e.UserId);

        builder.Property(e => e.CheckInTime).IsRequired(false);
        
        builder.Property(e => e.WorkDate)
            .IsRequired();

        builder.Property(e => e.CheckOutTime).IsRequired(false);

        /*builder
            .Property(e => e.WorkStatus)
            .HasConversion<byte>()
            .IsRequired(false);*/
        builder
            .Property(e => e.IsLateCome);
        builder
            .Property(e => e.IsLeaveEarly);
        
        /*builder
            .HasOne(e => e.ClaimDetail)
            .WithOne(e => e.AttendanceRecord)
            .HasForeignKey<ClaimDetail>(e => e.AttendanceId);*/
    }
}