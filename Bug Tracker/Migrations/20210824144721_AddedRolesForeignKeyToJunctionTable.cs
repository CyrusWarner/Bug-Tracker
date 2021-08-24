using Microsoft.EntityFrameworkCore.Migrations;

namespace Bug_Tracker.Migrations
{
    public partial class AddedRolesForeignKeyToJunctionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolesId",
                table: "UserBoard",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserBoard_RolesId",
                table: "UserBoard",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBoard_Roles_RolesId",
                table: "UserBoard",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "RolesId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBoard_Roles_RolesId",
                table: "UserBoard");

            migrationBuilder.DropIndex(
                name: "IX_UserBoard_RolesId",
                table: "UserBoard");

            migrationBuilder.DropColumn(
                name: "RolesId",
                table: "UserBoard");
        }
    }
}
