using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCenterLicenseCreatedByField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FKCreatedByUserId",
                table: "CenterLicense",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CenterLicense_FKCreatedByUserId",
                table: "CenterLicense",
                column: "FKCreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterLicense_AspNetUsers_FKCreatedByUserId",
                table: "CenterLicense",
                column: "FKCreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterLicense_AspNetUsers_FKCreatedByUserId",
                table: "CenterLicense");

            migrationBuilder.DropIndex(
                name: "IX_CenterLicense_FKCreatedByUserId",
                table: "CenterLicense");

            migrationBuilder.DropColumn(
                name: "FKCreatedByUserId",
                table: "CenterLicense");

        }
    }
}
