using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.AbnormalCases;
using ClaimRequest.Domain.AttendanceRecords;
using ClaimRequest.Domain.AuditLogs;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.CompanySettings;
using ClaimRequest.Domain.EmailTemplates;
using ClaimRequest.Domain.LateEarlyCases;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.Reasons;
using ClaimRequest.Domain.SitesAndRooms;
using ClaimRequest.Domain.Users;
using ClaimRequest.Infrastructure.Configurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Infrastructure.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
    : DbContext(options), IDbContext
{
    public DbSet<Claim> Claims { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ClaimDetail> ClaimDetails { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }
    public DbSet<ReasonType> ReasonTypes { get; set; }
    public DbSet<Reason> Reasons { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Department> Departments { get; set; }
    
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

    public DbSet<CompanySetting> CompanySettings { get; set; }
    
    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    
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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // When should you publish domain events?
        //
        // 1. BEFORE calling SaveChangesAsync
        //     - domain events are part of the same transaction
        //     - immediate consistency
        // 2. AFTER calling SaveChangesAsync
        //     - domain events are a separate transaction
        //     - eventual consistency
        //     - handlers can fail

        int result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync();

        return result;
    }

    private async Task PublishDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                List<IDomainEvent> domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await publisher.Publish(domainEvent);
        }
    }
}