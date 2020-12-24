using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehouse.WebApi.Migrations
{
    public partial class DefaultWarehouses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_terminals_warehouses_warehouse_id",
                table: "terminals");

            migrationBuilder.AlterColumn<string>(
                name: "warehouse_id",
                table: "terminals",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_terminals_warehouses_warehouse_id",
                table: "terminals",
                column: "warehouse_id",
                principalTable: "warehouses",
                principalColumn: "id");

            migrationBuilder.Sql(@"INSERT INTO warehouses (id, name) VALUES
                                  ('1111111-0000', 'Moscow.One'),
                                  ('1111112-0000', 'Moscow.Two'),
                                  ('2222222-0000', 'Spb.One')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM warehouses WHERE id IN ('1111111-0000', '1111112-0000', '2222222-0000')");

            migrationBuilder.DropForeignKey(
                name: "fk_terminals_warehouses_warehouse_id",
                table: "terminals");

            migrationBuilder.AlterColumn<string>(
                name: "warehouse_id",
                table: "terminals",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "fk_terminals_warehouses_warehouse_id",
                table: "terminals",
                column: "warehouse_id",
                principalTable: "warehouses",
                principalColumn: "id");
        }
    }
}
