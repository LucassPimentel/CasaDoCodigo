using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // definindo a chave primaria da entidade produto
            modelBuilder.Entity<Produto>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Pedido>()
                .HasKey(t => t.Id);
            // um pedido pode ter muitos itens, e muitos itens tem um pedido
            modelBuilder.Entity<Pedido>()
                .HasMany(t => t.Itens)
                .WithOne(t => t.Pedido);

            // um pedido tem um cadastro e vice versa
            modelBuilder.Entity<Pedido>()
                .HasOne(t => t.Cadastro)
                .WithOne(t => t.Pedido)
                .HasForeignKey<Pedido>(t => t.CadastroForeignKey)
                .IsRequired();

            modelBuilder.Entity<ItemPedido>()
                .HasKey(t => t.Id);
            // um itemPedido tem um pedido e um produto
            modelBuilder.Entity<ItemPedido>()
                .HasOne(t => t.Pedido);
            modelBuilder.Entity<ItemPedido>()
                .HasOne(t => t.Produto);

            modelBuilder.Entity<Cadastro>()
                .HasKey(t => t.Id);
            // um cadastro tem um pedido
            modelBuilder.Entity<Cadastro>()
                .HasOne(t => t.Pedido);
        }
    }
}
