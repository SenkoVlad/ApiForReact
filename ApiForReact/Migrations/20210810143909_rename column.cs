using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiForReact.Migrations
{
    public partial class renamecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_Users_SubscriberUsersId",
                table: "UserUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_Users_SubscriptionUsersId",
                table: "UserUser");

            migrationBuilder.RenameColumn(
                name: "SubscriptionUsersId",
                table: "UserUser",
                newName: "SubscriptionUserId");

            migrationBuilder.RenameColumn(
                name: "SubscriberUsersId",
                table: "UserUser",
                newName: "SubscriberUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserUser_SubscriptionUsersId",
                table: "UserUser",
                newName: "IX_UserUser_SubscriptionUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_Users_SubscriberUserId",
                table: "UserUser",
                column: "SubscriberUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_Users_SubscriptionUserId",
                table: "UserUser",
                column: "SubscriptionUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_Users_SubscriberUserId",
                table: "UserUser");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUser_Users_SubscriptionUserId",
                table: "UserUser");

            migrationBuilder.RenameColumn(
                name: "SubscriptionUserId",
                table: "UserUser",
                newName: "SubscriptionUsersId");

            migrationBuilder.RenameColumn(
                name: "SubscriberUserId",
                table: "UserUser",
                newName: "SubscriberUsersId");

            migrationBuilder.RenameIndex(
                name: "IX_UserUser_SubscriptionUserId",
                table: "UserUser",
                newName: "IX_UserUser_SubscriptionUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_Users_SubscriberUsersId",
                table: "UserUser",
                column: "SubscriberUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUser_Users_SubscriptionUsersId",
                table: "UserUser",
                column: "SubscriptionUsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
