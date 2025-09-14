using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addentityovertime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OvertimeRequests",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectManagerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ForDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<byte>(type: "smallint", nullable: false),
                    ApproverId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OvertimeRequests_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "public",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OvertimeRequests_Users_ApproverId",
                        column: x => x.ApproverId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OvertimeRequests_Users_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OverTimeEntries",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    Effort = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverTimeEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverTimeEntries_OvertimeRequests_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "public",
                        principalTable: "OvertimeRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OverTimeEntries_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OverTimeEntries_RequestId",
                schema: "public",
                table: "OverTimeEntries",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_OverTimeEntries_UserId",
                schema: "public",
                table: "OverTimeEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeRequests_ApproverId",
                schema: "public",
                table: "OvertimeRequests",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeRequests_ProjectId",
                schema: "public",
                table: "OvertimeRequests",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeRequests_ProjectManagerId",
                schema: "public",
                table: "OvertimeRequests",
                column: "ProjectManagerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OverTimeEntries",
                schema: "public");

            migrationBuilder.DropTable(
                name: "OvertimeRequests",
                schema: "public");
        }
    }
}
