using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace KR.Migrations
{
    public partial class BookStoragePortal3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GivBoocks",
                columns: table => new
                {
                    GivBoockId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GivUserId = table.Column<int>(type: "integer", nullable: false),
                    GivBookId = table.Column<int>(type: "integer", nullable: false),
                    DateGiv = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    GivDateReturn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GivBoocks", x => x.GivBoockId);
                    table.ForeignKey(
                        name: "FK_GivBoocks_Books_GivBookId",
                        column: x => x.GivBookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GivBoocks_Users_GivUserId",
                        column: x => x.GivUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GivBoocks_GivBookId",
                table: "GivBoocks",
                column: "GivBookId");

            migrationBuilder.CreateIndex(
                name: "IX_GivBoocks_GivUserId",
                table: "GivBoocks",
                column: "GivUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GivBoocks");
        }
    }
}
