using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addnewovertimeentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeRequests_Projects_ProjectId",
                schema: "public",
                table: "OvertimeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeRequests_Users_ApproverId",
                schema: "public",
                table: "OvertimeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_OvertimeRequests_Users_ProjectManagerId",
                schema: "public",
                table: "OvertimeRequests");

            migrationBuilder.DropTable(
                name: "OverTimeEntries",
                schema: "public");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OvertimeRequests",
                schema: "public",
                table: "OvertimeRequests");

            migrationBuilder.DropColumn(
                name: "ForDate",
                schema: "public",
                table: "OvertimeRequests");

            migrationBuilder.RenameTable(
                name: "OvertimeRequests",
                schema: "public",
                newName: "OverTimeRequests",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_OvertimeRequests_ProjectManagerId",
                schema: "public",
                table: "OverTimeRequests",
                newName: "IX_OverTimeRequests_ProjectManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_OvertimeRequests_ProjectId",
                schema: "public",
                table: "OverTimeRequests",
                newName: "IX_OverTimeRequests_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_OvertimeRequests_ApproverId",
                schema: "public",
                table: "OverTimeRequests",
                newName: "IX_OverTimeRequests_ApproverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OverTimeRequests",
                schema: "public",
                table: "OverTimeRequests",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OverTimeDates",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    OverTimeRequestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverTimeDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverTimeDates_OverTimeRequests_OverTimeRequestId",
                        column: x => x.OverTimeRequestId,
                        principalSchema: "public",
                        principalTable: "OverTimeRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OverTimeMembers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverTimeMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverTimeMembers_OverTimeRequests_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "public",
                        principalTable: "OverTimeRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OverTimeMembers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OverTimeEffort",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OverTimeMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    OverTimeDateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Hours = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverTimeEffort", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverTimeEffort_OverTimeDates_OverTimeDateId",
                        column: x => x.OverTimeDateId,
                        principalSchema: "public",
                        principalTable: "OverTimeDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OverTimeEffort_OverTimeMembers_OverTimeMemberId",
                        column: x => x.OverTimeMemberId,
                        principalSchema: "public",
                        principalTable: "OverTimeMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OverTimeDates_OverTimeRequestId",
                schema: "public",
                table: "OverTimeDates",
                column: "OverTimeRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_OverTimeEffort_OverTimeDateId",
                schema: "public",
                table: "OverTimeEffort",
                column: "OverTimeDateId");

            migrationBuilder.CreateIndex(
                name: "IX_OverTimeEffort_OverTimeMemberId_OverTimeDateId",
                schema: "public",
                table: "OverTimeEffort",
                columns: new[] { "OverTimeMemberId", "OverTimeDateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OverTimeMembers_RequestId",
                schema: "public",
                table: "OverTimeMembers",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_OverTimeMembers_UserId",
                schema: "public",
                table: "OverTimeMembers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OverTimeRequests_Projects_ProjectId",
                schema: "public",
                table: "OverTimeRequests",
                column: "ProjectId",
                principalSchema: "public",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OverTimeRequests_Users_ApproverId",
                schema: "public",
                table: "OverTimeRequests",
                column: "ApproverId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OverTimeRequests_Users_ProjectManagerId",
                schema: "public",
                table: "OverTimeRequests",
                column: "ProjectManagerId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OverTimeRequests_Projects_ProjectId",
                schema: "public",
                table: "OverTimeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_OverTimeRequests_Users_ApproverId",
                schema: "public",
                table: "OverTimeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_OverTimeRequests_Users_ProjectManagerId",
                schema: "public",
                table: "OverTimeRequests");

            migrationBuilder.DropTable(
                name: "OverTimeEffort",
                schema: "public");

            migrationBuilder.DropTable(
                name: "OverTimeDates",
                schema: "public");

            migrationBuilder.DropTable(
                name: "OverTimeMembers",
                schema: "public");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OverTimeRequests",
                schema: "public",
                table: "OverTimeRequests");

            migrationBuilder.RenameTable(
                name: "OverTimeRequests",
                schema: "public",
                newName: "OvertimeRequests",
                newSchema: "public");

            migrationBuilder.RenameIndex(
                name: "IX_OverTimeRequests_ProjectManagerId",
                schema: "public",
                table: "OvertimeRequests",
                newName: "IX_OvertimeRequests_ProjectManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_OverTimeRequests_ProjectId",
                schema: "public",
                table: "OvertimeRequests",
                newName: "IX_OvertimeRequests_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_OverTimeRequests_ApproverId",
                schema: "public",
                table: "OvertimeRequests",
                newName: "IX_OvertimeRequests_ApproverId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ForDate",
                schema: "public",
                table: "OvertimeRequests",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OvertimeRequests",
                schema: "public",
                table: "OvertimeRequests",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OverTimeEntries",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Effort = table.Column<int>(type: "integer", nullable: false),
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

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeRequests_Projects_ProjectId",
                schema: "public",
                table: "OvertimeRequests",
                column: "ProjectId",
                principalSchema: "public",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeRequests_Users_ApproverId",
                schema: "public",
                table: "OvertimeRequests",
                column: "ApproverId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OvertimeRequests_Users_ProjectManagerId",
                schema: "public",
                table: "OvertimeRequests",
                column: "ProjectManagerId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
