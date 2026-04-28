using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnsInCvOfferTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserCvOffers",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserCvOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserCvOffers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserCvOffers");
        }
    }
}
