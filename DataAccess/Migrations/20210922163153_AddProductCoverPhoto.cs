using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddProductCoverPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CoverPhotoId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Product_CoverPhotoId",
                table: "Product",
                column: "CoverPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductPhotos_CoverPhotoId",
                table: "Product",
                column: "CoverPhotoId",
                principalTable: "ProductPhotos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductPhotos_CoverPhotoId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CoverPhotoId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CoverPhotoId",
                table: "Product");
        }
    }
}
