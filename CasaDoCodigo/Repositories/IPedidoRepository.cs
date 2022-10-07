using CasaDoCodigo.Models;

public interface IPedidoRepository
{
    Pedido GetPedido();
    void AddItem(string codigoProduto);
}