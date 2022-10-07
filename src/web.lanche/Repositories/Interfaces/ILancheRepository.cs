using web.lanche.Views.Home;

namespace web.lanche.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get;}

        IEnumerable<Lanche> LanchesPreferidos { get;}

        Lanche GetLancheById(int LancheId);
    }
}