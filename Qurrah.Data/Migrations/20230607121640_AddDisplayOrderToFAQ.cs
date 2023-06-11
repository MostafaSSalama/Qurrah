﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDisplayOrderToFAQ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "FAQ",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "FAQ");
        }
    }
}
