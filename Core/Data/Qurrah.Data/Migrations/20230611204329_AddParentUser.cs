using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddParentUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FAQ_FAQType_FKTypeId",
                table: "FAQ");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FKCreatedByUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "FKUserTypeId",
                table: "AspNetUsers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParentUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThirdName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FourthName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    FKGenderId = table.Column<byte>(type: "tinyint", nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FKUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentUser_AspNetUsers_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentUser_Gender_FKGenderId",
                        column: x => x.FKGenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Male" },
                    { (byte)2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (byte)1, "Administrator" },
                    { (byte)2, "CenterApprover" },
                    { (byte)3, "Parent" },
                    { (byte)4, "Center" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FKCreatedByUserId",
                table: "AspNetUsers",
                column: "FKCreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FKUserTypeId",
                table: "AspNetUsers",
                column: "FKUserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentUser_FKGenderId",
                table: "ParentUser",
                column: "FKGenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentUser_FKUserId",
                table: "ParentUser",
                column: "FKUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_FKCreatedByUserId",
                table: "AspNetUsers",
                column: "FKCreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserType_FKUserTypeId",
                table: "AspNetUsers",
                column: "FKUserTypeId",
                principalTable: "UserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FAQ_FAQType_FKTypeId",
                table: "FAQ",
                column: "FKTypeId",
                principalTable: "FAQType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_FKCreatedByUserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserType_FKUserTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FAQ_FAQType_FKTypeId",
                table: "FAQ");

            migrationBuilder.DropTable(
                name: "ParentUser");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FKCreatedByUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FKUserTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FKCreatedByUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FKUserTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_FAQ_FAQType_FKTypeId",
                table: "FAQ",
                column: "FKTypeId",
                principalTable: "FAQType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
