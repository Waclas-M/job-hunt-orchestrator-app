using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class PRZEBUDOWABAZYDANYCH_Zmiany2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfilePersonalDatas_Profiles_ProfileId",
                table: "UserProfilePersonalDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfilePersonalDatas",
                table: "UserProfilePersonalDatas");

            migrationBuilder.RenameTable(
                name: "UserProfilePersonalDatas",
                newName: "UserProfilePersonalData");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfilePersonalDatas_ProfileId",
                table: "UserProfilePersonalData",
                newName: "IX_UserProfilePersonalData_ProfileId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "UserProfilePersonalData",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PersonalProfile",
                table: "UserProfilePersonalData",
                type: "nvarchar(550)",
                maxLength: 550,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(550)",
                oldMaxLength: 550);

            migrationBuilder.AlterColumn<string>(
                name: "LinkedInURL",
                table: "UserProfilePersonalData",
                type: "nvarchar(550)",
                maxLength: 550,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(550)",
                oldMaxLength: 550);

            migrationBuilder.AlterColumn<string>(
                name: "GitHubURL",
                table: "UserProfilePersonalData",
                type: "nvarchar(550)",
                maxLength: 550,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(550)",
                oldMaxLength: 550);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfilePersonalData",
                table: "UserProfilePersonalData",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfilePersonalData_Profiles_ProfileId",
                table: "UserProfilePersonalData",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfilePersonalData_Profiles_ProfileId",
                table: "UserProfilePersonalData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfilePersonalData",
                table: "UserProfilePersonalData");

            migrationBuilder.RenameTable(
                name: "UserProfilePersonalData",
                newName: "UserProfilePersonalDatas");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfilePersonalData_ProfileId",
                table: "UserProfilePersonalDatas",
                newName: "IX_UserProfilePersonalDatas_ProfileId");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "UserProfilePersonalDatas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PersonalProfile",
                table: "UserProfilePersonalDatas",
                type: "nvarchar(550)",
                maxLength: 550,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(550)",
                oldMaxLength: 550,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LinkedInURL",
                table: "UserProfilePersonalDatas",
                type: "nvarchar(550)",
                maxLength: 550,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(550)",
                oldMaxLength: 550,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GitHubURL",
                table: "UserProfilePersonalDatas",
                type: "nvarchar(550)",
                maxLength: 550,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(550)",
                oldMaxLength: 550,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfilePersonalDatas",
                table: "UserProfilePersonalDatas",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfilePersonalDatas_Profiles_ProfileId",
                table: "UserProfilePersonalDatas",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
