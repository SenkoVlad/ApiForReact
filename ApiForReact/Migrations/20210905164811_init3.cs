using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiForReact.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsersUsers_SubscriptionUserId",
                table: "UsersUsers",
                column: "SubscriptionUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersUsers_Users_SubscriberUserId",
                table: "UsersUsers",
                column: "SubscriberUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersUsers_Users_SubscriptionUserId",
                table: "UsersUsers",
                column: "SubscriptionUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersUsers_Users_SubscriberUserId",
                table: "UsersUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersUsers_Users_SubscriptionUserId",
                table: "UsersUsers");

            migrationBuilder.DropIndex(
                name: "IX_UsersUsers_SubscriptionUserId",
                table: "UsersUsers");
        }
    }
}
