using Microsoft.AspNetCore.Mvc;
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

        public IActionResult List()
        {
            // var lanches = _lancheRepository.Lanches;

            var viewModel = new LancheViewModel();

            viewModel.Lanches = _lancheRepository.Lanches;
            viewModel.CategoriaAtual = "Categoria Atual";

            return View(viewModel);
        }
    }
}