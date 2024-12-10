using bysproje.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�s�
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// IHttpClientFactory'yi kaydedin
builder.Services.AddHttpClient(); // Bu sat�r zaten mevcut, bu nedenle bir �ey de�i�tirmeye gerek yok


builder.Services.AddRazorPages(); // Di�er hizmetleri ekleme

// Servis ve Controller eklemeleri
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware yap�land�rmas�
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapRazorPages();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();


app.MapControllers();

app.Run();
