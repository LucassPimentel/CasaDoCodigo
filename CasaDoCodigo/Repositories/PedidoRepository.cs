using CasaDoCodigo.Context;
using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IItemPedidoRepository _itemPedidoRepository;

        public PedidoRepository(DataBaseContext dbContext, IHttpContextAccessor httpContextAccessor, IItemPedidoRepository itemPedidoRepository) : base(dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _itemPedidoRepository = itemPedidoRepository;
        }

        public void AddItem(string codigoProduto)
        {
            var produto = _dbContext.Set<Produto>()
                .Where(p => p.Codigo == codigoProduto)
                .SingleOrDefault();

            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado.");
            }

            var pedido = GetPedido();

            var itemPedido = _dbContext.Set<ItemPedido>()
                .Where(i => i.Produto.Codigo == codigoProduto
                && i.Pedido.Id == pedido.Id)
                .FirstOrDefault();

            if (itemPedido == null)
            {
                itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
                _dbContext.Set<ItemPedido>()
                    .Add(itemPedido);

                _dbContext.SaveChanges();
            }
        }

        public Pedido GetPedido()
        {
            var pedidoId = GetPedidoId();
            var pedido = _dbSet
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .Where(p => p.Id == pedidoId)
                .SingleOrDefault();

            if (pedido == null)
            {
                pedido = new Pedido();
                _dbSet.Add(pedido);
                _dbContext.SaveChanges();
                SetPedidoId(pedido.Id);
            }

            return pedido;
        }

        public UpdateQuantidadeResponse UpdateQuantidade(ItemPedido itemPedido)
        {
            var itemPedidoDb = _itemPedidoRepository.GetItemPedido(itemPedido.Id);

            if (itemPedido != null)
            {
                itemPedidoDb.AtualizaQuantidade(itemPedido.Quantidade);
                _dbContext.SaveChanges();

                var carrinhoVM = new CarrinhoViewModel(GetPedido().Itens);
                return new UpdateQuantidadeResponse(itemPedidoDb, carrinhoVM);
            }

            throw new ArgumentException("Item Pedido não encontrado.");
        }

        private int? GetPedidoId()
        {
            // lendo o Id do pedido na sessao
            return _httpContextAccessor.HttpContext.Session.GetInt32("pedidoId");
        }

        private void SetPedidoId(int pedidoId)
        {
            // definindo o Id do pedido na sessao
            _httpContextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        }

    }
}
