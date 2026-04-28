using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsLinkedInAndGitHubToTheAspNetUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GitHubURL",
                table: "AspNetUsers",
                type: "nvarchar(550)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInURL",
                table: "AspNetUsers",
                type: "nvarchar(550)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GitHubURL",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LinkedInURL",
                table: "AspNetUsers");
        }
    }
}
