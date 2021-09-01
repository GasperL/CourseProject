using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderRequest_AspNetUsers_Id",
                table: "ProviderRequest");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProviderRequest",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProviderRequest_UserId",
                table: "ProviderRequest",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderRequest_AspNetUsers_UserId",
                table: "ProviderRequest",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderRequest_AspNetUsers_UserId",
                table: "ProviderRequest");

            migrationBuilder.DropIndex(
                name: "IX_ProviderRequest_UserId",
                table: "ProviderRequest");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProviderRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderRequest_AspNetUsers_Id",
                table: "ProviderRequest",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
