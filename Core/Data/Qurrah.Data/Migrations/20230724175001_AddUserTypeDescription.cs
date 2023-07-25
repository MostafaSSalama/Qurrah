using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTypeDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTypeDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKUserTypeId = table.Column<int>(type: "int", nullable: false),
                    FKLanguageId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypeDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTypeDescription_Language_FKLanguageId",
                        column: x => x.FKLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserTypeDescription_UserType_FKUserTypeId",
                        column: x => x.FKUserTypeId,
                        principalTable: "UserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "UserTypeDescription",
                columns: new[] { "Id", "Description", "FKLanguageId", "FKUserTypeId" },
                values: new object[,]
                {
                    { 1, "مدير النظام", 1, 1 },
                    { 2, "Administrator", 2, 1 },
                    { 3, "ولي أمر", 1, 3 },
                    { 4, "Parent", 2, 3 },
                    { 5, "مركز", 1, 4 },
                    { 6, "Center", 2, 4 },
                    { 7, "مراجع مراكز", 1, 2 },
                    { 8, "Center Approver", 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserTypeDescription_FKLanguageId_FKUserTypeId",
                table: "UserTypeDescription",
                columns: new[] { "FKLanguageId", "FKUserTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTypeDescription_FKUserTypeId",
                table: "UserTypeDescription",
                column: "FKUserTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTypeDescription");

        }
    }
}
