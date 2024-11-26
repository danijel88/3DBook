using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DBook.Migrations
{
    /// <inheritdoc />
    public partial class AddAvatarToChild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Children",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ChildImages_ChildId",
                table: "ChildImages",
                column: "ChildId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChildImages_Children_ChildId",
                table: "ChildImages",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChildImages_Children_ChildId",
                table: "ChildImages");

            migrationBuilder.DropIndex(
                name: "IX_ChildImages_ChildId",
                table: "ChildImages");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Children");
        }
    }
}
