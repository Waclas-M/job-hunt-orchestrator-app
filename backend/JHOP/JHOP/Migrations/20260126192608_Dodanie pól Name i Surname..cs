using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class DodaniepólNameiSurname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "SurName");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "SurName",
                table: "AspNetUsers",
                newName: "FullName");
        }
    }
}
