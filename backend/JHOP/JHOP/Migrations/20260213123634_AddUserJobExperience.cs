using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class AddUserJobExperience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserJobExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(1100)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJobExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserJobExperiences_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserJobExperiences_UserId",
                table: "UserJobExperiences",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserJobExperiences");
        }
    }
}
