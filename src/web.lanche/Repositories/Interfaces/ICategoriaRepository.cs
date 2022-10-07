using web.lanche.Views.Home;

namespace web.lanche.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
         IEnumerable<Categoria> Categorias {get;}
    }
}