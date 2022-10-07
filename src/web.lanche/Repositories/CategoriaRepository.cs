using web.lanche.Context;
using web.lanche.Repositories.Interfaces;
using web.lanche.Views.Home;

namespace web.lanche.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}