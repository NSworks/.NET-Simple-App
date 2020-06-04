using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KolProbne.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klienci",
                columns: table => new
                {
                    IdKlient = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klienci", x => x.IdKlient);
                });

            migrationBuilder.CreateTable(
                name: "Pracownicy",
                columns: table => new
                {
                    IdPracownik = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pracownicy", x => x.IdPracownik);
                });

            migrationBuilder.CreateTable(
                name: "WyrobCukiernicze",
                columns: table => new
                {
                    IdWyrobuCukierniczego = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(maxLength: 200, nullable: false),
                    CenaZaSzt = table.Column<float>(nullable: false),
                    Typ = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WyrobCukiernicze", x => x.IdWyrobuCukierniczego);
                });

            migrationBuilder.CreateTable(
                name: "Zamowienia",
                columns: table => new
                {
                    IdZamowienia = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataPrzyjecia = table.Column<DateTime>(nullable: false),
                    DataRealizacji = table.Column<DateTime>(nullable: false),
                    Uwagi = table.Column<string>(maxLength: 300, nullable: true),
                    IdPracownik = table.Column<int>(nullable: false),
                    IdKlient = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienia", x => x.IdZamowienia);
                    table.ForeignKey(
                        name: "FK_Zamowienia_Klienci_IdKlient",
                        column: x => x.IdKlient,
                        principalTable: "Klienci",
                        principalColumn: "IdKlient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zamowienia_Pracownicy_IdPracownik",
                        column: x => x.IdPracownik,
                        principalTable: "Pracownicy",
                        principalColumn: "IdPracownik",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zamowienie_WyrobyCukiernicze",
                columns: table => new
                {
                    IdWyrobuCukierniczego = table.Column<int>(nullable: false),
                    IdZamowienia = table.Column<int>(nullable: false),
                    Ilosc = table.Column<int>(nullable: false),
                    Uwagi = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zamowienie_WyrobyCukiernicze", x => new { x.IdWyrobuCukierniczego, x.IdZamowienia });
                    table.ForeignKey(
                        name: "FK_Zamowienie_WyrobyCukiernicze_WyrobCukiernicze_IdWyrobuCukierniczego",
                        column: x => x.IdWyrobuCukierniczego,
                        principalTable: "WyrobCukiernicze",
                        principalColumn: "IdWyrobuCukierniczego",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zamowienie_WyrobyCukiernicze_Zamowienia_IdZamowienia",
                        column: x => x.IdZamowienia,
                        principalTable: "Zamowienia",
                        principalColumn: "IdZamowienia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Klienci",
                columns: new[] { "IdKlient", "Imie", "Nazwisko" },
                values: new object[,]
                {
                    { 1, "Klient1", "1" },
                    { 2, "Klient2", "2" },
                    { 3, "Klient3", "3" }
                });

            migrationBuilder.InsertData(
                table: "Pracownicy",
                columns: new[] { "IdPracownik", "Imie", "Nazwisko" },
                values: new object[,]
                {
                    { 1, "Pracownik1", "1" },
                    { 2, "Pracownik2", "2" },
                    { 3, "Pracownik3", "3" }
                });

            migrationBuilder.InsertData(
                table: "WyrobCukiernicze",
                columns: new[] { "IdWyrobuCukierniczego", "CenaZaSzt", "Nazwa", "Typ" },
                values: new object[,]
                {
                    { 1, 1f, "Wyrob1", "Typ1" },
                    { 2, 2f, "Wyrob2", "Typ2" },
                    { 3, 3f, "Wyrob3", "Typ3" }
                });

            migrationBuilder.InsertData(
                table: "Zamowienia",
                columns: new[] { "IdZamowienia", "DataPrzyjecia", "DataRealizacji", "IdKlient", "IdPracownik", "Uwagi" },
                values: new object[] { 1, new DateTime(2020, 6, 1, 18, 25, 29, 863, DateTimeKind.Local).AddTicks(9286), new DateTime(2020, 6, 1, 18, 25, 29, 866, DateTimeKind.Local).AddTicks(8821), 1, 1, "brak1" });

            migrationBuilder.InsertData(
                table: "Zamowienia",
                columns: new[] { "IdZamowienia", "DataPrzyjecia", "DataRealizacji", "IdKlient", "IdPracownik", "Uwagi" },
                values: new object[] { 2, new DateTime(2020, 6, 1, 18, 25, 29, 867, DateTimeKind.Local).AddTicks(179), new DateTime(2020, 6, 1, 18, 25, 29, 867, DateTimeKind.Local).AddTicks(196), 2, 2, "brak2" });

            migrationBuilder.InsertData(
                table: "Zamowienia",
                columns: new[] { "IdZamowienia", "DataPrzyjecia", "DataRealizacji", "IdKlient", "IdPracownik", "Uwagi" },
                values: new object[] { 3, new DateTime(2020, 6, 1, 18, 25, 29, 867, DateTimeKind.Local).AddTicks(220), new DateTime(2020, 6, 1, 18, 25, 29, 867, DateTimeKind.Local).AddTicks(223), 3, 3, "brak3" });

            migrationBuilder.InsertData(
                table: "Zamowienie_WyrobyCukiernicze",
                columns: new[] { "IdWyrobuCukierniczego", "IdZamowienia", "Ilosc", "Uwagi" },
                values: new object[] { 1, 1, 1, "Uwaga1" });

            migrationBuilder.InsertData(
                table: "Zamowienie_WyrobyCukiernicze",
                columns: new[] { "IdWyrobuCukierniczego", "IdZamowienia", "Ilosc", "Uwagi" },
                values: new object[] { 2, 2, 2, "Uwag2" });

            migrationBuilder.InsertData(
                table: "Zamowienie_WyrobyCukiernicze",
                columns: new[] { "IdWyrobuCukierniczego", "IdZamowienia", "Ilosc", "Uwagi" },
                values: new object[] { 3, 3, 3, "Uwaga3" });

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienia_IdKlient",
                table: "Zamowienia",
                column: "IdKlient");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienia_IdPracownik",
                table: "Zamowienia",
                column: "IdPracownik");

            migrationBuilder.CreateIndex(
                name: "IX_Zamowienie_WyrobyCukiernicze_IdZamowienia",
                table: "Zamowienie_WyrobyCukiernicze",
                column: "IdZamowienia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zamowienie_WyrobyCukiernicze");

            migrationBuilder.DropTable(
                name: "WyrobCukiernicze");

            migrationBuilder.DropTable(
                name: "Zamowienia");

            migrationBuilder.DropTable(
                name: "Klienci");

            migrationBuilder.DropTable(
                name: "Pracownicy");
        }
    }
}