using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCenterLicense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CenterLicense_FKFileId",
                table: "CenterLicense");

            migrationBuilder.DropIndex(
                name: "IX_Center_FKIBANFileId",
                table: "Center");

            migrationBuilder.AddColumn<int>(
                name: "FKCenterId",
                table: "CenterLicense",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Center",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FKCreatedByUserId",
                table: "Center",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterLicense_FKCenterId_LicenseNumber",
                table: "CenterLicense",
                columns: new[] { "FKCenterId", "LicenseNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterLicense_FKFileId",
                table: "CenterLicense",
                column: "FKFileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Center_FKIBANFileId",
                table: "Center",
                column: "FKIBANFileId",
                unique: true,
                filter: "[FKIBANFileId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterLicense_Center_FKCenterId",
                table: "CenterLicense",
                column: "FKCenterId",
                principalTable: "Center",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterLicense_Center_FKCenterId",
                table: "CenterLicense");

            migrationBuilder.DropIndex(
                name: "IX_CenterLicense_FKCenterId_LicenseNumber",
                table: "CenterLicense");

            migrationBuilder.DropIndex(
                name: "IX_CenterLicense_FKFileId",
                table: "CenterLicense");

            migrationBuilder.DropIndex(
                name: "IX_Center_FKIBANFileId",
                table: "Center");

            migrationBuilder.DropColumn(
                name: "FKCenterId",
                table: "CenterLicense");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Center",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "FKCreatedByUserId",
                table: "Center",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_CenterLicense_FKFileId",
                table: "CenterLicense",
                column: "FKFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Center_FKIBANFileId",
                table: "Center",
                column: "FKIBANFileId");
        }
    }
}
