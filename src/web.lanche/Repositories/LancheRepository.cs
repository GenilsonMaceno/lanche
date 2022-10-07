using Microsoft.EntityFrameworkCore;
using web.lanche.Context;
using web.lanche.Repositories.Interfaces;
using web.lanche.Views.Home;

namespace web.lanche.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches
                                                                .Where(l => l.IsLanchePreferido)
                                                                .Include(c => c.Categoria);

        public Lanche GetLancheById(int LancheId)
        {
            return _context.Lanches.FirstOrDefault(l => l.LancheId == LancheId);
        }
    }
}