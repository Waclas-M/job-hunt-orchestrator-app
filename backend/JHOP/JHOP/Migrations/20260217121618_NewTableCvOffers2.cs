using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class NewTableCvOffers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserCvOfferId",
                table: "UserJobExperiences",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserCvOfferId",
                table: "UserEducations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserCvOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OfferURL = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCvOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCvOffers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserJobExperiences_UserCvOfferId",
                table: "UserJobExperiences",
                column: "UserCvOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEducations_UserCvOfferId",
                table: "UserEducations",
                column: "UserCvOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCvOffers_UserId",
                table: "UserCvOffers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEducations_UserCvOffers_UserCvOfferId",
                table: "UserEducations",
                column: "UserCvOfferId",
                principalTable: "UserCvOffers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobExperiences_UserCvOffers_UserCvOfferId",
                table: "UserJobExperiences",
                column: "UserCvOfferId",
                principalTable: "UserCvOffers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEducations_UserCvOffers_UserCvOfferId",
                table: "UserEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserJobExperiences_UserCvOffers_UserCvOfferId",
                table: "UserJobExperiences");

            migrationBuilder.DropTable(
                name: "UserCvOffers");

            migrationBuilder.DropIndex(
                name: "IX_UserJobExperiences_UserCvOfferId",
                table: "UserJobExperiences");

            migrationBuilder.DropIndex(
                name: "IX_UserEducations_UserCvOfferId",
                table: "UserEducations");

            migrationBuilder.DropColumn(
                name: "UserCvOfferId",
                table: "UserJobExperiences");

            migrationBuilder.DropColumn(
                name: "UserCvOfferId",
                table: "UserEducations");
        }
    }
}
