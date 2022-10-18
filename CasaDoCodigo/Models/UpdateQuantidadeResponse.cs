using CasaDoCodigo.Models.ViewModels;

namespace CasaDoCodigo.Models
{
    public class UpdateQuantidadeResponse
    {
        public UpdateQuantidadeResponse(ItemPedido itemPedido, CarrinhoViewModel carrinho)
        {
            ItemPedido = itemPedido;
            Carrinho = carrinho;
        }

        public ItemPedido ItemPedido { get; }
        public CarrinhoViewModel Carrinho { get; }
    }
}
