using Microsoft.EntityFrameworkCore;
using Student.Models; // Assicurati che questa direttiva usi il namespace corretto dove si trova la classe Student

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = string.Empty;

try
{
    connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new Exception("Stringa di connessione assente!");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Environment.Exit(1);
}

builder.Services.AddDbContext<SchoolDbContext>(option => option.UseSqlServer(connectionString));

//builder.Services.AddScoped<StudentService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();
    db.Database.Migrate(); // richiede Microsoft.EntityFrameworkCore.Design + dotnet-ef (vedi comandi sotto)

    if (!db.Students.Any())
    {
        db.Students.AddRange(
            new Student.Models.Student { Nome = "Mario", Cognome = "Rossi", DataDiNascita = new DateOnly(2000, 1, 1), Email = "mario.rossi@example.com" },
            new Student.Models.Student { Nome = "Anna", Cognome = "Bianchi", DataDiNascita = new DateOnly(1999, 5, 12), Email = "anna.bianchi@example.com" }
        );
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
