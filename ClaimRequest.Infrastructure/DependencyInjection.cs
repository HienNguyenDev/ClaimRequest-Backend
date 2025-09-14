using System.Text;
using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.ClaimOverTimes.Redis;
using ClaimRequest.Application.Claims.Redis;
using ClaimRequest.Infrastructure.Authentication;
using ClaimRequest.Infrastructure.Database;
using ClaimRequest.Infrastructure.Services;
using ClaimRequest.Infrastructure.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using StackExchange.Redis;

namespace ClaimRequest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddDatabase(configuration)
            .AddHealthChecks(configuration)
            .AddAuthenticationInternal(configuration)
            .AddQuartzService(configuration)
            .AddMailService(configuration)
            .AddClientUrl(configuration)
            .AddRedis(configuration);



    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
                );

        services.AddScoped<IDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Database")!);

        return services;
    }

    private static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenProvider, TokenProvider>();

        return services;
    }

    private static IServiceCollection AddQuartzService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            var createAttendanceJob = JobKey.Create(nameof(CreateAttendanceRecordJob));
            options
                .AddJob<CreateAttendanceRecordJob>(createAttendanceJob)
                .AddTrigger(trigger => trigger
                    .ForJob(createAttendanceJob)
                    .WithCronSchedule(configuration["Quartz:Schedules:CreateAttendanceRecordJob"]!, cronScheduleBuilder => cronScheduleBuilder.InTimeZone(TimeZoneInfo.Utc)));


            var scanAbnormalCaseJob = JobKey.Create(nameof(ScanAbnormalCaseJob));
            options
                .AddJob<ScanAbnormalCaseJob>(scanAbnormalCaseJob)
                .AddTrigger(trigger => trigger
                    .ForJob(scanAbnormalCaseJob)
                    .WithCronSchedule(configuration["Quartz:Schedules:ScanAbnormalCaseJob"]!, cronScheduleBuilder => cronScheduleBuilder.InTimeZone(TimeZoneInfo.Utc)));

            var scanLateEarlyJob = JobKey.Create(nameof(ScanLateEarlyJob));
            options
                .AddJob<ScanLateEarlyJob>(scanLateEarlyJob)
                .AddTrigger(trigger => trigger
                    .ForJob(scanLateEarlyJob)
                    .WithCronSchedule(configuration["Quartz:Schedules:ScanLateEarlyCaseJob"]!, cronScheduleBuilder => cronScheduleBuilder.InTimeZone(TimeZoneInfo.Utc)));
            
            var createSalaryRecordJob = JobKey.Create(nameof(CreateSalaryRecordJob));
            options
                .AddJob<CreateSalaryRecordJob>(createSalaryRecordJob)
                .AddTrigger(trigger => trigger
                    .ForJob(createSalaryRecordJob)
                    .WithCronSchedule(configuration["Quartz:Schedules:CreateSalaryRecordJob"]!, cronScheduleBuilder => cronScheduleBuilder.InTimeZone(TimeZoneInfo.Utc)));
            
            var finalizeSalaryJob = JobKey.Create(nameof(FinalizeSalaryJob));
            options
                .AddJob<FinalizeSalaryJob>(finalizeSalaryJob)
                .AddTrigger(trigger => trigger
                    .ForJob(finalizeSalaryJob)
                    .WithCronSchedule(configuration["Quartz:Schedules:FinalizeSalaryJob"]!, cronScheduleBuilder => cronScheduleBuilder.InTimeZone(TimeZoneInfo.Utc)));
            
            var sendClaimCreatedEmailJob = JobKey.Create(nameof(SendClaimCreateEmailJob));
            options
                .AddJob<SendClaimCreateEmailJob>(sendClaimCreatedEmailJob)
                .AddTrigger(trigger => trigger
                    .ForJob(sendClaimCreatedEmailJob)
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(60)))
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever())
                );
            
            var sendClaimStatusChangedEmailJob = JobKey.Create(nameof(SendClaimStatusChangedEmailJob));
            options
                .AddJob<SendClaimStatusChangedEmailJob>(sendClaimStatusChangedEmailJob)
                .AddTrigger(trigger => trigger
                    .ForJob(sendClaimStatusChangedEmailJob)
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(60)))
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever())
                    );
            
            
            
            var sendOverTimeRequestCreatedEmailJob = JobKey.Create(nameof(SendOverTimeRequestCreatedEmailJob));
            options
                .AddJob<SendOverTimeRequestCreatedEmailJob>(sendOverTimeRequestCreatedEmailJob)
                .AddTrigger(trigger => trigger
                    .ForJob(sendOverTimeRequestCreatedEmailJob)
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(60)))
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever())
                );
            
            var sendOverTimeRequestApprovedEmailJob = JobKey.Create(nameof(SendOverTimeRequestApprovedEmailJob));
            options
                .AddJob<SendOverTimeRequestApprovedEmailJob>(sendOverTimeRequestApprovedEmailJob)
                .AddTrigger(trigger => trigger
                    .ForJob(sendOverTimeRequestApprovedEmailJob)
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(60)))
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever())
                );
            
            var sendOverTimeEffortCreatedEmailJob = JobKey.Create(nameof(SendOverTimeEffortCreatedEmailJob));
            options
                .AddJob<SendOverTimeEffortCreatedEmailJob>(sendOverTimeEffortCreatedEmailJob)
                .AddTrigger(trigger => trigger
                    .ForJob(sendOverTimeEffortCreatedEmailJob)
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(60)))
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever())
                );
            
            var sendOverTimeEffortStatusChangedEmailJob = JobKey.Create(nameof(SendOverTimeEffortStatusChangedEmailJob));
            options
                .AddJob<SendOverTimeEffortStatusChangedEmailJob>(sendOverTimeEffortStatusChangedEmailJob)
                .AddTrigger(trigger => trigger
                    .ForJob(sendOverTimeEffortStatusChangedEmailJob)
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(60)))
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever())
                );
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        return services;
    }

    private static IServiceCollection AddMailService(this IServiceCollection services,
        IConfiguration configuration)
    {
        var mailSettings = configuration.GetSection(nameof(MailSettings)).Get<MailSettings>();

        services.Configure<MailSettings>(options =>
        {
            options.SmtpServer = mailSettings!.SmtpServer;
            options.SmtpPort = mailSettings!.SmtpPort;
            options.SmtpUsername = mailSettings!.SmtpUsername;
            options.SmtpPassword = mailSettings!.SmtpPassword;
        });

        services.AddTransient<IMailService, MailService>();
        return services;
    }

    private static IServiceCollection AddClientUrl(this IServiceCollection services,
     IConfiguration configuration)
    {
        services.Configure<ClientSettings>(configuration.GetSection(nameof(ClientSettings)));
        return services;
    }

    private static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("Redis") ?? "localhost:6379";
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
        services.AddSingleton<IDatabase>(sp => 
            sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
        services.AddScoped<ClaimEventPublisher, ClaimEventPublisher>();
        services.AddSingleton<ClaimCreatedEventConsumer>();
        services.AddSingleton<ClaimStatusChangeEventConsumer>();
        services.AddScoped<OvertimeEventPublisher, OvertimeEventPublisher>();
        services.AddSingleton<OverTimeRequestCreatedEventConsumer>();
        services.AddSingleton<OverTimeRequestApprovedEventConsumer>();
        services.AddSingleton<OverTimeEffortCreatedEventConsumer>();
        services.AddSingleton<OverTimeEffortStatusChangedEventConsumer>();
        return services;
    }
}