using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContentTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c804c96-cec1-45d3-8c01-8cbc7518160f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e48b7b57-fd23-4a0f-8a9d-1353b8f6172c");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "aed4df74-b27f-4530-9442-7fd93b9b330c", "8a7a2253-8dbc-4817-afa6-1256b0ebea93" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0b42f4ff-8f8f-42b8-a3c6-3c8bcf827edd", "a8d8a368-1f07-4a6c-a2ed-c993cef0776f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b42f4ff-8f8f-42b8-a3c6-3c8bcf827edd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aed4df74-b27f-4530-9442-7fd93b9b330c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8a7a2253-8dbc-4817-afa6-1256b0ebea93");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a8d8a368-1f07-4a6c-a2ed-c993cef0776f");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "FileType",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "FileDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1eabda33-4045-48d1-82e5-c18f54b9bdc4", null, "Center", "Center" },
                    { "1f18dd05-4270-4150-81cb-00b355bf0881", null, "Parent", "Parent" },
                    { "4bf454c1-03e0-4e5e-a4b0-e06ce32e2445", null, "Administrator", "Administrator" },
                    { "f7032409-a2ed-4c29-8fa9-c25dc0560275", null, "CenterApprover", "CenterApprover" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FKCreatedByUserId", "FKUserTypeId", "LastModifiedOn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "424d29d3-706b-4f36-80fe-21ad3b1932ac", 0, "a69d2e60-b91f-4f14-9bdd-ccf1e573d79e", new DateTime(2023, 7, 18, 14, 25, 36, 478, DateTimeKind.Local).AddTicks(7595), "CenterReviewer@Qurrah.com", false, null, 2, null, false, null, "CenterReviewer@Qurrah.com", "CenterReviewer", "AQAAAAIAAYagAAAAEKxDrjTpus0G+A6dyF7PvtGIugkOvP4OMjVi85MhoXSbXvoSkhWCK6zouoaLJaFHEg==", "0543700745", false, "cdfc08e2-d42d-46e7-b174-b1500c38683e", false, "CenterReviewer" },
                    { "c5bceb8a-df1a-4f2e-a460-08d84415ec0c", 0, "15de08ea-439c-4cea-83c5-3e8952bfa87a", new DateTime(2023, 7, 18, 14, 25, 36, 354, DateTimeKind.Local).AddTicks(6745), "Admin@Qurrah.com", false, null, 1, null, false, null, "Admin@Qurrah.com", "Admin", "AQAAAAIAAYagAAAAENri6psikN9pFktkiMLWItA2wmcfEHdJhuTEzRA/Y205ho5Thdb2SpZnK0iDDpu9Sw==", "0543700744", false, "92eef05a-d2dd-4831-8d8b-b9716313da86", false, "Admin" }
                });

            migrationBuilder.UpdateData(
                table: "FileType",
                keyColumn: "Id",
                keyValue: 1,
                column: "ContentType",
                value: "application/pdf");

            migrationBuilder.UpdateData(
                table: "FileType",
                keyColumn: "Id",
                keyValue: 2,
                column: "ContentType",
                value: "application/vnd.openxmlformats-officedocument.wordprocessingml.document");

            migrationBuilder.InsertData(
                table: "FileType",
                columns: new[] { "Id", "ContentType", "Name" },
                values: new object[,]
                {
                    { 3, "application/msword", "DOC" },
                    { 4, "image/x-png", "PNG" },
                    { 5, "image/jpeg", "JPG" },
                    { 6, "image/jpeg", "JPEG" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "f7032409-a2ed-4c29-8fa9-c25dc0560275", "424d29d3-706b-4f36-80fe-21ad3b1932ac" },
                    { "4bf454c1-03e0-4e5e-a4b0-e06ce32e2445", "c5bceb8a-df1a-4f2e-a460-08d84415ec0c" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1eabda33-4045-48d1-82e5-c18f54b9bdc4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f18dd05-4270-4150-81cb-00b355bf0881");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f7032409-a2ed-4c29-8fa9-c25dc0560275", "424d29d3-706b-4f36-80fe-21ad3b1932ac" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4bf454c1-03e0-4e5e-a4b0-e06ce32e2445", "c5bceb8a-df1a-4f2e-a460-08d84415ec0c" });

            migrationBuilder.DeleteData(
                table: "FileType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FileType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FileType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FileType",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4bf454c1-03e0-4e5e-a4b0-e06ce32e2445");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7032409-a2ed-4c29-8fa9-c25dc0560275");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "424d29d3-706b-4f36-80fe-21ad3b1932ac");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c5bceb8a-df1a-4f2e-a460-08d84415ec0c");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "FileType");

            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "FileDetails");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b42f4ff-8f8f-42b8-a3c6-3c8bcf827edd", null, "CenterApprover", "CenterApprover" },
                    { "0c804c96-cec1-45d3-8c01-8cbc7518160f", null, "Center", "Center" },
                    { "aed4df74-b27f-4530-9442-7fd93b9b330c", null, "Administrator", "Administrator" },
                    { "e48b7b57-fd23-4a0f-8a9d-1353b8f6172c", null, "Parent", "Parent" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FKCreatedByUserId", "FKUserTypeId", "LastModifiedOn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8a7a2253-8dbc-4817-afa6-1256b0ebea93", 0, "8124443f-8c17-495e-8dfa-c6a6ba4a489c", new DateTime(2023, 7, 17, 17, 27, 14, 66, DateTimeKind.Local).AddTicks(4881), "Admin@Qurrah.com", false, null, 1, null, false, null, "Admin@Qurrah.com", "Admin", "AQAAAAIAAYagAAAAEKvLv1wE3Bc2Hos8C4NdEkb1vEvw+qkeSo6Zb+2MH/RxHW8TjhPEABrn0+CrB7lrLA==", "0543700744", false, "8541736b-7042-4066-8668-a182d47be8e8", false, "Admin" },
                    { "a8d8a368-1f07-4a6c-a2ed-c993cef0776f", 0, "1e87e10e-9d33-4646-b7da-cf251027cc08", new DateTime(2023, 7, 17, 17, 27, 14, 140, DateTimeKind.Local).AddTicks(4582), "CenterReviewer@Qurrah.com", false, null, 2, null, false, null, "CenterReviewer@Qurrah.com", "CenterReviewer", "AQAAAAIAAYagAAAAEJkmyyEfAndeBXlLEAMP49poUovIMhmhRbJtowHwSfXzBDu888R+UVlj1rCNJskoSA==", "0543700745", false, "65f77872-21ea-440c-91e4-7506cdafef90", false, "CenterReviewer" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "aed4df74-b27f-4530-9442-7fd93b9b330c", "8a7a2253-8dbc-4817-afa6-1256b0ebea93" },
                    { "0b42f4ff-8f8f-42b8-a3c6-3c8bcf827edd", "a8d8a368-1f07-4a6c-a2ed-c993cef0776f" }
                });
        }
    }
}
