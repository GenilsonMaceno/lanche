using Microsoft.EntityFrameworkCore;
using web.lanche.Context;

namespace web.lanche.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }

        public List<CarrinhoCompraItem> carrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services){

            // Define uma sessão
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // obtem um serviço do tipo nosso contexto
            var context = services.GetService<AppDbContext>();

            // obtem ou gera o Id do carrinho
            // ??  :  Operador de coalescência nula
            // ??= :  Operador de atribuição de agrupamento nulo
            // Objetos?.Expressão : Operador Elvis
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // Atribui o id do carrinho na Sessão
            session.SetString("CarrinhoId", carrinhoId);

            //retorna o carrinho com o contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context){
                CarrinhoCompraId = carrinhoId
            };
    
        }

        #region [ ADICIONAR CARRINHO ]
        public void AdicionarCarrinho(Lanche lanche){

            var carrinhoCompraItem = _context.CarrinhoCompraItems
                                             .SingleOrDefault(s => s.Lanche.LancheId == lanche.LancheId &&
                                                              s.CarrinhoCompraId == CarrinhoCompraId);

             if (carrinhoCompraItem is null)
             {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _context.CarrinhoCompraItems.Add(carrinhoCompraItem);
             }else{
                carrinhoCompraItem.Quantidade++;
             }

             _context.SaveChanges();
        }
        
        #endregion [ ADICIONAR CARRINHO ]

        #region [ REMOVER CARRINHO ]

        public void RemoverCarrinho(Lanche lanche){


            var carrinhoCompraItem = _context.CarrinhoCompraItems
                                             .SingleOrDefault
                                             (
                                                s => s.Lanche.LancheId == lanche.LancheId 
                                                &&   s.CarrinhoCompraId == CarrinhoCompraId
                                             );

            //var Quantidade = 0;

            if(carrinhoCompraItem != null){

                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                }else{
                    _context.CarrinhoCompraItems.Remove(carrinhoCompraItem);
                }

                _context.SaveChanges();
            }  
        }
       
        #endregion [ REMOVER CARRINHO ]

        #region [ OBTER CARRINHO DE COMPRA ITEM ]
        public List<CarrinhoCompraItem> GetCarrinhoCompraItems(){
            
            return carrinhoCompraItems ?? 
                   (carrinhoCompraItems = 
                        _context.CarrinhoCompraItems
                        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                        .Include(s => s.Lanche)
                        .ToList());
        }

        #endregion [ OBTER CARRINHO DE COMPRA ITEM ]

        #region [LIMPAR CARRINHO ]
        public void LimparCarrinho(){
            var carrinhoItens = _context.CarrinhoCompraItems
                                .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoCompraItems.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        #endregion [LIMPAR CARRINHO ]
        
        #region [OBTER TOTAL DE COMPRA]
        public decimal GetCarrinhoCompraTotal(){

            var total = _context.CarrinhoCompraItems
                        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                        .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

            return total;
        }

        #endregion [OBTER TOTAL DE COMPRA]

    }

}