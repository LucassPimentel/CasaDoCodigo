namespace CasaDoCodigo.Models.ViewModels
{
    public class CarrinhoViewModel
    {
        public IList<ItemPedido> Itens { get; }

        public decimal Total => Itens.Sum(i => i.Quantidade * i.PrecoUnitario);

        public CarrinhoViewModel(IList<ItemPedido> itens)
        {
            Itens = itens;
        }



    }
}
