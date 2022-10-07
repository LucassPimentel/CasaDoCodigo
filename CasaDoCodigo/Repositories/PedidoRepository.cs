using CasaDoCodigo.Context;
using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PedidoRepository(DataBaseContext dbContext, IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
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
