using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Finance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<int>(type: "INTEGER", nullable: false),
                    Mail = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ID_Card = table.Column<long>(type: "INTEGER", nullable: false),
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spendings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false),
                    IdWallet = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spendings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spendings_Wallets_IdWallet",
                        column: x => x.IdWallet,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Mail", "Name", "Password", "Phone", "UserName" },
                values: new object[] { 1, "123 Main Street", "john.doe@example.com", "John Doe", "SamplePassword", 1234567890, "SampleUser" });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "ID_Card", "IdUser" },
                values: new object[,]
                {
                    { 1, 123456789L, 1 },
                    { 2, 2345134553L, 1 }
                });

            migrationBuilder.InsertData(
                table: "Spendings",
                columns: new[] { "Id", "Amount", "Balance", "Description", "IdWallet", "Time" },
                values: new object[,]
                {
                    { 1, 50.00m, 100.00m, 0, 1, new DateTime(2023, 12, 3, 15, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 20.00m, 80.00m, 1, 1, new DateTime(2023, 12, 4, 17, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 30.00m, 50.00m, 2, 1, new DateTime(2023, 12, 4, 20, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 100.00m, 50.00m, 3, 2, new DateTime(2023, 12, 5, 15, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 200.00m, 300.00m, 4, 2, new DateTime(2023, 12, 5, 20, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 25.00m, 275.00m, 5, 2, new DateTime(2023, 12, 6, 8, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 40.00m, 235.00m, 0, 2, new DateTime(2023, 12, 6, 21, 50, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spendings_IdWallet",
                table: "Spendings",
                column: "IdWallet");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_IdUser",
                table: "Wallets",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spendings");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
