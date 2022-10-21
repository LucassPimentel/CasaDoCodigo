using CasaDoCodigo.Context;
using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(DataBaseContext dbContext) : base(dbContext)
        {
        }

        public ItemPedido GetItemPedido(int itemPedidoId)
        {
            return _dbSet
               .Where(ip => ip.Id == itemPedidoId)
               .SingleOrDefault();
        }

        public void RemoveItemPedido(int itemPedidoId)
        {
            //var itemPedido = GetItemPedido(itemPedidoId);
            //_dbSet.Remove(itemPedido);
            _dbSet.Remove(GetItemPedido(itemPedidoId));
        }
    }
}
