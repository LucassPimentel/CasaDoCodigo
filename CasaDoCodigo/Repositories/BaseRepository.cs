using CasaDoCodigo.Context;
using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Repositories
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        protected readonly DataBaseContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

    }
}
