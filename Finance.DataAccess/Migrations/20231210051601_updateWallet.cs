using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Spendings");

            migrationBuilder.AlterColumn<string>(
                name: "ID_Card",
                table: "Wallets",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Wallets",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Wallets");

            migrationBuilder.AlterColumn<long>(
                name: "ID_Card",
                table: "Wallets",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Spendings",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
