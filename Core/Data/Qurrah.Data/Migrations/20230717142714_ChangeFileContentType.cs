using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFileContentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13ea01e6-d4f2-4057-834c-56a0d86e1eb2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bc3d7e1-616c-4453-b585-40027787b18c");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "063140b5-5846-4d2a-8b06-31a28f74874a", "4393593c-7587-4823-a694-43bcc28fc443" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8c184d43-b12b-4c81-b15a-15e0b91a5d71", "b48073ae-515e-4735-bffa-e238f4b52bf4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "063140b5-5846-4d2a-8b06-31a28f74874a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c184d43-b12b-4c81-b15a-15e0b91a5d71");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4393593c-7587-4823-a694-43bcc28fc443");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b48073ae-515e-4735-bffa-e238f4b52bf4");

            migrationBuilder.AlterColumn<string>(
                name: "FileData",
                table: "FileDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<byte[]>(
                name: "FileData",
                table: "FileDetails",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "063140b5-5846-4d2a-8b06-31a28f74874a", null, "Administrator", "Administrator" },
                    { "13ea01e6-d4f2-4057-834c-56a0d86e1eb2", null, "Parent", "Parent" },
                    { "8bc3d7e1-616c-4453-b585-40027787b18c", null, "Center", "Center" },
                    { "8c184d43-b12b-4c81-b15a-15e0b91a5d71", null, "CenterApprover", "CenterApprover" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FKCreatedByUserId", "FKUserTypeId", "LastModifiedOn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "4393593c-7587-4823-a694-43bcc28fc443", 0, "422b58f4-4e79-42a8-8674-8698f90c7a31", new DateTime(2023, 7, 17, 0, 13, 13, 13, DateTimeKind.Local).AddTicks(2618), "Admin@Qurrah.com", false, null, 1, null, false, null, "Admin@Qurrah.com", "Admin", "AQAAAAIAAYagAAAAEIrSIczHjnSCwjsGi2Baixxodc0I2k+YiDnT/ix/46dJryLzpcvoLUaR5obmUxl3Lw==", "0543700744", false, "86f84dd5-6945-47a3-a947-e891933aad77", false, "Admin" },
                    { "b48073ae-515e-4735-bffa-e238f4b52bf4", 0, "5a9536e0-52c9-4599-9bf6-b572e5119ad0", new DateTime(2023, 7, 17, 0, 13, 13, 133, DateTimeKind.Local).AddTicks(6556), "CenterReviewer@Qurrah.com", false, null, 2, null, false, null, "CenterReviewer@Qurrah.com", "CenterReviewer", "AQAAAAIAAYagAAAAEGPsG7Kl4i4sfvrED8936ytTfLHeBNKWOeUGa0STMOkuBRuyUtGZRXX8ezVYSlCdAA==", "0543700745", false, "2183bb9e-595c-4591-8c6b-0dae53f8ecc7", false, "CenterReviewer" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "063140b5-5846-4d2a-8b06-31a28f74874a", "4393593c-7587-4823-a694-43bcc28fc443" },
                    { "8c184d43-b12b-4c81-b15a-15e0b91a5d71", "b48073ae-515e-4735-bffa-e238f4b52bf4" }
                });
        }
    }
}
