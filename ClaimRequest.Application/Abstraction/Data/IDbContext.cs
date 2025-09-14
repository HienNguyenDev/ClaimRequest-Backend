using ClaimRequest.Domain.AbnormalCases;
using ClaimRequest.Domain.AttendanceRecords;
using ClaimRequest.Domain.AuditLogs;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.CompanySettings;
using ClaimRequest.Domain.EmailTemplates;
using ClaimRequest.Domain.LateEarlyCases;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.Reasons;
using ClaimRequest.Domain.SitesAndRooms;
using ClaimRequest.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Abstraction.Data;

public interface IDbContext
{
    DbSet<Claim> Claims { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<ClaimDetail> ClaimDetails { get; set; }
    DbSet<Project> Projects { get; set; }
    DbSet<ProjectMember> ProjectMembers { get; set; }
    DbSet<ReasonType> ReasonTypes { get; set; }
    DbSet<Reason> Reasons { get; set; }
    DbSet<AuditLog> AuditLogs { get; set; }
    
    DbSet<Department> Departments { get; set; }
    DbSet<EmailTemplate> EmailTemplates { get; set; }
    
    DbSet<RefreshToken> RefreshTokens { get; set; }
    
    DbSet<AttendanceRecord> AttendanceRecords { get; set; }
    
    DbSet<CompanySetting> CompanySettings { get; set; }
    
    public DbSet<AbnormalCase> AbnormalCases{ get; set; }
    
    public DbSet<LateEarlyCase> LateEarlyCases { get; set; }
    
    public DbSet<InformTo> InformTos { get; set; }
    
    public DbSet<OverTimeRequest> OverTimeRequests { get; set; }
    
    public DbSet<OverTimeMember> OverTimeMembers { get; set; }
    
    public DbSet<OverTimeDate> OverTimeDates { get; set; }
    public DbSet<OverTimeEffort> OverTimeEffort { get; set; }
    
    public DbSet<Site> Sites { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<SalaryPerMonth> Salaries { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}