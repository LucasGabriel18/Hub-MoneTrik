using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hub.MoneTrik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovendoColunaDeDataDaTabelaDespesas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_vencimento",
                table: "despesas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "data_vencimento",
                table: "despesas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
