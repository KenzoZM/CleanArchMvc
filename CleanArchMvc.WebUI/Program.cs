using CleanArchMvc.Domain.Account;
using CleanArchMvc.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("ApiBaseUrl"); // altere a Url com base na sua Url de acesso.
                                                // Para identificar a sua Url abra o pasta
                                                // Properties e lauchSettings.json na camada CleanArchMvc.API
                                                // l� voc� ver� a sua URL baseada em qual padr�o vc est� executando o projeto 
                                                // por exemplo http ou https.
                                                // depois de pegar a sua URL v� at� a camadam CleanArchMvc.Infra.Ioc
                                                // e altere a sua ApiBaseUrl com o valor da sua URL.
});

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

using (var scope = app.Services.CreateScope())
{
    var seedUserRoleInitial = scope.ServiceProvider.GetRequiredService<ISeedUserRoleInitial>();
    seedUserRoleInitial.SeedRoles();
    seedUserRoleInitial.SeedUsers();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// RESPONSABILIDADES DE CADA CAMADA;

// CleanArch.Domain: Modelo de dominio, regras de negocio, interfaces; (N�o possui nenhuma dependencia);
// CleanArch.Application: Regras de dominio da aplica��o, Mapeamentos, servi�os, DTOs, CQRS; (Dependencia com o Projeto Domain);
// CleanArch.Infra.Data: EF Core, Contexto, Configura��es, Migrations, Repository; (Dependencia com o Projeto Domain);
// CleanArch.Infra.Ioc: Dependncy Injection, Registro dos servi�os, tempo de vida; (Dependencia com os projetos: Domain, Application, Infra.Data) 
// CleanArch.WebUI: MVC, Controllers, Views, Filtros, ViewModels(MVC); (Dependencia com o Infra.Ioc);