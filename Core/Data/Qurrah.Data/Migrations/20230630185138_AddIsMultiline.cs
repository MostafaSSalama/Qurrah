using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsMultiline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a41331c-fa06-4869-af68-6cb6eebd4663");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d1236b0-3d83-42ff-b9fe-7ffdc0c7fb84");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "30660785-170d-4879-a50a-fbb0538fe184", "a5b268ff-af25-4ebb-9910-06e4e5a55446" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7c75f593-da32-4e69-90f6-d31dce7d05b9", "f9c63271-a583-4604-aafc-3ad89bfc701b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30660785-170d-4879-a50a-fbb0538fe184");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c75f593-da32-4e69-90f6-d31dce7d05b9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a5b268ff-af25-4ebb-9910-06e4e5a55446");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f9c63271-a583-4604-aafc-3ad89bfc701b");

            migrationBuilder.AddColumn<bool>(
                name: "IsMultiLine",
                table: "LocalizedProperty",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e4b9ebc-680a-4581-a986-cc29c096baf8", null, "Parent", "Parent" },
                    { "69d7c466-3d10-4c06-aeb0-fb47ffb2eebb", null, "CenterApprover", "CenterApprover" },
                    { "d2906067-2255-4cfc-89c3-d4c2601aecae", null, "Center", "Center" },
                    { "edaaae21-b26a-4825-9196-0f33be438507", null, "Administrator", "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FKCreatedByUserId", "FKUserTypeId", "LastModifiedOn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7177e935-0653-4f36-b34a-f6f89174a3d7", 0, "b934f843-d8f8-4d88-8afc-10d168d9ddd3", new DateTime(2023, 6, 30, 20, 51, 37, 467, DateTimeKind.Local).AddTicks(7229), "CenterReviewer@Qurrah.com", false, null, 2, null, false, null, "CenterReviewer@Qurrah.com", "CenterReviewer", "AQAAAAIAAYagAAAAEMaX8AvBpKr6rPuD8TuLAdxymw/aEqofHqtGVopXUAQ88+70KNlHkckwVpmTn4/gRg==", "0543700745", false, "546585bd-2c95-4b97-904d-f23a5a43904d", false, "CenterReviewer" },
                    { "fecda846-2806-48d5-99f7-efbe24568a4d", 0, "902b1baa-1f3f-4dd0-83a5-49b536ac9726", new DateTime(2023, 6, 30, 20, 51, 37, 308, DateTimeKind.Local).AddTicks(4539), "Admin@Qurrah.com", false, null, 1, null, false, null, "Admin@Qurrah.com", "Admin", "AQAAAAIAAYagAAAAEJdgpgkpglJetDTZCsdTZQWeM89PGEF2kKd7fNp1svnXBZ1OxlivaTgnKk6zBNfvyg==", "0543700744", false, "738cf347-a40e-4ed6-979b-a8194b000e49", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "69d7c466-3d10-4c06-aeb0-fb47ffb2eebb", "7177e935-0653-4f36-b34a-f6f89174a3d7" },
                    { "edaaae21-b26a-4825-9196-0f33be438507", "fecda846-2806-48d5-99f7-efbe24568a4d" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e4b9ebc-680a-4581-a986-cc29c096baf8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2906067-2255-4cfc-89c3-d4c2601aecae");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "69d7c466-3d10-4c06-aeb0-fb47ffb2eebb", "7177e935-0653-4f36-b34a-f6f89174a3d7" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "edaaae21-b26a-4825-9196-0f33be438507", "fecda846-2806-48d5-99f7-efbe24568a4d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69d7c466-3d10-4c06-aeb0-fb47ffb2eebb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "edaaae21-b26a-4825-9196-0f33be438507");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7177e935-0653-4f36-b34a-f6f89174a3d7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fecda846-2806-48d5-99f7-efbe24568a4d");

            migrationBuilder.DropColumn(
                name: "IsMultiLine",
                table: "LocalizedProperty");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a41331c-fa06-4869-af68-6cb6eebd4663", null, "Center", "Center" },
                    { "30660785-170d-4879-a50a-fbb0538fe184", null, "CenterApprover", "CenterApprover" },
                    { "5d1236b0-3d83-42ff-b9fe-7ffdc0c7fb84", null, "Parent", "Parent" },
                    { "7c75f593-da32-4e69-90f6-d31dce7d05b9", null, "Administrator", "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FKCreatedByUserId", "FKUserTypeId", "LastModifiedOn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a5b268ff-af25-4ebb-9910-06e4e5a55446", 0, "341c5d00-5c69-421b-a2b2-9c69ce305383", new DateTime(2023, 6, 26, 12, 37, 56, 623, DateTimeKind.Local).AddTicks(6222), "CenterReviewer@Qurrah.com", false, null, 2, null, false, null, "CenterReviewer@Qurrah.com", "CenterReviewer", "AQAAAAIAAYagAAAAEDvdDjhuvWiHIYqeCcbehFaJud4JG4r8SnotqoFoaesYDHk5N1xWGOYTE1BusWdOZg==", "0543700745", false, "ddee8e8b-693c-4365-8b7c-bcd47dcd004d", false, "CenterReviewer" },
                    { "f9c63271-a583-4604-aafc-3ad89bfc701b", 0, "8c56e77c-f4bd-4f67-941b-010c59c1d80f", new DateTime(2023, 6, 26, 12, 37, 56, 521, DateTimeKind.Local).AddTicks(2361), "Admin@Qurrah.com", false, null, 1, null, false, null, "Admin@Qurrah.com", "Admin", "AQAAAAIAAYagAAAAEHv3gMUbBn2OCwKoGU3GnpyD1E0CRj1IbZdFIPivnARNJrfbqiGHJpfQrro8gbKshg==", "0543700744", false, "a4819d91-17e3-4efb-88b9-89b7f52bcf81", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "30660785-170d-4879-a50a-fbb0538fe184", "a5b268ff-af25-4ebb-9910-06e4e5a55446" },
                    { "7c75f593-da32-4e69-90f6-d31dce7d05b9", "f9c63271-a583-4604-aafc-3ad89bfc701b" }
                });
        }
    }
}
