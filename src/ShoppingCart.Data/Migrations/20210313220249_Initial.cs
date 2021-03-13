using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cadastros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Sobrenome = table.Column<string>(type: "varchar(80)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(20)", nullable: false),
                    Documento = table.Column<string>(type: "varchar(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cadastros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(50)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(7)", nullable: false),
                    Bairro = table.Column<string>(type: "varchar(35)", nullable: false),
                    Cep = table.Column<string>(type: "varchar(9)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(35)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(35)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(70)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(4000)", nullable: false),
                    Imagem = table.Column<string>(type: "varchar(max)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CadastroId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Cadastros_CadastroId",
                        column: x => x.CadastroId,
                        principalTable: "Cadastros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CadastroEndereco",
                columns: table => new
                {
                    CadastrosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnderecosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadastroEndereco", x => new { x.CadastrosId, x.EnderecosId });
                    table.ForeignKey(
                        name: "FK_CadastroEndereco_Cadastros_CadastrosId",
                        column: x => x.CadastrosId,
                        principalTable: "Cadastros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CadastroEndereco_Enderecos_EnderecosId",
                        column: x => x.EnderecosId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPedidos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPedidos_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CadastroEndereco_EnderecosId",
                table: "CadastroEndereco",
                column: "EnderecosId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_PedidoId",
                table: "ItemPedidos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_ProdutoId",
                table: "ItemPedidos",
                column: "ProdutoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_CadastroId",
                table: "Pedidos",
                column: "CadastroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CadastroEndereco");

            migrationBuilder.DropTable(
                name: "ItemPedidos");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Cadastros");
        }
    }
}
