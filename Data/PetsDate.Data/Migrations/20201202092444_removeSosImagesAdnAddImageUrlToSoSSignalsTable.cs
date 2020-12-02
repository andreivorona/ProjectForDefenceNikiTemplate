using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PetsDate.Data.Migrations
{
    public partial class removeSosImagesAdnAddImageUrlToSoSSignalsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SosImages");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "SosSignals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "SosSignals");

            migrationBuilder.CreateTable(
                name: "SosImages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SosSignalId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SosImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SosImages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SosImages_SosSignals_SosSignalId",
                        column: x => x.SosSignalId,
                        principalTable: "SosSignals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SosImages_SosSignalId",
                table: "SosImages",
                column: "SosSignalId");

            migrationBuilder.CreateIndex(
                name: "IX_SosImages_UserId",
                table: "SosImages",
                column: "UserId");
        }
    }
}
