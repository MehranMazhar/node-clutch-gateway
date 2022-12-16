using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class AddProveArrived : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProveArriveds",
                schema: "Blockchain",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RideId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveArriveds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProveArriveds_Rides_RideId",
                        column: x => x.RideId,
                        principalSchema: "Blockchain",
                        principalTable: "Rides",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProveArriveds_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalSchema: "Blockchain",
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProveArriveds_RideId",
                schema: "Blockchain",
                table: "ProveArriveds",
                column: "RideId");

            migrationBuilder.CreateIndex(
                name: "IX_ProveArriveds_TransactionId",
                schema: "Blockchain",
                table: "ProveArriveds",
                column: "TransactionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProveArriveds",
                schema: "Blockchain");
        }
    }
}
