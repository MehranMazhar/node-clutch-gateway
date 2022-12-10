using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class AddParentBlockId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousHash",
                schema: "Blockchain",
                table: "Blocks");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentBlockId",
                schema: "Blockchain",
                table: "Blocks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_ParentBlockId",
                schema: "Blockchain",
                table: "Blocks",
                column: "ParentBlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_Blocks_ParentBlockId",
                schema: "Blockchain",
                table: "Blocks",
                column: "ParentBlockId",
                principalSchema: "Blockchain",
                principalTable: "Blocks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Blocks_ParentBlockId",
                schema: "Blockchain",
                table: "Blocks");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_ParentBlockId",
                schema: "Blockchain",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "ParentBlockId",
                schema: "Blockchain",
                table: "Blocks");

            migrationBuilder.AddColumn<string>(
                name: "PreviousHash",
                schema: "Blockchain",
                table: "Blocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
