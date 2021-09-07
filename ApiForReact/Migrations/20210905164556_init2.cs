using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiForReact.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_CompanionUserId",
                table: "Dialogs",
                column: "CompanionUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_OwnerUserId",
                table: "Dialogs",
                column: "OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_Users_CompanionUserId",
                table: "Dialogs",
                column: "CompanionUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_Users_OwnerUserId",
                table: "Dialogs",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_Users_CompanionUserId",
                table: "Dialogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_Users_OwnerUserId",
                table: "Dialogs");

            migrationBuilder.DropIndex(
                name: "IX_Dialogs_CompanionUserId",
                table: "Dialogs");

            migrationBuilder.DropIndex(
                name: "IX_Dialogs_OwnerUserId",
                table: "Dialogs");
        }
    }
}
