using CasaDoCodigo.Context;
using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Newtonsoft.Json;

namespace CasaDoCodigo.DataService
{
    public class DataService : IDataService
    {
        private readonly DataBaseContext _dbContext;
        private readonly IProdutoRepository _produtoRepository;

        public DataService(DataBaseContext dbContext, IProdutoRepository produtoRepository)
        {
            _dbContext = dbContext;
            _produtoRepository = produtoRepository;
        }
        public void InicializaDb()
        {
            _dbContext.Database.EnsureCreated();


            // lendo os dados que estão salvos no arquivo em json 
            List<Livro>? livros = GetLivros();

            _produtoRepository.SaveProdutos(livros);
        }



        private static List<Livro>? GetLivros()
        {
            var livrosJson = File.ReadAllText("_Recursos/dados/livros.json");

            // deserializando de json para um objeto c#
            var livros = JsonConvert.DeserializeObject<List<Livro>>(livrosJson);
            return livros;
        }
    }
}
