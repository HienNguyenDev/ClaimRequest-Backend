using ClaimRequest.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimRequest.Infrastructure.Configurations;

public class SalaryPerMonthConfiguration : IEntityTypeConfiguration<SalaryPerMonth>
{
    public void Configure(EntityTypeBuilder<SalaryPerMonth> builder)
    {
        builder.ToTable("SalaryPerMonth");
        
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.MonthYear)
            .HasColumnType("date")
            .IsRequired();
        
        builder.Property(s => s.BaseSalary)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(s => s.SalaryPerOvertimeHour)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(s => s.FinePerAbnormalCase)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(s => s.FinePerAbnormalCase)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.Property(s => s.TotalSalary)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.HasOne(s => s.User)
            .WithMany(u => u.SalaryPerMonths)
            .HasForeignKey(s => s.UserId);
        
        builder.Property(s => s.OtherMoney) .HasColumnType("decimal(18,2)")
            .IsRequired();
    }
}
