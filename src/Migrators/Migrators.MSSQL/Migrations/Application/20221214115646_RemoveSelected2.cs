using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class RemoveSelected2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideOffers_Transactions_TransactionId",
                schema: "Blockchain",
                table: "RideOffers");

            migrationBuilder.AddForeignKey(
                name: "FK_RideOffers_Transactions_TransactionId",
                schema: "Blockchain",
                table: "RideOffers",
                column: "TransactionId",
                principalSchema: "Blockchain",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideOffers_Transactions_TransactionId",
                schema: "Blockchain",
                table: "RideOffers");

            migrationBuilder.AddForeignKey(
                name: "FK_RideOffers_Transactions_TransactionId",
                schema: "Blockchain",
                table: "RideOffers",
                column: "TransactionId",
                principalSchema: "Blockchain",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}
