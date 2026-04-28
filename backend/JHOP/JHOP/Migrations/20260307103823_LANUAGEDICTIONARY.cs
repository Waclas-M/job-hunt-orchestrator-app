using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class LANUAGEDICTIONARY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Language",
                table: "UserLanguages",
                type: "int",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "UserLanguages",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 80);
        }
    }
}
