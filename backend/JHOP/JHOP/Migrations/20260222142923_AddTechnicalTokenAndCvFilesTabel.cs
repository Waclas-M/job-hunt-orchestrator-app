using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JHOP.Migrations
{
    /// <inheritdoc />
    public partial class AddTechnicalTokenAndCvFilesTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CvFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sha256 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CvFiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CvFiles_UserId",
                table: "CvFiles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CvFiles");
        }
    }
}
