using Microsoft.EntityFrameworkCore;
using Portfolio.Application.Interfaces;
using Portfolio.Application.Services;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure;
using Portfolio.Infrastructure.ExternalServices;
using Portfolio.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Registering services (Dependency Injection)

// Add controller support
builder.Services.AddControllers();

// Add swagger 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DB
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("DataSource=portfolio.db"));

// Register custom repository and service
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Add CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Register HttpClient for Alpha vantage
builder.Services.AddHttpClient<IMarketDataService, AlphaVantageService>(client => 
{
    client.BaseAddress = new Uri(builder.Configuration["AlphaVantage:BaseUrl"]!);
});

var app = builder.Build();

// Configuring http request pipeline

// Enable swagger UI if we are in development mode

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");
// Map controllers so the API can finc our endpoints
app.MapControllers();

app.Run();