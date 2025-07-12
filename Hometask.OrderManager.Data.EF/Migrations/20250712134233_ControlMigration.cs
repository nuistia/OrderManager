using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hometask.OrderManager.Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class ControlMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    gr_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    gr_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    gr_temp = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Groups__2BC0F88E02FCCB78", x => x.gr_id);
                });

            migrationBuilder.CreateTable(
                name: "Analysis",
                columns: table => new
                {
                    an_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    an_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    an_cost = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    an_price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    an_group = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Analysis__831DABF3BC7A571F", x => x.an_id);
                    table.ForeignKey(
                        name: "FK_Analysis_Groups",
                        column: x => x.an_group,
                        principalTable: "Groups",
                        principalColumn: "gr_id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ord_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ord_datetime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ord_an = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__DC39D7DF5F23AF78", x => x.ord_id);
                    table.ForeignKey(
                        name: "FK_Orders_Analysis",
                        column: x => x.ord_an,
                        principalTable: "Analysis",
                        principalColumn: "an_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analysis_an_group",
                table: "Analysis",
                column: "an_group");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ord_an",
                table: "Orders",
                column: "ord_an");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Analysis");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
