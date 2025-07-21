using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hub.MoneTrik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoTabelaParcelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numero_parcela",
                table: "despesas");

            migrationBuilder.DropColumn(
                name: "situacao",
                table: "despesas");

            migrationBuilder.DropColumn(
                name: "valor_parcela",
                table: "despesas");

            migrationBuilder.CreateTable(
                name: "parcelas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    despesa_id = table.Column<int>(type: "int", nullable: false),
                    numero_parcela = table.Column<int>(type: "int", nullable: false),
                    valor_parcela = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    data_vencimento = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    situacao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parcelas", x => x.id);
                    table.ForeignKey(
                        name: "FK_parcelas_despesas_despesa_id",
                        column: x => x.despesa_id,
                        principalTable: "despesas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_parcelas_despesa_id",
                table: "parcelas",
                column: "despesa_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "parcelas");

            migrationBuilder.AddColumn<int>(
                name: "numero_parcela",
                table: "despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "situacao",
                table: "despesas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "valor_parcela",
                table: "despesas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
