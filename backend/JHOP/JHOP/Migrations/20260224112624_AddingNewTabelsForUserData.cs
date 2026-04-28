using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewTabelsForUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserIntrests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Intrest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIntrests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserIntrests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLanguages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Skill = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSkills_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserStrengs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Streng = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStrengs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStrengs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserIntrests_UserId",
                table: "UserIntrests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguages_UserId",
                table: "UserLanguages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_UserId",
                table: "UserSkills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStrengs_UserId",
                table: "UserStrengs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserIntrests");

            migrationBuilder.DropTable(
                name: "UserLanguages");

            migrationBuilder.DropTable(
                name: "UserSkills");

            migrationBuilder.DropTable(
                name: "UserStrengs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AspNetUsers");
        }
    }
}
