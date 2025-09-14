using ClaimRequest.Domain.AbnormalCases;
using ClaimRequest.Domain.AttendanceRecords;
using ClaimRequest.Domain.AuditLogs;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.LateEarlyCases;
using ClaimRequest.Domain.Projects;

namespace ClaimRequest.Domain.Users;

public class User : Entity 
{ 
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; } 
    public UserRank Rank { get; set; }
    public decimal BaseSalary { get; set; }
    public byte IsSoftDelete { get; set; }
    public UserStatus Status { get; set; }
    
    public Guid DepartmentId { get; set; }

    public bool IsVerified { get; set; }
    //navigation property
    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<ProjectMember> ProjectMembers { get; set; } = new List<ProjectMember>();
    public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    public virtual Department Departments { get; set; } = null!;
    
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    
    public ICollection<AbnormalCase>  AbnormalCases { get; set; } = new List<AbnormalCase>();
    public ICollection<LateEarlyCase> LateEarlyCases { get; set; } = new List<LateEarlyCase>();
    public ICollection<OverTimeRequest>  ForApproveOverTimeRequests{ get; set; } = new List<OverTimeRequest>();
   
    public ICollection<OverTimeRequest>  ForConfirmOverTimeRequests{ get; set; } = new List<OverTimeRequest>();
    
    public ICollection<OverTimeMember> OverTimeMembers { get; set; } = new List<OverTimeMember>();
    public ICollection<SalaryPerMonth> SalaryPerMonths { get; set; } = new List<SalaryPerMonth>();
}