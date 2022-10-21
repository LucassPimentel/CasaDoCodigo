using CasaDoCodigo.Context;
using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repositories
{
    public class CadastroRepository : BaseRepository<Cadastro>, ICadastroRepository
    {
        public CadastroRepository(DataBaseContext dbContext) : base(dbContext)
        {
        }

        public Cadastro UpdateCadastro(int cadastroId, Cadastro novoCadastro)
        {
            var cadastroDB = _dbSet
                .Where(c => c.Id == cadastroId)
                .SingleOrDefault();

            if (cadastroDB == null)
            {
                throw new ArgumentNullException("Cadastro");
            }

            cadastroDB.Update(novoCadastro);
            _dbContext.SaveChanges();

            return cadastroDB;

        }
    }
}
