using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class RemoveBonusPointsFromProductGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Product_ProductId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "BonusPoints",
                table: "ProductGroup");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "ProductPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_Files_ProductId",
                table: "ProductPhotos",
                newName: "IX_ProductPhotos_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPhotos",
                table: "ProductPhotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Product_ProductId",
                table: "ProductPhotos",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Product_ProductId",
                table: "ProductPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPhotos",
                table: "ProductPhotos");

            migrationBuilder.RenameTable(
                name: "ProductPhotos",
                newName: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPhotos_ProductId",
                table: "Files",
                newName: "IX_Files_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "BonusPoints",
                table: "ProductGroup",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Product_ProductId",
                table: "Files",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
