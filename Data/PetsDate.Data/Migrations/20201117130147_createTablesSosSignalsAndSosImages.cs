namespace PetsDate.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CreateTablesSosSignalsAndSosImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SosSignals",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SosSignals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SosSignals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SosImages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    SosSignalId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SosImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SosImages_SosSignals_SosSignalId",
                        column: x => x.SosSignalId,
                        principalTable: "SosSignals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SosImages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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

            migrationBuilder.CreateIndex(
                name: "IX_SosSignals_IsDeleted",
                table: "SosSignals",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_SosSignals_UserId",
                table: "SosSignals",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SosImages");

            migrationBuilder.DropTable(
                name: "SosSignals");
        }
    }
}
