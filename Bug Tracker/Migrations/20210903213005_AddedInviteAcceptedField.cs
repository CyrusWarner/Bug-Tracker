using Microsoft.EntityFrameworkCore.Migrations;

namespace Bug_Tracker.Migrations
{
    public partial class AddedInviteAcceptedField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InviteAccepted",
                table: "UserBoard",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteAccepted",
                table: "UserBoard");
        }
    }
}
