using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DBook.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEnterAndExitToint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Exit",
                table: "Folders",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,3)");

            migrationBuilder.AlterColumn<int>(
                name: "Enter",
                table: "Folders",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,3)");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_MachineId",
                table: "Folders",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Machines_MachineId",
                table: "Folders",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Machines_MachineId",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_MachineId",
                table: "Folders");

            migrationBuilder.AlterColumn<decimal>(
                name: "Exit",
                table: "Folders",
                type: "decimal(8,3)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Enter",
                table: "Folders",
                type: "decimal(8,3)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
