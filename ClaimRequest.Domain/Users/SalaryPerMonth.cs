namespace ClaimRequest.Domain.Users;

public class SalaryPerMonth
{
    public Guid Id { get; set; }
    public DateOnly MonthYear { get; set; }
    public Guid UserId { get; set; }
    public decimal BaseSalary { get; set; }
    public int OvertimeHours { get; set; }
    public decimal SalaryPerOvertimeHour { get; set; }
    public int LateEarlyLeaveCases { get; set; } 
    public int AbnormalCases { get; set; }
    public decimal FinePerLateEarlyCase { get; set; }
    public decimal FinePerAbnormalCase { get; set; }
    public decimal TotalSalary { get; set; }

    public decimal OtherMoney { get; set; }
    public User User { get; set; } = null;
}
