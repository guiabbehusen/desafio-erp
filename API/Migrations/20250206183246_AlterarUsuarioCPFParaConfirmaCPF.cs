using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AlterarUsuarioCPFParaConfirmaCPF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Usuarios_UsuarioCPF",
                table: "Enderecos");

            migrationBuilder.RenameColumn(
                name: "UsuarioCPF",
                table: "Enderecos",
                newName: "Confirmar_CPF");

            migrationBuilder.RenameIndex(
                name: "IX_Enderecos_UsuarioCPF",
                table: "Enderecos",
                newName: "IX_Enderecos_Confirmar_CPF");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Usuarios_Confirmar_CPF",
                table: "Enderecos",
                column: "Confirmar_CPF",
                principalTable: "Usuarios",
                principalColumn: "CPF",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Usuarios_Confirmar_CPF",
                table: "Enderecos");

            migrationBuilder.RenameColumn(
                name: "Confirmar_CPF",
                table: "Enderecos",
                newName: "UsuarioCPF");

            migrationBuilder.RenameIndex(
                name: "IX_Enderecos_Confirmar_CPF",
                table: "Enderecos",
                newName: "IX_Enderecos_UsuarioCPF");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Usuarios_UsuarioCPF",
                table: "Enderecos",
                column: "UsuarioCPF",
                principalTable: "Usuarios",
                principalColumn: "CPF",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
