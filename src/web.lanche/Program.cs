using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using web.lanche.Context;
using web.lanche.Models;
using web.lanche.Repositories;
using web.lanche.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//IdentityUser => A classe IdentityUser é fornecida pela ASP.NET Core e contém propriedade para UserName, PasswordHash, Email etc. 
// Essa é a classe usada pelo Identity para gerenciar usuários registrados do seu aplicativo.

//IdentityRole => É uma classe interna do Identity e fornece informações sobre perfil do usuário.
builder.Services.AddIdentity<IdentityUser,IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


//pesquisa sobre tipo de escopo
builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddScoped(serviceProvider => CarrinhoCompra.GetCarrinho(serviceProvider));

builder.Services.AddControllersWithViews();

// adicionando o Middleware
builder.Services.AddMemoryCache();
builder.Services.AddSession();


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

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {

    endpoints.MapControllerRoute(
    name: "categoriaFiltro",
    pattern: "Lanche/{action}/{categoria?}",
    defaults: new { Controller = "Lanche", Action= "List"});

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

});

app.Run();
