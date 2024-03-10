using AgendaVoluntarios.Areas.Identity.Data;
using AgendaVoluntarios.Data;
using AgendaVoluntarios.Data.Persistence;
using AgendaVoluntarios.Repositories.Interfaces;
using AgendaVoluntarios.Repositories.Repositories;
using AgendaVoluntarios.Services.Interfaces;
using AgendaVoluntarios.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AgendaVoluntariosContext") ?? throw new InvalidOperationException("Connection string 'AgendaVoluntariosContext' not found.");

builder.Services.AddDbContext<AgendaVoluntariosDbContext>(options =>
    options.UseSqlServer(connectionString));

var identityConnectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(identityConnectionString));

// Configuração do Identity
//builder.Services.AddIdentity<User, IdentityRole>()
//    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();


#region Repository
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileFunctionRepository, ProfileFunctionRepository>();
builder.Services.AddScoped<IProfileEventRepository, ProfileEventRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventMusicRepository, EventMusicRepository>();
builder.Services.AddScoped<IFunctionRepository, FunctionRepository>(); 
builder.Services.AddScoped<IMusicRepository, MusicRepository>();
#endregion

#region Service
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IFunctionService, FunctionService>();
builder.Services.AddScoped<IMusicService, MusicService>();
#endregion


builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//CreateRoles(app.Services).Wait();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Profile}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});


app.Run();


async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Verifica se a role 'User' existe e, se não existir, cria-a
    var roleAdminExists = await roleManager.RoleExistsAsync("Admin");
    if (!roleAdminExists)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    var roleEmployeeExists = await roleManager.RoleExistsAsync("Employee");
    if (!roleEmployeeExists)
    {
        await roleManager.CreateAsync(new IdentityRole("Employee"));
    }

    // Adicione mais roles aqui, se necessário
}