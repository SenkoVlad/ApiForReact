using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiForReact.Migrations
{
    public partial class UserUseradded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubscriberUserId = table.Column<Guid>(type: "TEXT", nullable: true),
                    SubscriptionUserId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersUsers_Users_SubscriberUserId",
                        column: x => x.SubscriberUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersUsers_Users_SubscriptionUserId",
                        column: x => x.SubscriptionUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersUsers_SubscriberUserId",
                table: "UsersUsers",
                column: "SubscriberUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersUsers_SubscriptionUserId",
                table: "UsersUsers",
                column: "SubscriptionUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersUsers");
        }
    }
}
