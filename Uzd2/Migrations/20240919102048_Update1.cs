using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uzd2.Migrations
{
    /// <inheritdoc />
    public partial class Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Valsts",
                table: "MajaItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pilseta",
                table: "MajaItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PastIndeks",
                table: "MajaItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Iela",
                table: "MajaItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Vards",
                table: "IedzivotajsItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Uzvards",
                table: "IedzivotajsItems",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "IedzivotajsItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<double>(
                name: "Platiba",
                table: "DzivoklisItems",
                type: "float(18)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DzivPlatiba",
                table: "DzivoklisItems",
                type: "float(18)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DzivoklisIedzivotajs",
                columns: table => new
                {
                    DzivoklisNumurs = table.Column<long>(type: "bigint", nullable: false),
                    IedzivotajsKods = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DzivoklisIedzivotajs", x => new { x.DzivoklisNumurs, x.IedzivotajsKods });
                    table.ForeignKey(
                        name: "FK_DzivoklisIedzivotajs_DzivoklisItems_DzivoklisNumurs",
                        column: x => x.DzivoklisNumurs,
                        principalTable: "DzivoklisItems",
                        principalColumn: "DzivNumurs",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DzivoklisIedzivotajs_IedzivotajsItems_IedzivotajsKods",
                        column: x => x.IedzivotajsKods,
                        principalTable: "IedzivotajsItems",
                        principalColumn: "PersKods",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DzivoklisIedzivotajs_IedzivotajsKods",
                table: "DzivoklisIedzivotajs",
                column: "IedzivotajsKods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DzivoklisIedzivotajs");

            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "IedzivotajsItems");

            migrationBuilder.AlterColumn<string>(
                name: "Valsts",
                table: "MajaItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pilseta",
                table: "MajaItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PastIndeks",
                table: "MajaItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Iela",
                table: "MajaItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Vards",
                table: "IedzivotajsItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Uzvards",
                table: "IedzivotajsItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<double>(
                name: "Platiba",
                table: "DzivoklisItems",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DzivPlatiba",
                table: "DzivoklisItems",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float(18)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);
        }
    }
}
