using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TuristickaAgencija.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Destinacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", nullable: false),
                    Drzava = table.Column<string>(type: "TEXT", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: false),
                    Popularna = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinacije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aranzmani",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: false),
                    Cijena = table.Column<decimal>(type: "TEXT", nullable: false),
                    DatumPolaska = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DatumPovratka = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BrojDana = table.Column<int>(type: "INTEGER", nullable: false),
                    DestinacijaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aranzmani", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aranzmani_Destinacije_DestinacijaId",
                        column: x => x.DestinacijaId,
                        principalTable: "Destinacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aranzmani_DestinacijaId",
                table: "Aranzmani",
                column: "DestinacijaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aranzmani");

            migrationBuilder.DropTable(
                name: "Destinacije");
        }
    }
}
