using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChecklistVeiculos.Migrations
{
    /// <inheritdoc />
#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    public partial class first : Migration
#pragma warning restore CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChecklistVeiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlacaVeiculo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescricaoVeiculo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Executor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistVeiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistVeiculoItens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ChecklistVeiculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistVeiculoItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistVeiculoItens_ChecklistVeiculos_ChecklistVeiculoId",
                        column: x => x.ChecklistVeiculoId,
                        principalTable: "ChecklistVeiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistVeiculoObservacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChecklistVeiculoItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistVeiculoObservacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistVeiculoObservacoes_ChecklistVeiculoItens_ChecklistVeiculoItemId",
                        column: x => x.ChecklistVeiculoItemId,
                        principalTable: "ChecklistVeiculoItens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistVeiculoItens_ChecklistVeiculoId",
                table: "ChecklistVeiculoItens",
                column: "ChecklistVeiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistVeiculoObservacoes_ChecklistVeiculoItemId",
                table: "ChecklistVeiculoObservacoes",
                column: "ChecklistVeiculoItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistVeiculoObservacoes");

            migrationBuilder.DropTable(
                name: "ChecklistVeiculoItens");

            migrationBuilder.DropTable(
                name: "ChecklistVeiculos");
        }
    }
}
