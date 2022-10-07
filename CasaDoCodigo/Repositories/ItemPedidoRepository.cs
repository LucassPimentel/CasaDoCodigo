using CasaDoCodigo.Context;
using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(DataBaseContext dbContext) : base(dbContext)
        {
        }


    }
}
