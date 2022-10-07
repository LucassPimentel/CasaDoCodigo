using CasaDoCodigo.Context;
using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DataBaseContext dbContext) : base(dbContext)
        {
        }

        public void SaveProdutos(List<Livro>? livros)
        {
            foreach (var livro in livros)
            {
                // verificando se o produto não existe existe no banco de dados
                if (!_dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    // adicionando os produtos no banco de dados na tabela Produto
                    _dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));
                }
            }
            _dbContext.SaveChanges();
        }

        public List<Produto> GetProdutos()
        {
            return _dbSet.ToList();
        }
    }
}
