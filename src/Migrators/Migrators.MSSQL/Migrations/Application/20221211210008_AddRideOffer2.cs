using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class AddRideOffer2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideOffer_Transactions_TransactionId",
                schema: "Catalog",
                table: "RideOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RideOffer",
                schema: "Catalog",
                table: "RideOffer");

            migrationBuilder.RenameTable(
                name: "RideOffer",
                schema: "Catalog",
                newName: "RideOffers",
                newSchema: "Blockchain");

            migrationBuilder.RenameIndex(
                name: "IX_RideOffer_TransactionId",
                schema: "Blockchain",
                table: "RideOffers",
                newName: "IX_RideOffers_TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RideOffers",
                schema: "Blockchain",
                table: "RideOffers",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_RideOffers",
                schema: "Blockchain",
                table: "RideOffers");

            migrationBuilder.RenameTable(
                name: "RideOffers",
                schema: "Blockchain",
                newName: "RideOffer",
                newSchema: "Catalog");

            migrationBuilder.RenameIndex(
                name: "IX_RideOffers_TransactionId",
                schema: "Catalog",
                table: "RideOffer",
                newName: "IX_RideOffer_TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RideOffer",
                schema: "Catalog",
                table: "RideOffer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RideOffer_Transactions_TransactionId",
                schema: "Catalog",
                table: "RideOffer",
                column: "TransactionId",
                principalSchema: "Blockchain",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
