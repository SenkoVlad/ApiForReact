using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiForReact.Migrations
{
    public partial class addeduniquekeyUserUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersUsers_Users_SubscriberUserId",
                table: "UsersUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersUsers_Users_SubscriptionUserId",
                table: "UsersUsers");

            migrationBuilder.DropIndex(
                name: "IX_UsersUsers_SubscriberUserId",
                table: "UsersUsers");

            migrationBuilder.DropIndex(
                name: "IX_UsersUsers_SubscriptionUserId",
                table: "UsersUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriptionUserId",
                table: "UsersUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriberUserId",
                table: "UsersUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersUsers_SubscriberUserId_SubscriptionUserId",
                table: "UsersUsers",
                columns: new[] { "SubscriberUserId", "SubscriptionUserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersUsers_SubscriberUserId_SubscriptionUserId",
                table: "UsersUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriptionUserId",
                table: "UsersUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriberUserId",
                table: "UsersUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_UsersUsers_SubscriberUserId",
                table: "UsersUsers",
                column: "SubscriberUserId");

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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersUsers_Users_SubscriptionUserId",
                table: "UsersUsers",
                column: "SubscriptionUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
