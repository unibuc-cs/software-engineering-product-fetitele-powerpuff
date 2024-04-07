using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healthy_lifestyle_web_app.Migrations
{
    /// <inheritdoc />
    public partial class AddedMuscle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Muscles",
                table: "PhysicalActivities");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Muscle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muscle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MusclePhysicalActivity",
                columns: table => new
                {
                    MusclesId = table.Column<int>(type: "int", nullable: false),
                    PhysicalActivitiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusclePhysicalActivity", x => new { x.MusclesId, x.PhysicalActivitiesId });
                    table.ForeignKey(
                        name: "FK_MusclePhysicalActivity_Muscle_MusclesId",
                        column: x => x.MusclesId,
                        principalTable: "Muscle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusclePhysicalActivity_PhysicalActivities_PhysicalActivitiesId",
                        column: x => x.PhysicalActivitiesId,
                        principalTable: "PhysicalActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusclePhysicalActivity_PhysicalActivitiesId",
                table: "MusclePhysicalActivity",
                column: "PhysicalActivitiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusclePhysicalActivity");

            migrationBuilder.DropTable(
                name: "Muscle");

            migrationBuilder.AddColumn<string>(
                name: "Muscles",
                table: "PhysicalActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
