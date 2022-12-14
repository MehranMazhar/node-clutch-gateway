using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class AddRideOffersSelected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RideRequestId",
                schema: "Blockchain",
                table: "RideOffers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RideOffers_RideRequestId",
                schema: "Blockchain",
                table: "RideOffers",
                column: "RideRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RideOffers_RideRequests_RideRequestId",
                schema: "Blockchain",
                table: "RideOffers",
                column: "RideRequestId",
                principalSchema: "Blockchain",
                principalTable: "RideRequests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RideOffers_RideRequests_RideRequestId",
                schema: "Blockchain",
                table: "RideOffers");

            migrationBuilder.DropIndex(
                name: "IX_RideOffers_RideRequestId",
                schema: "Blockchain",
                table: "RideOffers");

            migrationBuilder.DropColumn(
                name: "RideRequestId",
                schema: "Blockchain",
                table: "RideOffers");
        }
    }
}
