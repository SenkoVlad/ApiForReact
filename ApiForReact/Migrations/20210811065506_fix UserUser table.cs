using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiForReact.Migrations
{
    public partial class fixUserUsertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserUser",
                columns: table => new
                {
                    SubscriberUserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubscriptionUserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUser", x => new { x.SubscriberUserId, x.SubscriptionUserId });
                    table.ForeignKey(
                        name: "FK_UserUser_Users_SubscriberUserId",
                        column: x => x.SubscriberUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserUser_Users_SubscriptionUserId",
                        column: x => x.SubscriptionUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserUser_SubscriptionUserId",
                table: "UserUser",
                column: "SubscriptionUserId");
        }
    }
}
