using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEARLINGO.Migrations
{
    /// <inheritdoc />
    public partial class updateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDDangNhap",
                table: "NguoiDung");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IDDangNhap",
                table: "NguoiDung",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
