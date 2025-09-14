using ClaimRequest.Domain.AbnormalCases;
using ClaimRequest.Domain.Claims;

namespace ClaimRequest.Application.Attendance.GetAbnormalCase;

public class GetAbnormalItem
{
    public DateOnly WorkDate { get; set; }
    public AbnormalType RecordType { get; set; }
    
    public string? ReasonType { get; set; }
    
    public ClaimStatus? Status { get; set; }
    
    public Partial? PartialDay { get; set; }
}