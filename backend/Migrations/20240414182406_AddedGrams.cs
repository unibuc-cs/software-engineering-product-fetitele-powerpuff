using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healthy_lifestyle_web_app.Migrations
{
    /// <inheritdoc />
    public partial class AddedGrams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayFood");

            migrationBuilder.CreateTable(
                name: "DayFoods",
                columns: table => new
                {
                    DayId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    DayProfileId = table.Column<int>(type: "int", nullable: false),
                    DayDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Grams = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayFoods", x => new { x.DayId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_DayFoods_Days_DayProfileId_DayDate",
                        columns: x => new { x.DayProfileId, x.DayDate },
                        principalTable: "Days",
                        principalColumns: new[] { "ProfileId", "Date" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayFoods_DayProfileId_DayDate",
                table: "DayFoods",
                columns: new[] { "DayProfileId", "DayDate" });

            migrationBuilder.CreateIndex(
                name: "IX_DayFoods_FoodId",
                table: "DayFoods",
                column: "FoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayFoods");

            migrationBuilder.CreateTable(
                name: "DayFood",
                columns: table => new
                {
                    FoodsId = table.Column<int>(type: "int", nullable: false),
                    DaysProfileId = table.Column<int>(type: "int", nullable: false),
                    DaysDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayFood", x => new { x.FoodsId, x.DaysProfileId, x.DaysDate });
                    table.ForeignKey(
                        name: "FK_DayFood_Days_DaysProfileId_DaysDate",
                        columns: x => new { x.DaysProfileId, x.DaysDate },
                        principalTable: "Days",
                        principalColumns: new[] { "ProfileId", "Date" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayFood_Foods_FoodsId",
                        column: x => x.FoodsId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayFood_DaysProfileId_DaysDate",
                table: "DayFood",
                columns: new[] { "DaysProfileId", "DaysDate" });
        }
    }
}
