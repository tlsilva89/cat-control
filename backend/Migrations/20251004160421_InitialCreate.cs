using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CatControl.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Categoria = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ValorMensal = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Ano = table.Column<int>(type: "integer", nullable: false),
                    Mes = table.Column<int>(type: "integer", nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Raca = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Cor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Sexo = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Castrado = table.Column<bool>(type: "boolean", nullable: false),
                    Peso = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    NumeroMicrochip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FotoUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    NomeProduto = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Categoria = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    QuantidadeAtual = table.Column<int>(type: "integer", nullable: false),
                    QuantidadeMinima = table.Column<int>(type: "integer", nullable: false),
                    Unidade = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    DataValidade = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PrecoUnitario = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    ConsumoMedioDiario = table.Column<decimal>(type: "numeric", nullable: true),
                    Marca = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dewormings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CatId = table.Column<int>(type: "integer", nullable: false),
                    Medicamento = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataAplicacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProximaAplicacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Dosagem = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    PesoGatoNaData = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dewormings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dewormings_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CatId = table.Column<int>(type: "integer", nullable: true),
                    Descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Categoria = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    DataGasto = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FormaPagamento = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Recorrente = table.Column<bool>(type: "boolean", nullable: false),
                    FrequenciaRecorrencia = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finances_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Finances_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hygienes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CatId = table.Column<int>(type: "integer", nullable: false),
                    TipoCuidado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DataRealizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProximoAgendamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FrequenciaDias = table.Column<int>(type: "integer", nullable: true),
                    ProdutoUtilizado = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CatId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hygienes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hygienes_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hygienes_Cats_CatId1",
                        column: x => x.CatId1,
                        principalTable: "Cats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CatId = table.Column<int>(type: "integer", nullable: true),
                    Tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Mensagem = table.Column<string>(type: "text", nullable: false),
                    DataNotificacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Lida = table.Column<bool>(type: "boolean", nullable: false),
                    Prioridade = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ReferenciaId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CatId = table.Column<int>(type: "integer", nullable: false),
                    TipoVacina = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataAplicacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProximaAplicacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LocalAplicacao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Veterinario = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Valor = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaccines_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VeterinaryVisits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CatId = table.Column<int>(type: "integer", nullable: false),
                    DataConsulta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Veterinario = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Clinica = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    MotivoConsulta = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Diagnostico = table.Column<string>(type: "text", nullable: true),
                    Tratamento = table.Column<string>(type: "text", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    ProximaConsulta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CatId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeterinaryVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeterinaryVisits_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeterinaryVisits_Cats_CatId1",
                        column: x => x.CatId1,
                        principalTable: "Cats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeightHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CatId = table.Column<int>(type: "integer", nullable: false),
                    Peso = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    DataPesagem = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CatId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeightHistories_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeightHistories_Cats_CatId1",
                        column: x => x.CatId1,
                        principalTable: "Cats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CatId = table.Column<int>(type: "integer", nullable: true),
                    NomeProduto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Categoria = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PrecoEstimado = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    Prioridade = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LinkProduto = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Loja = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Comprado = table.Column<bool>(type: "boolean", nullable: false),
                    DataCompra = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Observacoes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Wishlists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cats_UserId",
                table: "Cats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dewormings_CatId",
                table: "Dewormings",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_Finances_CatId",
                table: "Finances",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_Finances_UserId_DataGasto",
                table: "Finances",
                columns: new[] { "UserId", "DataGasto" });

            migrationBuilder.CreateIndex(
                name: "IX_Hygienes_CatId",
                table: "Hygienes",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_Hygienes_CatId1",
                table: "Hygienes",
                column: "CatId1");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CatId",
                table: "Notifications",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_Lida",
                table: "Notifications",
                columns: new[] { "UserId", "Lida" });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_UserId_Categoria",
                table: "Stocks",
                columns: new[] { "UserId", "Categoria" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_CatId",
                table: "Vaccines",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinaryVisits_CatId",
                table: "VeterinaryVisits",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_VeterinaryVisits_CatId1",
                table: "VeterinaryVisits",
                column: "CatId1");

            migrationBuilder.CreateIndex(
                name: "IX_WeightHistories_CatId",
                table: "WeightHistories",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightHistories_CatId1",
                table: "WeightHistories",
                column: "CatId1");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_CatId",
                table: "Wishlists",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Dewormings");

            migrationBuilder.DropTable(
                name: "Finances");

            migrationBuilder.DropTable(
                name: "Hygienes");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Vaccines");

            migrationBuilder.DropTable(
                name: "VeterinaryVisits");

            migrationBuilder.DropTable(
                name: "WeightHistories");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "Cats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
