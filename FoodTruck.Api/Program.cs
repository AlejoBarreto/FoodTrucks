using FluentValidation;
using FluentValidation.AspNetCore;
using FoodTruck.Api.Extensions;
using FoodTruck.Api.Handlers;
using FoodTruck.Common.Models;
using FoodTruck.Common.Validators;
using FoodTruck.Infrastructure.Clients;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddClients();
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddExceptionHandler<ExceptionHandler>();

//Add FV
builder.Services.AddFluentValidationAutoValidation();

//Validators
builder.Services.AddValidatorsFromAssemblyContaining<GetFoodTrucksRequestValidator>();

var config = builder.Configuration;

builder.Services.Configure<SodaClientSettings>(config.GetSection(SodaClientSettings.OptionKey));

builder.Services.AddHttpClient(FoodTrucksClient.ClientName, (provider, httpClient) =>
{
    var settings = provider.GetRequiredService<IOptions<SodaClientSettings>>().Value;
    httpClient.BaseAddress = new Uri(settings.BaseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

app.UseCors(applicationBuilder =>
{
    applicationBuilder.AllowAnyMethod();
    applicationBuilder.AllowAnyHeader();
    applicationBuilder.AllowCredentials();
    applicationBuilder.SetIsOriginAllowed(_ => true);
});

app.UseAuthorization();

app.MapControllers();

app.Run();
