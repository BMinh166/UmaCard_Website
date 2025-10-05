using UmaCardAPI.Data;
using Microsoft.EntityFrameworkCore;
using UmaCardAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// ---------------------- DATABASE ----------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------------------- CONTROLLERS + SWAGGER ----------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------------- CORS (Cho phép React) ----------------------
// Đọc FRONTEND_ORIGINS từ biến môi trường (ví dụ: "http://localhost:5173,https://your-vercel-domain")
// Nếu không có env var thì mặc định là http: //localhost:5173
var originsEnv = builder.Configuration["FRONTEND_ORIGINS"] ?? "http://localhost:5173";
var allowedOrigins = originsEnv
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
        policy.WithOrigins(allowedOrigins)   // truyền mảng string[]
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// ---------------------- MIGRATION + SEED DATA ----------------------
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
    Console.WriteLine("Migration failed:");
    Console.WriteLine(ex.ToString());
}

// ---------------------- MIDDLEWARE ----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ⚡ Dùng đúng policy CORS đã cấu hình (AllowReactApp)
app.UseCors("AllowReactApp");

app.UseAuthorization();
app.MapControllers();

// ---------------------- RUN APP ----------------------
app.Run();
