using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Waterer.Api.Migrations
{
    public partial class StatusesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_plantImages_Plants_PlantId",
                table: "plantImages");

            migrationBuilder.DropTable(
                name: "plantStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_plantImages",
                table: "plantImages");

            migrationBuilder.RenameTable(
                name: "plantImages",
                newName: "PlantImages");

            migrationBuilder.RenameIndex(
                name: "IX_plantImages_PlantId",
                table: "PlantImages",
                newName: "IX_PlantImages_PlantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlantImages",
                table: "PlantImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PlantStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Humidity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantStatuses_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantStatuses_PlantId",
                table: "PlantStatuses",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlantImages_Plants_PlantId",
                table: "PlantImages",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantImages_Plants_PlantId",
                table: "PlantImages");

            migrationBuilder.DropTable(
                name: "PlantStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlantImages",
                table: "PlantImages");

            migrationBuilder.RenameTable(
                name: "PlantImages",
                newName: "plantImages");

            migrationBuilder.RenameIndex(
                name: "IX_PlantImages_PlantId",
                table: "plantImages",
                newName: "IX_plantImages_PlantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_plantImages",
                table: "plantImages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "plantStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Humidity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plantStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_plantStates_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_plantStates_PlantId",
                table: "plantStates",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_plantImages_Plants_PlantId",
                table: "plantImages",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
