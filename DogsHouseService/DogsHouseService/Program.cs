using DogsHouseService.BLL.Abstractions;
using DogsHouseService.BLL.DTOs;
using DogsHouseService.BLL.Services;
using DogsHouseService.BLL.Services.Validators;
using DogsHouseService.DAL;
using DogsHouseService.DAL.Abstractions;
using DogsHouseService.DAL.Repositories;
using DogsHouseService.Settings;
using FluentValidation;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

IConfiguration appConfig = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.Configure<RateSettings>("RateSettings", appConfig.GetSection("RateSettings"));

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(appConfig.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDogsRepository, DogsRepository>();
builder.Services.AddScoped<IDogsService, DogsService>();
builder.Services.AddScoped<IValidator<DogDto>, DogValidator>();

using IServiceScope scope = builder.Services.BuildServiceProvider().CreateScope();
RateSettings? rateSettings = scope.ServiceProvider
    .GetService<IOptionsSnapshot<RateSettings>>()?
    .Get("RateSettings");

if (rateSettings is null)
{
    throw new Exception($"Rate settings is not configured.");
}

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddConcurrencyLimiter("concurrency", options =>
    {
        options.PermitLimit = rateSettings.RequestLimit!.Value;
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 5;
    });
});

builder.Services.AddLogging();

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
