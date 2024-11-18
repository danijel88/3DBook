using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DBook.Migrations
{
    /// <inheritdoc />
    public partial class removePathAddedbyMistake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Children");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Children",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
