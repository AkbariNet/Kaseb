using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KasebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Main2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUri",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageUri",
                table: "AspNetUsers");
        }
    }
}
