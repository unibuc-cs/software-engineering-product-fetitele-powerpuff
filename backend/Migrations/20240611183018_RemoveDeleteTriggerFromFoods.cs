using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healthy_lifestyle_web_app.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeleteTriggerFromFoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP TRIGGER IF EXISTS trg_DeleteFoods;
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            -- Re-create the trigger if needed
            CREATE TRIGGER trg_DeleteFoods
            ON [dbo].[AspNetUsers]
            FOR DELETE
            AS
            BEGIN
                DELETE FROM [dbo].[Foods]
                WHERE [ApplicationUserId] IN (SELECT [Id] FROM DELETED);
            END
        ");
        }
    }

}
