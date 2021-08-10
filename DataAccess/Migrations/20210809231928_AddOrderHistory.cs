using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddOrderHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_UserOrder_OrderHistoryId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "UserOrder");

            migrationBuilder.RenameColumn(
                name: "OrderHistoryId",
                table: "OrderItems",
                newName: "UserOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderHistoryId",
                table: "OrderItems",
                newName: "IX_OrderItems_UserOrderId");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Provider",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                table: "Provider",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsOrderSuccessful = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistory_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderHistory_UserOrder_UserOrderId",
                        column: x => x.UserOrderId,
                        principalTable: "UserOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Provider_RoleId1",
                table: "Provider",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_ProviderId",
                table: "OrderHistory",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_UserId",
                table: "OrderHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_UserOrderId",
                table: "OrderHistory",
                column: "UserOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_UserOrder_UserOrderId",
                table: "OrderItems",
                column: "UserOrderId",
                principalTable: "UserOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Provider_AspNetRoles_RoleId1",
                table: "Provider",
                column: "RoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_UserOrder_UserOrderId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Provider_AspNetRoles_RoleId1",
                table: "Provider");

            migrationBuilder.DropTable(
                name: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_Provider_RoleId1",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "Provider");

            migrationBuilder.RenameColumn(
                name: "UserOrderId",
                table: "OrderItems",
                newName: "OrderHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_UserOrderId",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderHistoryId");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "UserOrder",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_UserOrder_OrderHistoryId",
                table: "OrderItems",
                column: "OrderHistoryId",
                principalTable: "UserOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
