using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healthy_lifestyle_web_app.Migrations
{
    /// <inheritdoc />
    public partial class AddedGrams1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayFoods_Days_DayProfileId_DayDate",
                table: "DayFoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayFoods",
                table: "DayFoods");

            migrationBuilder.DropIndex(
                name: "IX_DayFoods_DayProfileId_DayDate",
                table: "DayFoods");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "DayFoods");

            migrationBuilder.RenameColumn(
                name: "DayProfileId",
                table: "DayFoods",
                newName: "ProfileId");

            migrationBuilder.RenameColumn(
                name: "DayDate",
                table: "DayFoods",
                newName: "Date");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "DayFoods",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "DayFoods",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayFoods",
                table: "DayFoods",
                columns: new[] { "ProfileId", "Date", "FoodId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DayFoods_Days_ProfileId_Date",
                table: "DayFoods",
                columns: new[] { "ProfileId", "Date" },
                principalTable: "Days",
                principalColumns: new[] { "ProfileId", "Date" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DayFoods_Days_ProfileId_Date",
                table: "DayFoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DayFoods",
                table: "DayFoods");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "DayFoods",
                newName: "DayDate");

            migrationBuilder.RenameColumn(
                name: "ProfileId",
                table: "DayFoods",
                newName: "DayProfileId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DayDate",
                table: "DayFoods",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "DayProfileId",
                table: "DayFoods",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "DayFoods",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DayFoods",
                table: "DayFoods",
                columns: new[] { "DayId", "FoodId" });

            migrationBuilder.CreateIndex(
                name: "IX_DayFoods_DayProfileId_DayDate",
                table: "DayFoods",
                columns: new[] { "DayProfileId", "DayDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_DayFoods_Days_DayProfileId_DayDate",
                table: "DayFoods",
                columns: new[] { "DayProfileId", "DayDate" },
                principalTable: "Days",
                principalColumns: new[] { "ProfileId", "Date" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
