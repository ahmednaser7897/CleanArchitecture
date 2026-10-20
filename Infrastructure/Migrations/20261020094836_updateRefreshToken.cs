using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "RevokedOn",
                table: "RefreshToken",
                newName: "RevokedAt");

            migrationBuilder.RenameColumn(
                name: "ExpiresOn",
                table: "RefreshToken",
                newName: "ExpiresAt");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "RefreshToken",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "RefreshToken",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedByUserId",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RefreshToken",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "RefreshToken",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_ApplicationUserId",
                table: "RefreshToken",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_ApplicationUserId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "RevokedAt",
                table: "RefreshToken",
                newName: "RevokedOn");

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                table: "RefreshToken",
                newName: "ExpiresOn");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "RefreshToken",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                columns: new[] { "ApplicationUserId", "Id" });
        }
    }
}
