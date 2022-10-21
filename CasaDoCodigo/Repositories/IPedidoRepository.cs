using CasaDoCodigo.Models;

public interface IPedidoRepository
{
    Pedido GetPedido();
    void AddItem(string codigoProduto);
    UpdateQuantidadeResponse UpdateQuantidade(ItemPedido itemPedido);

    Pedido UpdateCadastro(Cadastro cadastro);
}