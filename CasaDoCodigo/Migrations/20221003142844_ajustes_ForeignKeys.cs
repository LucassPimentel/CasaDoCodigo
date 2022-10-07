using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaDoCodigo.Migrations
{
    public partial class ajustes_ForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cadastro_Pedido_PedidoForeignKey",
                table: "Cadastro");

            migrationBuilder.DropIndex(
                name: "IX_Cadastro_PedidoForeignKey",
                table: "Cadastro");

            migrationBuilder.DropColumn(
                name: "PedidoForeignKey",
                table: "Cadastro");

            migrationBuilder.AddColumn<int>(
                name: "CadastroForeignKey",
                table: "Pedido",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_CadastroForeignKey",
                table: "Pedido",
                column: "CadastroForeignKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Cadastro_CadastroForeignKey",
                table: "Pedido",
                column: "CadastroForeignKey",
                principalTable: "Cadastro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Cadastro_CadastroForeignKey",
                table: "Pedido");

            migrationBuilder.DropIndex(
                name: "IX_Pedido_CadastroForeignKey",
                table: "Pedido");

            migrationBuilder.DropColumn(
                name: "CadastroForeignKey",
                table: "Pedido");

            migrationBuilder.AddColumn<int>(
                name: "PedidoForeignKey",
                table: "Cadastro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cadastro_PedidoForeignKey",
                table: "Cadastro",
                column: "PedidoForeignKey",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cadastro_Pedido_PedidoForeignKey",
                table: "Cadastro",
                column: "PedidoForeignKey",
                principalTable: "Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
