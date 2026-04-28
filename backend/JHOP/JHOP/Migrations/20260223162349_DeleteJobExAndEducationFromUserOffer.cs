using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class DeleteJobExAndEducationFromUserOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEducations_UserCvOffers_UserCvOfferId",
                table: "UserEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserJobExperiences_UserCvOffers_UserCvOfferId",
                table: "UserJobExperiences");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_UserJobExperiences_UserCvOfferId",
                table: "UserJobExperiences",
                column: "UserCvOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEducations_UserCvOfferId",
                table: "UserEducations",
                column: "UserCvOfferId");

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
    }
}
