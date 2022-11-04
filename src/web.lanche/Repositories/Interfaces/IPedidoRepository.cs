using web.lanche.Models;

namespace web.lanche.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
         void CriarPedido(Pedido pedido);
    }
}