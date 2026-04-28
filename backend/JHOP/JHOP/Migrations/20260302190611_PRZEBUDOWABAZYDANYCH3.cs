using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class PRZEBUDOWABAZYDANYCH3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfilePersonalDatas",
                columns: table => new
                {
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PersonalProfile = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: false),
                    GitHubURL = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: false),
                    LinkedInURL = table.Column<string>(type: "nvarchar(550)", maxLength: 550, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfilePersonalDatas", x => x.ProfileId);
                    table.ForeignKey(
                        name: "FK_UserProfilePersonalDatas_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilePersonalDatas_ProfileId",
                table: "UserProfilePersonalDatas",
                column: "ProfileId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfilePersonalDatas");
        }
    }
}
