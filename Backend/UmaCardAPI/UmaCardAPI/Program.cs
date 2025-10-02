using UmaCardAPI.Data;
using Microsoft.EntityFrameworkCore;
using UmaCardAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Kết nối SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controller + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS cho React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// Tự migrate & seed data
try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    if (!db.UmaCards.Any())
    {
        db.UmaCards.AddRange(new[]
        {
            new UmaCard
            {
                Name = "Special Tokai Teio",
                OutfitType = "Race",
                Type = "Speed",
                Description = "A cheerful Uma Musume with unmatched energy.",
                ImageUrl = "/images/tokai_teio_01.png"
            },
            new UmaCard
            {
                Name = "Elegant Mejiro McQueen",
                OutfitType = "Formal",
                Type = "Stamina",
                Description = "A graceful Uma Musume with noble charm.",
                ImageUrl = "/images/mejiro_mcqueen_01.png"
            }
        });
        db.SaveChanges();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Migration failed: {ex.Message}");
}

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
});

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
