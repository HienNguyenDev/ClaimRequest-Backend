using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "CompanySetting",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LimitDayOff = table.Column<int>(type: "integer", nullable: false, defaultValue: 12),
                    WorkStartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    WorkEndTime = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsSoftDelete = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)0),
                    Status = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReasonTypes",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    IsSoftDeleted = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<byte>(type: "smallint", nullable: false),
                    Rank = table.Column<byte>(type: "smallint", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "numeric", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: false),
                    IsSoftDelete = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)0),
                    Status = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)2)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reasons",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    IsOther = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsSoftDeleted = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reasons_ReasonTypes_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalSchema: "public",
                        principalTable: "ReasonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceRecord",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CheckInTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CheckOutTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WorkDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkStatus = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceRecord_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMembers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleInProject = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMembers_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "public",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMembers_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    StaffDepartment = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    RoleInProject = table.Column<byte>(type: "smallint", nullable: false),
                    ReasonId = table.Column<Guid>(type: "uuid", nullable: false),
                    OtherReasonText = table.Column<string>(type: "text", nullable: false),
                    Remark = table.Column<string>(type: "text", nullable: false),
                    ClaimFee = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)1),
                    IsSoftDeleted = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claims_Reasons_ReasonId",
                        column: x => x.ReasonId,
                        principalSchema: "public",
                        principalTable: "Reasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimId = table.Column<Guid>(type: "uuid", nullable: false),
                    Action = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PerformedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalSchema: "public",
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClaimDetails",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimID = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalHours = table.Column<decimal>(type: "numeric", nullable: false),
                    Remarks = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimDetails_Claims_ClaimID",
                        column: x => x.ClaimID,
                        principalSchema: "public",
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "Users",
                columns: new[] { "Id", "BaseSalary", "Department", "Email", "FullName", "Password", "Rank", "Role" },
                values: new object[] { new Guid("123e4567-e89b-12d3-a456-426614174000"), 100000m, "Soumaki", "john.doe@gmail.com", "John Doe", "$2a$11$s9Z7z8J7q6X5Y4w3v2Z1Ou1Z3X4Y5W6V7B8C9D0E1F2G3H4I5J6K7L8M9N0O", (byte)4, (byte)4 });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecord_UserId",
                schema: "public",
                table: "AttendanceRecord",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ClaimId",
                schema: "public",
                table: "AuditLogs",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                schema: "public",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimDetails_ClaimID",
                schema: "public",
                table: "ClaimDetails",
                column: "ClaimID");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ProjectId",
                schema: "public",
                table: "Claims",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ReasonId",
                schema: "public",
                table: "Claims",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_UserId",
                schema: "public",
                table: "Claims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_ProjectID",
                schema: "public",
                table: "ProjectMembers",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMembers_UserID",
                schema: "public",
                table: "ProjectMembers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Code",
                schema: "public",
                table: "Projects",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reasons_RequestTypeId",
                schema: "public",
                table: "Reasons",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "public",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceRecord",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AuditLogs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ClaimDetails",
                schema: "public");

            migrationBuilder.DropTable(
                name: "CompanySetting",
                schema: "public");

            migrationBuilder.DropTable(
                name: "EmailTemplate",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ProjectMembers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Claims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Reasons",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ReasonTypes",
                schema: "public");
        }
    }
}
