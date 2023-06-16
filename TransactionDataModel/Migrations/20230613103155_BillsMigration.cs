using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TransactionDataModel.Migrations
{
    /// <inheritdoc />
    public partial class BillsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillType);
                });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "BillType", "Amount" },
                values: new object[,]
                {
                    { 20, 4L },
                    { 50, 4L },
                    { 100, 4L },
                    { 200, 4L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_Amount",
                table: "Bills",
                column: "Amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");
        }
    }
}
