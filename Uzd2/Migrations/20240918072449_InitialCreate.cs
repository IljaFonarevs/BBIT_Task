using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uzd2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MajaItems",
                columns: table => new
                {
                    MajaNumurs = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Iela = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pilseta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valsts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PastIndeks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajaItems", x => x.MajaNumurs);
                });

            migrationBuilder.CreateTable(
                name: "DzivoklisItems",
                columns: table => new
                {
                    DzivNumurs = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stavs = table.Column<int>(type: "int", nullable: true),
                    IstabSkaits = table.Column<int>(type: "int", nullable: true),
                    IeiedzSkaits = table.Column<int>(type: "int", nullable: true),
                    Platiba = table.Column<double>(type: "float", nullable: true),
                    DzivPlatiba = table.Column<double>(type: "float", nullable: true),
                    MajaID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DzivoklisItems", x => x.DzivNumurs);
                    table.ForeignKey(
                        name: "FK_DzivoklisItems_MajaItems_MajaID",
                        column: x => x.MajaID,
                        principalTable: "MajaItems",
                        principalColumn: "MajaNumurs",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IedzivotajsItems",
                columns: table => new
                {
                    PersKods = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vards = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Uzvards = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DzivNumurs = table.Column<long>(type: "bigint", nullable: false),
                    DzimDat = table.Column<DateOnly>(type: "date", nullable: true),
                    DzivoklisDzivNumurs = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IedzivotajsItems", x => x.PersKods);
                    table.ForeignKey(
                        name: "FK_IedzivotajsItems_DzivoklisItems_DzivoklisDzivNumurs",
                        column: x => x.DzivoklisDzivNumurs,
                        principalTable: "DzivoklisItems",
                        principalColumn: "DzivNumurs");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DzivoklisItems_MajaID",
                table: "DzivoklisItems",
                column: "MajaID");

            migrationBuilder.CreateIndex(
                name: "IX_IedzivotajsItems_DzivoklisDzivNumurs",
                table: "IedzivotajsItems",
                column: "DzivoklisDzivNumurs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IedzivotajsItems");

            migrationBuilder.DropTable(
                name: "DzivoklisItems");

            migrationBuilder.DropTable(
                name: "MajaItems");
        }
    }
}
