using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDisplayOrderToFAQType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "FAQType",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "FAQType");
        }
    }
}
