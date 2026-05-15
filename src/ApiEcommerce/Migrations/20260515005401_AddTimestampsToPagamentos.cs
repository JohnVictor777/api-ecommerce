using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEcommerce.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestampsToPagamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AtualizadoEm",
                table: "Pagamentos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Pagamentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtualizadoEm",
                table: "Pagamentos");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Pagamentos");
        }
    }
}
