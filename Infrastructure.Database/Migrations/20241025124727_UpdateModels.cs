using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_Id",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_Id",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderIds",
                table: "Tables");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuTypeId",
                table: "MenuItems",
                column: "MenuTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_MenuTypeId",
                table: "MenuItems",
                column: "MenuTypeId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_MenuTypeId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_MenuTypeId",
                table: "MenuItems");

            migrationBuilder.AddColumn<string>(
                name: "OrderIds",
                table: "Tables",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_Id",
                table: "MenuItems",
                column: "Id",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_Id",
                table: "OrderItems",
                column: "Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
