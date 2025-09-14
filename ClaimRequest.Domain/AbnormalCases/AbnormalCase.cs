using ClaimRequest.Domain.AttendanceRecords;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.AbnormalCases;

public class AbnormalCase
{
    public Guid Id { get; set; }
    public DateOnly WorkDate { get; set; } 
    
    public Guid UserId { get; set; }
    
    public AbnormalType AbnormalType { get; set; }
    //navigation property
    /*
    public ClaimDetail? ClaimDetail { get; set; }
    */
    
    public ICollection<ClaimDetail> ClaimDetails { get; set; } = new List<ClaimDetail>();
    
    public User User { get; set; }
}