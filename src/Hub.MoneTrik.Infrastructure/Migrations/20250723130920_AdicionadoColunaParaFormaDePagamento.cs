using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hub.MoneTrik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoColunaParaFormaDePagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "forma_pagamento",
                table: "parcelas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "forma_pagamento",
                table: "parcelas");
        }
    }
}
