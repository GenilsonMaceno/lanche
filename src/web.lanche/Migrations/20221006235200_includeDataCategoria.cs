using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.lanche.Migrations
{
    public partial class includeDataCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Categorias(Nome, Descricao) " +
            "values('Normal','Lanche feito com igredientes normais')");

            migrationBuilder.Sql("Insert into Categorias(Nome, Descricao) " +
            "values('Natural','Lanche feito com ingredientes integrais e naturais')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categorias");
        }
    }
}
