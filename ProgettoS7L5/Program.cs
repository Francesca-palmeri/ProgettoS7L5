using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgettoS7L5.Data;
using ProgettoS7L5.Models;

var builder = WebApplication.CreateBuilder(args);

// registrare DbContext 
builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Imposta se l'account deve essere confermato via email prima di poter accedere
    options.SignIn.RequireConfirmedAccount =
       builder.Configuration.GetSection("Identity").GetValue<bool>("RequireConfirmedAccount");

    // Imposta la lunghezza minima della password
    options.Password.RequiredLength =
        builder.Configuration.GetSection("Identity").GetValue<int>("RequiredLength");

    // Richiede che la password contenga almeno un numero
    options.Password.RequireDigit =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireDigit");

    // Richiede almeno una lettera minuscola nella password
    options.Password.RequireLowercase =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireLowercase");

    // Richiede almeno un carattere speciale nella password
    options.Password.RequireNonAlphanumeric =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireNonAlphanumeric");

    // Richiede almeno una lettera maiuscola nella password
    options.Password.RequireUppercase =
        builder.Configuration.GetSection("Identity").GetValue<bool>("RequireUppercase");
})
    // Utilizza il contesto del database per archiviare utenti e ruoli
    .AddEntityFrameworkStores<ApplicationDbContext>()
    // Aggiunge provider di token predefiniti per la gestione delle autenticazioni e conferme
    .AddDefaultTokenProviders();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
