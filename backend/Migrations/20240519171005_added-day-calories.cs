using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healthy_lifestyle_web_app.Migrations
{
    /// <inheritdoc />
    public partial class addeddaycalories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "Days",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Days");
        }
    }
}
