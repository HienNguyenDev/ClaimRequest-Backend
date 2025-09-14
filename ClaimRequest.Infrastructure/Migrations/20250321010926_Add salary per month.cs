using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addsalarypermonth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinePerAbnormalCase",
                schema: "public",
                table: "CompanySettings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FinePerLateEarlyCase",
                schema: "public",
                table: "CompanySettings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalaryPerOvertimeHour",
                schema: "public",
                table: "CompanySettings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "SalaryPerMonth",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MonthYear = table.Column<DateOnly>(type: "date", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    OvertimeHours = table.Column<int>(type: "integer", nullable: false),
                    SalaryPerOvertimeHour = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    LateEarlyLeaveCases = table.Column<int>(type: "integer", nullable: false),
                    AbnormalCases = table.Column<int>(type: "integer", nullable: false),
                    FinePerLateEarlyCase = table.Column<decimal>(type: "numeric", nullable: false),
                    FinePerAbnormalCase = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalSalary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPerMonth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryPerMonth_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaryPerMonth_Users_UserId1",
                        column: x => x.UserId1,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPerMonth_UserId",
                schema: "public",
                table: "SalaryPerMonth",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPerMonth_UserId1",
                schema: "public",
                table: "SalaryPerMonth",
                column: "UserId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalaryPerMonth",
                schema: "public");

            migrationBuilder.DropColumn(
                name: "FinePerAbnormalCase",
                schema: "public",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "FinePerLateEarlyCase",
                schema: "public",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "SalaryPerOvertimeHour",
                schema: "public",
                table: "CompanySettings");
        }
    }
}
