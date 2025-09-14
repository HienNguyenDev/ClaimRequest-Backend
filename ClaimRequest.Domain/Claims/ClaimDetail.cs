using ClaimRequest.Domain.AbnormalCases;
using ClaimRequest.Domain.AttendanceRecords;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.LateEarlyCases;

namespace ClaimRequest.Domain.Claims;

public class ClaimDetail : Entity
{
    public Guid Id { get; set; }
    public Guid ClaimId { get; set; } 
    public DateOnly Date { get; set; }
    /*public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal TotalHours { get; set; }
    public string Remarks { get; set; } = null!;
    public decimal ClaimFee { get; set; }*/
    /*
    public Guid? AttendanceId { get; set; }
    */
    public Guid? LateEarlyId { get; set; }
    public Guid? AbnormalId { get; set; }

    //navigation property
    public virtual Claim Claim { get; set; } = null!;
    public virtual AbnormalCase? AbnormalCase { get; set; }
    public virtual LateEarlyCase? LateEarlyCase { get; set; }
    /*
    public AttendanceRecord? AttendanceRecord { get; set; }
    */

}