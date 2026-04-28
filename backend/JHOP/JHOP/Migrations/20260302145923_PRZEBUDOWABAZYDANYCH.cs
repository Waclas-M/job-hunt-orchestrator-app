using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class PRZEBUDOWABAZYDANYCH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEducations_AspNetUsers_UserId",
                table: "UserEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserJobExperiences_AspNetUsers_UserId",
                table: "UserJobExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguages_AspNetUsers_UserId",
                table: "UserLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_AspNetUsers_UserId",
                table: "UserSkills");

            migrationBuilder.DropTable(
                name: "UserIntrests");

            migrationBuilder.DropTable(
                name: "UserStrengs");

            migrationBuilder.DropIndex(
                name: "IX_UserSkills_UserId",
                table: "UserSkills");

            migrationBuilder.DropIndex(
                name: "IX_UserLanguages_UserId",
                table: "UserLanguages");

            migrationBuilder.DropIndex(
                name: "IX_UserJobExperiences_UserId",
                table: "UserJobExperiences");

            migrationBuilder.DropIndex(
                name: "IX_UserEducations_UserId",
                table: "UserEducations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserSkills");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserLanguages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserJobExperiences");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserEducations");

            migrationBuilder.AlterColumn<string>(
                name: "Skill",
                table: "UserSkills",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "UserSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "UserLanguages",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "UserLanguages",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "UserLanguages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "UserJobExperiences",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "JobDescription",
                table: "UserJobExperiences",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1100)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "UserJobExperiences",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "UserJobExperiences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "StudyProfile",
                table: "UserEducations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "SchoolName",
                table: "UserEducations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "UserEducations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileIndex = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.CheckConstraint("CK_Profiles_ProfileIndex_1_3", "[ProfileIndex] BETWEEN 1 AND 3");
                    table.ForeignKey(
                        name: "FK_Profiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    Interest = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInterests_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserStrengths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStrengths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStrengths_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_ProfileId_Skill",
                table: "UserSkills",
                columns: new[] { "ProfileId", "Skill" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguages_ProfileId_Language",
                table: "UserLanguages",
                columns: new[] { "ProfileId", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserJobExperiences_ProfileId",
                table: "UserJobExperiences",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEducations_ProfileId",
                table: "UserEducations",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId_ProfileIndex",
                table: "Profiles",
                columns: new[] { "UserId", "ProfileIndex" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInterests_ProfileId_Interest",
                table: "UserInterests",
                columns: new[] { "ProfileId", "Interest" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStrengths_ProfileId_Strength",
                table: "UserStrengths",
                columns: new[] { "ProfileId", "Strength" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEducations_Profiles_ProfileId",
                table: "UserEducations",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobExperiences_Profiles_ProfileId",
                table: "UserJobExperiences",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguages_Profiles_ProfileId",
                table: "UserLanguages",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_Profiles_ProfileId",
                table: "UserSkills",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEducations_Profiles_ProfileId",
                table: "UserEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserJobExperiences_Profiles_ProfileId",
                table: "UserJobExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLanguages_Profiles_ProfileId",
                table: "UserLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_Profiles_ProfileId",
                table: "UserSkills");

            migrationBuilder.DropTable(
                name: "UserInterests");

            migrationBuilder.DropTable(
                name: "UserStrengths");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_UserSkills_ProfileId_Skill",
                table: "UserSkills");

            migrationBuilder.DropIndex(
                name: "IX_UserLanguages_ProfileId_Language",
                table: "UserLanguages");

            migrationBuilder.DropIndex(
                name: "IX_UserJobExperiences_ProfileId",
                table: "UserJobExperiences");

            migrationBuilder.DropIndex(
                name: "IX_UserEducations_ProfileId",
                table: "UserEducations");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "UserSkills");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "UserLanguages");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "UserJobExperiences");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "UserEducations");

            migrationBuilder.AlterColumn<string>(
                name: "Skill",
                table: "UserSkills",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserSkills",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "UserLanguages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                table: "UserLanguages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserLanguages",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "UserJobExperiences",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "JobDescription",
                table: "UserJobExperiences",
                type: "nvarchar(1100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)",
                oldMaxLength: 4000);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "UserJobExperiences",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserJobExperiences",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "StudyProfile",
                table: "UserEducations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "SchoolName",
                table: "UserEducations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserEducations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserIntrests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Intrest = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "IX_UserSkills_UserId",
                table: "UserSkills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguages_UserId",
                table: "UserLanguages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserJobExperiences_UserId",
                table: "UserJobExperiences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEducations_UserId",
                table: "UserEducations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIntrests_UserId",
                table: "UserIntrests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStrengs_UserId",
                table: "UserStrengs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEducations_AspNetUsers_UserId",
                table: "UserEducations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobExperiences_AspNetUsers_UserId",
                table: "UserJobExperiences",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLanguages_AspNetUsers_UserId",
                table: "UserLanguages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_AspNetUsers_UserId",
                table: "UserSkills",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
