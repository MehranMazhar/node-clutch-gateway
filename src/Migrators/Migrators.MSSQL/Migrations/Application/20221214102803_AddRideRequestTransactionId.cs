using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class AddRideRequestTransactionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideOffers_Transactions_TransactionId",
                schema: "Blockchain",
                table: "RideOffers");

            migrationBuilder.AddColumn<Guid>(
                name: "RideRequestTransactionId",
                schema: "Blockchain",
                table: "RideOffers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RideOffers_RideRequestTransactionId",
                schema: "Blockchain",
                table: "RideOffers",
                column: "RideRequestTransactionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RideOffers_Transactions_RideRequestTransactionId",
                schema: "Blockchain",
                table: "RideOffers",
                column: "RideRequestTransactionId",
                principalSchema: "Blockchain",
                principalTable: "Transactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RideOffers_Transactions_TransactionId",
                schema: "Blockchain",
                table: "RideOffers",
                column: "TransactionId",
                principalSchema: "Blockchain",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideOffers_Transactions_RideRequestTransactionId",
                schema: "Blockchain",
                table: "RideOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_RideOffers_Transactions_TransactionId",
                schema: "Blockchain",
                table: "RideOffers");

            migrationBuilder.DropIndex(
                name: "IX_RideOffers_RideRequestTransactionId",
                schema: "Blockchain",
                table: "RideOffers");

            migrationBuilder.DropColumn(
                name: "RideRequestTransactionId",
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
    }
}
