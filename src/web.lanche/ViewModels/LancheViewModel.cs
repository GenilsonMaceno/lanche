using web.lanche.Models;

namespace web.lanche.ViewModels
{
    public class LancheViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; }

        public string CategoriaAtual { get; set; }
    }
}