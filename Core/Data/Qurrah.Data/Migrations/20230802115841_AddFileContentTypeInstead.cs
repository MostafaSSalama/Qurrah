using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFileContentTypeInstead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileDetails_FileType_FKFileTypeId",
                table: "FileDetails");

            migrationBuilder.DropTable(
                name: "FileType");

            migrationBuilder.DropIndex(
                name: "IX_FileDetails_FKFileTypeId",
                table: "FileDetails");

            migrationBuilder.DropColumn(
                name: "FKFileTypeId",
                table: "FileDetails");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "FileDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "FileDetails");

            migrationBuilder.AddColumn<int>(
                name: "FKFileTypeId",
                table: "FileDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FileType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileDetails_FKFileTypeId",
                table: "FileDetails",
                column: "FKFileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FileType_Name",
                table: "FileType",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileDetails_FileType_FKFileTypeId",
                table: "FileDetails",
                column: "FKFileTypeId",
                principalTable: "FileType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}