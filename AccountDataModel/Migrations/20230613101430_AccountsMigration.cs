using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccountDataModel.Migrations
{
    /// <inheritdoc />
    public partial class AccountsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "Balance" },
                values: new object[,]
                {
                    { 177202, 2979m },
                    { 383306, 1194m },
                    { 425555, 4008m },
                    { 525527, 4711m },
                    { 727775, 3239m },
                    { 907010, 4722m },
                    { 994190, 2417m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Balance",
                table: "Accounts",
                column: "Balance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
