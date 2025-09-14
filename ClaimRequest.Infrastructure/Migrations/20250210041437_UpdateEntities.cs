using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("123e4567-e89b-12d3-a456-426614174000"));

            migrationBuilder.DropColumn(
                name: "Department",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClaimFee",
                schema: "public",
                table: "Claims");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                schema: "public",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "ClaimFee",
                schema: "public",
                table: "ClaimDetails",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "varchar", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                schema: "public",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                schema: "public",
                table: "Users",
                column: "DepartmentId",
                principalSchema: "public",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                schema: "public",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "public",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClaimFee",
                schema: "public",
                table: "ClaimDetails");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                schema: "public",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ClaimFee",
                schema: "public",
                table: "Claims",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                schema: "public",
                table: "Users",
                columns: new[] { "Id", "BaseSalary", "Department", "Email", "FullName", "Password", "Rank", "Role" },
                values: new object[] { new Guid("123e4567-e89b-12d3-a456-426614174000"), 100000m, "Soumaki", "john.doe@gmail.com", "John Doe", "$2a$11$s9Z7z8J7q6X5Y4w3v2Z1Ou1Z3X4Y5W6V7B8C9D0E1F2G3H4I5J6K7L8M9N0O", (byte)4, (byte)1 });
        }
    }
}
