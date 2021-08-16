using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiForReact.Migrations
{
    public partial class updateduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLookingForAJob",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ResumeText",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserContactsId",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Facebook = table.Column<string>(type: "TEXT", nullable: true),
                    Vk = table.Column<string>(type: "TEXT", nullable: true),
                    Instagram = table.Column<string>(type: "TEXT", nullable: true),
                    Youtube = table.Column<string>(type: "TEXT", nullable: true),
                    GitHub = table.Column<string>(type: "TEXT", nullable: true),
                    Twitter = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContacts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserContactsId",
                table: "Users",
                column: "UserContactsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserContacts_UserContactsId",
                table: "Users",
                column: "UserContactsId",
                principalTable: "UserContacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserContacts_UserContactsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserContacts");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserContactsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsLookingForAJob",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResumeText",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserContactsId",
                table: "Users");
        }
    }
}
