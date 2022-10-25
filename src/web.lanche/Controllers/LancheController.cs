using Microsoft.AspNetCore.Mvc;
using web.lanche.Models;
using web.lanche.Repositories.Interfaces;
using web.lanche.ViewModels;

namespace web.lanche.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";

            }else
            {
                if (string.Equals("Normal", categoria , StringComparison.OrdinalIgnoreCase))
                {
                    lanches = _lancheRepository.Lanches
                        .Where(l => l.Categoria.Nome.Equals("Normal"))
                        .OrderBy(l => l.Nome);
                }else{

                    lanches = _lancheRepository.Lanches
                        .Where(l => l.Categoria.Nome.Equals("Natural"))
                        .OrderBy(l => l.Nome);
                }
            }

            var viewModel = new LancheViewModel(){
                Lanches = lanches,
                CategoriaAtual = categoria
            };

            return View(viewModel);
        }
    }
}