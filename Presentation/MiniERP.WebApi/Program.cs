using MiniERP.Application.Configuration;
using MiniERP.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services Clean Architecture
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Services API
builder.Services.AddControllers();

// Configuration CORS pour le développement
builder.Services.AddCors(options =>
{
    options.AddPolicy("Development", policy =>
    {
        policy.WithOrigins("https://localhost:7013", "http://localhost:5025")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
