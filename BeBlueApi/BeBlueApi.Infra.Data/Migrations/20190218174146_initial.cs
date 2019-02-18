using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeBlueApi.Infra.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicGender",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicGender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SalesDate = table.Column<DateTime>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalCashback = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cashback",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdGender = table.Column<Guid>(nullable: false),
                    WeekDay = table.Column<int>(nullable: false),
                    Percent = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cashback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cashback_MusicGender_IdGender",
                        column: x => x.IdGender,
                        principalTable: "MusicGender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscMusic",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    IdGender = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscMusic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscMusic_MusicGender_IdGender",
                        column: x => x.IdGender,
                        principalTable: "MusicGender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdSales = table.Column<Guid>(nullable: false),
                    IdItem = table.Column<Guid>(nullable: false),
                    DiscName = table.Column<string>(maxLength: 250, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    PriceUnit = table.Column<decimal>(nullable: false),
                    SalesPrice = table.Column<decimal>(nullable: false),
                    Cashback = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesLine_DiscMusic_IdItem",
                        column: x => x.IdItem,
                        principalTable: "DiscMusic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesLine_Sales_IdSales",
                        column: x => x.IdSales,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cashback_IdGender",
                table: "Cashback",
                column: "IdGender");

            migrationBuilder.CreateIndex(
                name: "IX_DiscMusic_IdGender",
                table: "DiscMusic",
                column: "IdGender");

            migrationBuilder.CreateIndex(
                name: "IX_SalesLine_IdItem",
                table: "SalesLine",
                column: "IdItem");

            migrationBuilder.CreateIndex(
                name: "IX_SalesLine_IdSales",
                table: "SalesLine",
                column: "IdSales");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cashback");

            migrationBuilder.DropTable(
                name: "SalesLine");

            migrationBuilder.DropTable(
                name: "DiscMusic");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "MusicGender");
        }
    }
}
