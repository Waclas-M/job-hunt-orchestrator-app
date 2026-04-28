using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class UserProjectsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "projectURL",
                table: "UserProjects",
                newName: "ProjectURL");

            migrationBuilder.RenameColumn(
                name: "TechnologiesUsed",
                table: "UserProjects",
                newName: "Technologies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectURL",
                table: "UserProjects",
                newName: "projectURL");

            migrationBuilder.RenameColumn(
                name: "Technologies",
                table: "UserProjects",
                newName: "TechnologiesUsed");
        }
    }
}
