using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiForReact.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_Users_UserId",
                table: "Dialogs");

            migrationBuilder.DropColumn(
                name: "UserCompanion",
                table: "Dialogs");

            migrationBuilder.DropColumn(
                name: "UserOwner",
                table: "Dialogs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Dialogs",
                newName: "UserOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Dialogs_UserId",
                table: "Dialogs",
                newName: "IX_Dialogs_UserOwnerId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserCompanionId",
                table: "Dialogs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_UserCompanionId",
                table: "Dialogs",
                column: "UserCompanionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_Users_UserCompanionId",
                table: "Dialogs",
                column: "UserCompanionId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_Users_UserOwnerId",
                table: "Dialogs",
                column: "UserOwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_Users_UserCompanionId",
                table: "Dialogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_Users_UserOwnerId",
                table: "Dialogs");

            migrationBuilder.DropIndex(
                name: "IX_Dialogs_UserCompanionId",
                table: "Dialogs");

            migrationBuilder.DropColumn(
                name: "UserCompanionId",
                table: "Dialogs");

            migrationBuilder.RenameColumn(
                name: "UserOwnerId",
                table: "Dialogs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Dialogs_UserOwnerId",
                table: "Dialogs",
                newName: "IX_Dialogs_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserCompanion",
                table: "Dialogs",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserOwner",
                table: "Dialogs",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_Users_UserId",
                table: "Dialogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
