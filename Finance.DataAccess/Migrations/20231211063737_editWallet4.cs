using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editWallet4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiveWalletId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ActiveWalletId",
                table: "AspNetUsers",
                column: "ActiveWalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Wallets_ActiveWalletId",
                table: "AspNetUsers",
                column: "ActiveWalletId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Wallets_ActiveWalletId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ActiveWalletId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ActiveWalletId",
                table: "AspNetUsers");
        }
    }
}
