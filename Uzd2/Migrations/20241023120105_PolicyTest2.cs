using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uzd2.Migrations
{
    /// <inheritdoc />
    public partial class PolicyTest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "apartNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "apartNumber",
                table: "AspNetUsers");
        }
    }
}
