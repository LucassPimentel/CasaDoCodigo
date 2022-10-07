using CasaDoCodigo.Context;
using CasaDoCodigo.DataService;
using CasaDoCodigo.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();


// Sessao serve para manter o estado de uma maneira artificial controlada pelo servidor, para que não se perca dados ao navegar por outras paginas da aplicação (a web é stateless ou seja, não tem estado).

// Adicionando sessão e cache distribuida na memoria
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<DataBaseContext>(opts =>
    opts.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationInsightsTelemetry();




builder.Services.AddTransient<IDataService, DataService>();
builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddTransient<ICadastroRepository, CadastroRepository>();
builder.Services.AddTransient<IItemPedidoRepository, ItemPedidoRepository>();



builder.Services.BuildServiceProvider().GetService<IDataService>().InicializaDb();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

builder.Services.AddHttpContextAccessor();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pedido}/{action=Carrossel}/{Codigo?}");

app.Run();
