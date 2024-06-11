using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healthy_lifestyle_web_app.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteTriggerToFoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE TRIGGER trg_DeleteFoods
            ON [dbo].[AspNetUsers]
            FOR DELETE
            AS
            BEGIN
                DELETE FROM [dbo].[Foods]
                WHERE [ApplicationUserId] IN (SELECT [Id] FROM DELETED);
            END
        ");

            migrationBuilder.Sql(@"
            ALTER TABLE [dbo].[Foods]
            DROP CONSTRAINT [FK_Foods_AspNetUsers_ApplicationUserId];
        ");

            migrationBuilder.Sql(@"
            ALTER TABLE [dbo].[Foods]
            ADD CONSTRAINT [FK_Foods_AspNetUsers_ApplicationUserId]
            FOREIGN KEY ([ApplicationUserId])
            REFERENCES [dbo].[AspNetUsers] ([Id])
            ON DELETE NO ACTION;
        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            ALTER TABLE [dbo].[Foods]
            DROP CONSTRAINT [FK_Foods_AspNetUsers_ApplicationUserId];
        ");

            migrationBuilder.Sql(@"
            ALTER TABLE [dbo].[Foods]
            ADD CONSTRAINT [FK_Foods_AspNetUsers_ApplicationUserId]
            FOREIGN KEY ([ApplicationUserId])
            REFERENCES [dbo].[AspNetUsers] ([Id])
            ON DELETE CASCADE;
        ");

            migrationBuilder.Sql(@"
            DROP TRIGGER trg_DeleteFoods;
        ");
        }
    }

}
