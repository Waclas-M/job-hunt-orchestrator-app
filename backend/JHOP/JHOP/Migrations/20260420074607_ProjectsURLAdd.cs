using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class ProjectsURLAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "projectURL",
                table: "UserProjects",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "projectURL",
                table: "UserProjects");
        }
    }
}
