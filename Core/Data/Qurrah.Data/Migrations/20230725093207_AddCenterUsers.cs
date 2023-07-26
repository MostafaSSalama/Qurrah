using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCenterUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FKCreatedByUserId",
                table: "Center",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CenterUser",
                columns: table => new
                {
                    FKUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FKCenterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterUser", x => new { x.FKCenterId, x.FKUserId });
                    table.ForeignKey(
                        name: "FK_CenterUser_AspNetUsers_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterUser_Center_FKCenterId",
                        column: x => x.FKCenterId,
                        principalTable: "Center",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Center_FKCreatedByUserId",
                table: "Center",
                column: "FKCreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterUser_FKUserId",
                table: "CenterUser",
                column: "FKUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Center_AspNetUsers_FKCreatedByUserId",
                table: "Center",
                column: "FKCreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Center_AspNetUsers_FKCreatedByUserId",
                table: "Center");

            migrationBuilder.DropTable(
                name: "CenterUser");

            migrationBuilder.DropIndex(
                name: "IX_Center_FKCreatedByUserId",
                table: "Center");

            migrationBuilder.DropColumn(
                name: "FKCreatedByUserId",
                table: "Center");

        }
    }
}
