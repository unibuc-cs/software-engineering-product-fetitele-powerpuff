using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healthy_lifestyle_web_app.Migrations
{
    /// <inheritdoc />
    public partial class DayPhysicalActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayPhysicalActivity");

            migrationBuilder.CreateTable(
                name: "DayPhysicalActivities",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    PhysicalActivityId = table.Column<int>(type: "int", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayPhysicalActivities", x => new { x.ProfileId, x.Date, x.PhysicalActivityId });
                    table.ForeignKey(
                        name: "FK_DayPhysicalActivities_Days_ProfileId_Date",
                        columns: x => new { x.ProfileId, x.Date },
                        principalTable: "Days",
                        principalColumns: new[] { "ProfileId", "Date" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayPhysicalActivities_PhysicalActivities_PhysicalActivityId",
                        column: x => x.PhysicalActivityId,
                        principalTable: "PhysicalActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayPhysicalActivities_PhysicalActivityId",
                table: "DayPhysicalActivities",
                column: "PhysicalActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayPhysicalActivities");

            migrationBuilder.CreateTable(
                name: "DayPhysicalActivity",
                columns: table => new
                {
                    PhysicalActivitiesId = table.Column<int>(type: "int", nullable: false),
                    DaysProfileId = table.Column<int>(type: "int", nullable: false),
                    DaysDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayPhysicalActivity", x => new { x.PhysicalActivitiesId, x.DaysProfileId, x.DaysDate });
                    table.ForeignKey(
                        name: "FK_DayPhysicalActivity_Days_DaysProfileId_DaysDate",
                        columns: x => new { x.DaysProfileId, x.DaysDate },
                        principalTable: "Days",
                        principalColumns: new[] { "ProfileId", "Date" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayPhysicalActivity_PhysicalActivities_PhysicalActivitiesId",
                        column: x => x.PhysicalActivitiesId,
                        principalTable: "PhysicalActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayPhysicalActivity_DaysProfileId_DaysDate",
                table: "DayPhysicalActivity",
                columns: new[] { "DaysProfileId", "DaysDate" });
        }
    }
}
