using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class UpdateProviderRequestAndProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderRequest_AspNetUsers_UserId",
                table: "ProviderRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderRequest_Provider_ProviderId",
                table: "ProviderRequest");

            migrationBuilder.DropIndex(
                name: "IX_ProviderRequest_ProviderId",
                table: "ProviderRequest");

            migrationBuilder.DropColumn(
                name: "ProviderId",
                table: "ProviderRequest");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Provider");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProviderRequest",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProviderRequest",
                type: "nvarchar(1200)",
                maxLength: 1200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Provider",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Provider",
                type: "nvarchar(1200)",
                maxLength: 1200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProviderRequestId",
                table: "Provider",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProviderRequestId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Provider_ProviderRequestId",
                table: "Provider",
                column: "ProviderRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Provider_ProviderRequest_ProviderRequestId",
                table: "Provider",
                column: "ProviderRequestId",
                principalTable: "ProviderRequest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderRequest_AspNetUsers_UserId",
                table: "ProviderRequest",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Provider_ProviderRequest_ProviderRequestId",
                table: "Provider");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderRequest_AspNetUsers_UserId",
                table: "ProviderRequest");

            migrationBuilder.DropIndex(
                name: "IX_Provider_ProviderRequestId",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "ProviderRequestId",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "ProviderRequestId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProviderRequest",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProviderRequest",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1200)",
                oldMaxLength: 1200,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProviderId",
                table: "ProviderRequest",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Provider",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Provider",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1200)",
                oldMaxLength: 1200,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Provider",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ProviderRequest_ProviderId",
                table: "ProviderRequest",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderRequest_AspNetUsers_UserId",
                table: "ProviderRequest",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderRequest_Provider_ProviderId",
                table: "ProviderRequest",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
