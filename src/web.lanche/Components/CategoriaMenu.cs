using Microsoft.AspNetCore.Mvc;
using web.lanche.Repositories.Interfaces;

namespace web.lanche.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaMenu(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public IViewComponentResult Invoke(){
            
            var categoria = _categoriaRepository.Categorias.OrderBy(c => c.Nome);
            return View(categoria);
        }
    }
}