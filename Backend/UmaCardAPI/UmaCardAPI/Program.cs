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

    var newCards = new[]
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
                },
                new UmaCard
                {
                Name = "Fine Motion",
                OutfitType = "Race",
                Type = "Witness",
                Description = "A precise runner with incredible focus and determination.",
                ImageUrl = "/images/fine_motion.png"
                },
                new UmaCard
                {
                Name = "Super Creek",
                OutfitType = "Race",
                Type = "Stamina",
                Description = "A reliable Uma Musume with outstanding endurance.",
                ImageUrl = "/images/super_creek.png"
                },
                new UmaCard
                {
                Name = "Silent Suzuka",
                OutfitType = "Race",
                Type = "Speed",
                Description = "An agile and swift Uma Musume with explosive bursts of speed.",
                ImageUrl = "/images/silent_suzuka.png"
                },
                new UmaCard
                {
                Name = "Special Week",
                OutfitType = "Race",
                Type = "Guts",
                Description = "A determined Uma Musume who never gives up, regardless of challenge.",
                ImageUrl = "/images/special_week.png"
                },
                new UmaCard
                {
                Name = "Oguri Cap",
                OutfitType = "Race",
                Type = "Power",
                Description = "A strong Uma Musume known for her incredible finishing power.",
                ImageUrl = "/images/oguri_cap.png"
                },
                new UmaCard
                {
                Name = "Haru Urara",
                OutfitType = "Casual",
                Type = "Guts",
                Description = "A spirited Uma Musume who continues to run with optimism.",
                ImageUrl = "/images/haru_urara.png"
                },
                new UmaCard
                {
                Name = "Sakura Bakushin O",
                OutfitType = "Race",
                Type = "Speed",
                Description = "A young, speedy Uma Musume with an explosive start.",
                ImageUrl = "/images/sakura_bakushin_o.png"
                },
                new UmaCard
                {
                Name = "Satono Diamond",
                OutfitType = "Formal",
                Type = "Stamina",
                Description = "A composed Uma Musume with exceptional endurance and style.",
                ImageUrl = "/images/satono_diamond.png"
                },
                new UmaCard
                {
                Name = "Mejiro Dober",
                OutfitType = "Bride",
                Type = "Witness",
                Description = "A skilled and refined Uma Musume with precise timing.",
                ImageUrl = "/images/mejiro_dober.png"
                },
                new UmaCard
                {
                Name = "Sakura Chiyono O",
                OutfitType = "Race",
                Type = "Stamina",
                Description = "A dedicated Uma Musume with strong persistence on the track.",
                ImageUrl = "/images/sakura_chiyono_o.png"
                },
                new UmaCard
                {
                Name = "Kawakami Princess",
                OutfitType = "Race",
                Type = "Speed",
                Description = "A fast Uma Musume with an elegant and graceful running style.",
                ImageUrl = "/images/kawakami_princess.png"
                },
                new UmaCard
                {
                Name = "Seiun Sky",
                OutfitType = "Race",
                Type = "Stamina",
                Description = "A reliable Uma Musume with impressive endurance and consistency.",
                ImageUrl = "/images/seiun_sky.png"
                },
                new UmaCard
                {
                Name = "Nishino Flower",
                OutfitType = "Race",
                Type = "Speed",
                Description = "A sprightly Uma Musume with quick acceleration.",
                ImageUrl = "/images/nishino_flower.png"
                },
                new UmaCard
                {
                Name = "Gold City",
                OutfitType = "Race",
                Type = "Speed",
                Description = "A strong, speedy Uma Musume with radiant energy.",
                ImageUrl = "/images/gold_city.png"
                },
                new UmaCard
                {
                Name = "Kitasan Black",
                OutfitType = "Race",
                Type = "Speed",
                Description = "A top-class Uma Musume with phenomenal speed and power.",
                ImageUrl = "/images/kitasan_black_01.png"
                }

    };

    foreach (var card in newCards)
    {
        if (!db.UmaCards.Any(c => c.Name == card.Name))
        {
            db.UmaCards.Add(card);
        }
    }
    db.SaveChanges();

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
