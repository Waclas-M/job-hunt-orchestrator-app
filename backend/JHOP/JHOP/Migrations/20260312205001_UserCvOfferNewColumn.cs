using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class UserCvOfferNewColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "UserCvOffers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "UserCvOffers");
        }
    }
}
