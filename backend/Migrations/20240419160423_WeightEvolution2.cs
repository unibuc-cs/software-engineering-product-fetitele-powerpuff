using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healthy_lifestyle_web_app.Migrations
{
    public partial class WeightEvolution2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key constraint associated with EvolutionId
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightEvolutions",
                table: "WeightEvolutions");

            // Drop the existing EvolutionId column
            migrationBuilder.DropColumn(
                name: "EvolutionId",
                table: "WeightEvolutions");

            // Recreate the EvolutionId column with identity specification
            migrationBuilder.AddColumn<int>(
                name: "EvolutionId",
                table: "WeightEvolutions",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Add the primary key constraint to the new EvolutionId column
            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightEvolutions",
                table: "WeightEvolutions",
                column: "EvolutionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key constraint associated with EvolutionId
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeightEvolutions",
                table: "WeightEvolutions");

            // Drop the recreated EvolutionId column
            migrationBuilder.DropColumn(
                name: "EvolutionId",
                table: "WeightEvolutions");

            // Add back the EvolutionId column without identity specification
            migrationBuilder.AddColumn<int>(
                name: "EvolutionId",
                table: "WeightEvolutions",
                type: "int",
                nullable: false);

            // Add back the primary key constraint to the original EvolutionId column
            migrationBuilder.AddPrimaryKey(
                name: "PK_WeightEvolutions",
                table: "WeightEvolutions",
                column: "EvolutionId");
        }
    }
}
