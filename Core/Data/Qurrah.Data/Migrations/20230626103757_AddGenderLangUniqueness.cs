using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderLangUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71773683-0f94-4e25-9d97-ffe70827fbb6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4196a4c-6713-4214-b3a2-0ef0159ba7e3");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1c1fad40-cd3b-426f-b4c0-6205236df447", "ad856c8c-eebb-4984-b0ec-5ce7f3efa22c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "abd66b75-dd65-4809-8db5-874c0f8ce275", "b6578ba7-8b09-41c1-83b5-331186006e14" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c1fad40-cd3b-426f-b4c0-6205236df447");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abd66b75-dd65-4809-8db5-874c0f8ce275");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ad856c8c-eebb-4984-b0ec-5ce7f3efa22c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b6578ba7-8b09-41c1-83b5-331186006e14");

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

            migrationBuilder.CreateIndex(
                name: "IX_Language_LanguageCulture",
                table: "Language",
                column: "LanguageCulture",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gender_Name",
                table: "Gender",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Language_LanguageCulture",
                table: "Language");

            migrationBuilder.DropIndex(
                name: "IX_Gender_Name",
                table: "Gender");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c1fad40-cd3b-426f-b4c0-6205236df447", null, "Administrator", "Administrator" },
                    { "71773683-0f94-4e25-9d97-ffe70827fbb6", null, "Center", "Center" },
                    { "a4196a4c-6713-4214-b3a2-0ef0159ba7e3", null, "Parent", "Parent" },
                    { "abd66b75-dd65-4809-8db5-874c0f8ce275", null, "CenterApprover", "CenterApprover" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FKCreatedByUserId", "FKUserTypeId", "LastModifiedOn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "ad856c8c-eebb-4984-b0ec-5ce7f3efa22c", 0, "e643d9a2-d478-4e5c-8ccf-f9bf32142061", new DateTime(2023, 6, 25, 22, 1, 55, 342, DateTimeKind.Local).AddTicks(7471), "Admin@Qurrah.com", false, null, 1, null, false, null, "Admin@Qurrah.com", "Admin", "AQAAAAIAAYagAAAAEJTE5PrIrSrBthlhc7n16og3Xnir72Sc+ZRYWHO50eRqXjQOt2nbGZK+KpRey196KA==", "0543700744", false, "c2a4accb-0aa3-4cde-af0e-fcc63ca75a45", false, "Admin" },
                    { "b6578ba7-8b09-41c1-83b5-331186006e14", 0, "3a869b20-05ee-4d0b-a25f-b2caf6391cea", new DateTime(2023, 6, 25, 22, 1, 55, 474, DateTimeKind.Local).AddTicks(4718), "CenterReviewer@Qurrah.com", false, null, 2, null, false, null, "CenterReviewer@Qurrah.com", "CenterReviewer", "AQAAAAIAAYagAAAAEOrEm4/zLg7B487c0Wx+WZQH1SK+JiEmA9HgPsg9Z+PuE99fu+OuvMZDs8pv3of0Dw==", "0543700745", false, "544dead9-996c-481a-a720-e1b46bd93844", false, "CenterReviewer" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1c1fad40-cd3b-426f-b4c0-6205236df447", "ad856c8c-eebb-4984-b0ec-5ce7f3efa22c" },
                    { "abd66b75-dd65-4809-8db5-874c0f8ce275", "b6578ba7-8b09-41c1-83b5-331186006e14" }
                });
        }
    }
}
