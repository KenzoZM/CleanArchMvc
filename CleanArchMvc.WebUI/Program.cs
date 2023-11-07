using CleanArchMvc.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// RESPONSABILIDADES DE CADA CAMADA;

// CleanArch.Domain: Modelo de dominio, regras de negocio, interfaces; (Não possui nenbuma dependencia);
// CleanArch.Application: Regras de dominio da aplicação, Mapeamentos, serviços, DTOs, CQRS; (Dependencia com o Projeto Domain);
// CleanArch.Infra.Data: EF Core, Contexto, Configurações, Migrations, Repository; (Dependencia com o Projeto Domain);
// CleanArch.Infra.Ioc: Dependncy Injection, Registro dos serviços, tempo de vida; (Dependencia com os projetos: Domain, Application, Infra.Data) 
// CleanArch.WebUI: MVC, Controllers, Views, Filtros, ViewModels(MVC); (Dependencia com o Infra.Ioc);