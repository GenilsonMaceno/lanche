using web.lanche.Models;

namespace web.lanche.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
         IEnumerable<Categoria> Categorias {get;}
    }
}