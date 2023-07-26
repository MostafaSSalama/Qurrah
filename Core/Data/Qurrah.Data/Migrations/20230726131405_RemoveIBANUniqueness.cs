using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIBANUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Center_FKIBANFileId",
                table: "Center");

            migrationBuilder.DropIndex(
                name: "IX_Center_IBAN",
                table: "Center");

            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "Center",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FKIBANFileId",
                table: "Center",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FKCenterTypeId",
                table: "Center",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Center_FKIBANFileId",
                table: "Center",
                column: "FKIBANFileId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Center_FKIBANFileId",
                table: "Center");

            migrationBuilder.AlterColumn<string>(
                name: "IBAN",
                table: "Center",
                type: "nvarchar(24)",
                maxLength: 24,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(24)",
                oldMaxLength: 24);

            migrationBuilder.AlterColumn<Guid>(
                name: "FKIBANFileId",
                table: "Center",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "FKCenterTypeId",
                table: "Center",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Center_FKIBANFileId",
                table: "Center",
                column: "FKIBANFileId",
                unique: true,
                filter: "[FKIBANFileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Center_IBAN",
                table: "Center",
                column: "IBAN",
                unique: true,
                filter: "[IBAN] IS NOT NULL");
        }
    }
}
