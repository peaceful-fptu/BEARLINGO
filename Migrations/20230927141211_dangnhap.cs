using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEARLINGO.Migrations
{
    /// <inheritdoc />
    public partial class dangnhap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IDDangNhap",
                table: "NguoiDung",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDDangNhap",
                table: "NguoiDung");
        }
    }
}
