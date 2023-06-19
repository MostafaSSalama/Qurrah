using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesAndAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "ParentUser",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(9)",
                oldMaxLength: 9);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15d21182-c2c0-45fa-99f3-d137c021675d", null, "Administrator", "Administrator" },
                    { "6aa46d74-1e43-4c02-8998-1df4b32ded1a", null, "Parent", "Parent" },
                    { "ba176f18-9f5a-4640-b055-b93aba912e9f", null, "Center", "Center" },
                    { "eeec8e40-b4e8-4641-80e7-a455872f4ee1", null, "CenterApprover", "CenterApprover" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FKCreatedByUserId", "FKUserTypeId", "LastModifiedOn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "31d8fe38-8f0f-444b-b447-5b73eaaf6863", 0, "203d3e4c-f8ee-4256-a6ab-e06a3ccae92e", new DateTime(2023, 6, 13, 14, 4, 14, 434, DateTimeKind.Local).AddTicks(8770), "Admin@Qurrah.com", false, null, (byte)1, null, false, null, "Admin@Qurrah.com", "Admin", "AQAAAAIAAYagAAAAEAjKV+3qUrGUsrAs4kwFhMWWeEBFXnFHpMif2wJerb77Xg6Okp/HWAI7DFvaIcHb3Q==", "0543700744", false, "aea583e8-f40b-4081-806f-861d3452bc38", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "15d21182-c2c0-45fa-99f3-d137c021675d", "31d8fe38-8f0f-444b-b447-5b73eaaf6863" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6aa46d74-1e43-4c02-8998-1df4b32ded1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba176f18-9f5a-4640-b055-b93aba912e9f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eeec8e40-b4e8-4641-80e7-a455872f4ee1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "15d21182-c2c0-45fa-99f3-d137c021675d", "31d8fe38-8f0f-444b-b447-5b73eaaf6863" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15d21182-c2c0-45fa-99f3-d137c021675d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "31d8fe38-8f0f-444b-b447-5b73eaaf6863");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "ParentUser",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }
    }
}
