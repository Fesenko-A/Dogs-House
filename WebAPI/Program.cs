using WebAPI.Extensions;
using Business.Configuration;
using DataAccess.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string is not specified.");

builder.Services.AddDataAccess(connectionString);
builder.Services.AddBusinessLogic();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddGlobalExceptionHandler();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.UseSwaggerUI(opt => {
        opt.SwaggerEndpoint("/openapi/v1.json", "Dog House API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler(_ => { });

app.MapControllers();

app.Run();

public partial class Program { }