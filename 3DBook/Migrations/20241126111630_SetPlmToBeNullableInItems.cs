using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3DBook.Migrations
{
    /// <inheritdoc />
    public partial class SetPlmToBeNullableInItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Plm",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsImages_ItemId",
                table: "ItemsImages",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MachineId",
                table: "Items",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ItemsTypes_ItemTypeId",
                table: "Items",
                column: "ItemTypeId",
                principalTable: "ItemsTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Machines_MachineId",
                table: "Items",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsImages_Items_ItemId",
                table: "ItemsImages",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ItemsTypes_ItemTypeId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Machines_MachineId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsImages_Items_ItemId",
                table: "ItemsImages");

            migrationBuilder.DropIndex(
                name: "IX_ItemsImages_ItemId",
                table: "ItemsImages");

            migrationBuilder.DropIndex(
                name: "IX_Items_ItemTypeId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_MachineId",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "Plm",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
