using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hub.MoneTrik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoTipoDeDadosDaColunaValoresParaArredondamenteEInteiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "total_parcelas",
                table: "despesas",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "total_parcelas",
                table: "despesas",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
