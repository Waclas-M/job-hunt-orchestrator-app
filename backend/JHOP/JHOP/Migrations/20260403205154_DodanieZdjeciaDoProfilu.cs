using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class DodanieZdjeciaDoProfilu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfilePhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    Sha256 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfilePhoto_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePhoto_ProfileId",
                table: "ProfilePhoto",
                column: "ProfileId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfilePhoto");
        }
    }
}
