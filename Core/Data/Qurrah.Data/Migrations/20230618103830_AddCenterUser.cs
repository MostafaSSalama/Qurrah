using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCenterUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentUser_AspNetUsers_FKUserId",
                table: "ParentUser");

            migrationBuilder.DropIndex(
                name: "IX_ParentUser_FKUserId",
                table: "ParentUser");

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

            migrationBuilder.CreateTable(
                name: "Center",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Center", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CenterOwnerUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKCenterId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThirdName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FourthName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FKGenderId = table.Column<byte>(type: "tinyint", nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FKUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterOwnerUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterOwnerUser_AspNetUsers_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterOwnerUser_Center_FKCenterId",
                        column: x => x.FKCenterId,
                        principalTable: "Center",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterOwnerUser_Gender_FKGenderId",
                        column: x => x.FKGenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "113ca503-db17-41a0-b74f-704c85acc2e5", null, "Administrator", "Administrator" },
                    { "3006c5e5-d5f6-438d-92e6-7035502278cc", null, "CenterApprover", "CenterApprover" },
                    { "443b9ab1-91f4-4e43-b6a9-d878e16f9bb3", null, "Parent", "Parent" },
                    { "49fab27d-476e-4b81-b2c7-27282340dace", null, "Center", "Center" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FKCreatedByUserId", "FKUserTypeId", "LastModifiedOn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a92dd7e3-f9cb-4c4d-ac58-a1cd75e69a43", 0, "20b53309-5688-4886-a279-da65086ec66f", new DateTime(2023, 6, 18, 13, 38, 30, 28, DateTimeKind.Local).AddTicks(6202), "Admin@Qurrah.com", false, null, (byte)1, null, false, null, "Admin@Qurrah.com", "Admin", "AQAAAAIAAYagAAAAEMUnWcBw7CfKM8pPHBA6xUW0ngG3V6/eqJA4lyC6RM+PMLY3LEWXmPb4z6lbKUir/g==", "0543700744", false, "2cf9982e-3193-47ac-9f27-6ea3475c309f", false, "Admin" },
                    { "d95fe55c-2d73-4282-882d-284212ecd035", 0, "2c78fa7b-0843-43d3-885f-433f0589a7f3", new DateTime(2023, 6, 18, 13, 38, 30, 89, DateTimeKind.Local).AddTicks(9430), "CenterReviewer@Qurrah.com", false, null, (byte)2, null, false, null, "CenterReviewer@Qurrah.com", "CenterReviewer", "AQAAAAIAAYagAAAAEPOK88qguTwUOyh79ogwfeGBiNBGaAWhhY/rAU2zNYIVnSgUsEMbqHAA1kBZrVcmAQ==", "0543700745", false, "1897374d-76f8-4d4a-a7dc-58711cbd4dc1", false, "CenterReviewer" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "113ca503-db17-41a0-b74f-704c85acc2e5", "a92dd7e3-f9cb-4c4d-ac58-a1cd75e69a43" },
                    { "3006c5e5-d5f6-438d-92e6-7035502278cc", "d95fe55c-2d73-4282-882d-284212ecd035" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParentUser_FKUserId",
                table: "ParentUser",
                column: "FKUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Center_Name",
                table: "Center",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterOwnerUser_FKCenterId",
                table: "CenterOwnerUser",
                column: "FKCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterOwnerUser_FKGenderId",
                table: "CenterOwnerUser",
                column: "FKGenderId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterOwnerUser_FKUserId",
                table: "CenterOwnerUser",
                column: "FKUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentUser_AspNetUsers_FKUserId",
                table: "ParentUser",
                column: "FKUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentUser_AspNetUsers_FKUserId",
                table: "ParentUser");

            migrationBuilder.DropTable(
                name: "CenterOwnerUser");

            migrationBuilder.DropTable(
                name: "Center");

            migrationBuilder.DropIndex(
                name: "IX_ParentUser_FKUserId",
                table: "ParentUser");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "443b9ab1-91f4-4e43-b6a9-d878e16f9bb3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49fab27d-476e-4b81-b2c7-27282340dace");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "113ca503-db17-41a0-b74f-704c85acc2e5", "a92dd7e3-f9cb-4c4d-ac58-a1cd75e69a43" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3006c5e5-d5f6-438d-92e6-7035502278cc", "d95fe55c-2d73-4282-882d-284212ecd035" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "113ca503-db17-41a0-b74f-704c85acc2e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3006c5e5-d5f6-438d-92e6-7035502278cc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a92dd7e3-f9cb-4c4d-ac58-a1cd75e69a43");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d95fe55c-2d73-4282-882d-284212ecd035");

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

            migrationBuilder.CreateIndex(
                name: "IX_ParentUser_FKUserId",
                table: "ParentUser",
                column: "FKUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentUser_AspNetUsers_FKUserId",
                table: "ParentUser",
                column: "FKUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
