using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Warehouse.WebApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "warehouses",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    created_on = table.Column<DateTime>(nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_warehouses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "terminals",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    warehouse_id = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_terminals", x => x.id);
                    table.ForeignKey(
                        name: "fk_terminals_warehouses_warehouse_id",
                        column: x => x.warehouse_id,
                        principalTable: "warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_terminals_warehouse_id",
                table: "terminals",
                column: "warehouse_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "terminals");

            migrationBuilder.DropTable(
                name: "warehouses");
        }
    }
}
