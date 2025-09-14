using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClaimRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_ApproverId1",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_SuperivisorId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_SuperivisorId1",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_ApproverId1",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropIndex(
                name: "IX_Claims_SuperivisorId1",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "ApproverId1",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "SuperivisorId1",
                schema: "public",
                table: "Claims");

            migrationBuilder.AlterColumn<Guid>(
                name: "SuperivisorId",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                schema: "public",
                table: "Claims",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldDefaultValue: (byte)1);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                schema: "public",
                table: "Claims",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "OtherReasonText",
                schema: "public",
                table: "Claims",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Token",
                schema: "public",
                table: "RefreshToken",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                schema: "public",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_SuperivisorId",
                schema: "public",
                table: "Claims",
                column: "SuperivisorId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Claims_Users_SuperivisorId",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "public");

            migrationBuilder.AlterColumn<Guid>(
                name: "SuperivisorId",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                schema: "public",
                table: "Claims",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)1,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldDefaultValue: (byte)0);

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                schema: "public",
                table: "Claims",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OtherReasonText",
                schema: "public",
                table: "Claims",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApproverId1",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SuperivisorId1",
                schema: "public",
                table: "Claims",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ApproverId1",
                schema: "public",
                table: "Claims",
                column: "ApproverId1");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_SuperivisorId1",
                schema: "public",
                table: "Claims",
                column: "SuperivisorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_ApproverId1",
                schema: "public",
                table: "Claims",
                column: "ApproverId1",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_SuperivisorId",
                schema: "public",
                table: "Claims",
                column: "SuperivisorId",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Claims_Users_SuperivisorId1",
                schema: "public",
                table: "Claims",
                column: "SuperivisorId1",
                principalSchema: "public",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
