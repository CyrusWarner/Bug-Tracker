using Microsoft.EntityFrameworkCore.Migrations;

namespace Bug_Tracker.Migrations
{
    public partial class AddedUserBoardModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBoard",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BoardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBoard", x => new { x.UserId, x.BoardId });
                    table.ForeignKey(
                        name: "FK_UserBoard_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "BoardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBoard_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBoard_BoardId",
                table: "UserBoard",
                column: "BoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
