using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hub.MoneTrik.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriandoTabelaDespesas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "despesas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    titulo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    categoria = table.Column<int>(type: "int", nullable: false),
                    tipo = table.Column<int>(type: "int", nullable: false),
                    valor_parcela = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    numero_parcela = table.Column<int>(type: "int", nullable: false),
                    total_parcelas = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    valor_total = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    data_vencimento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_registro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_despesas", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "despesas");
        }
    }
}
