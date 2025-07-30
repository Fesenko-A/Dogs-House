using Business.Configuration;
using DataAccess.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using WebAPI.Extensions;
using WebAPI.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string is not specified.");

builder.Services.Configure<RateLimitingOptions>(
    builder.Configuration.GetSection("RateLimiting"));

var rateOptions = builder.Configuration.GetSection("RateLimiting").Get<RateLimitingOptions>()
    ?? throw new InvalidOperationException("Rate limiting options are not specified.");

builder.Services.AddRateLimiter(options => {
    options.OnRejected = async (context, cancellationToken) => {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.ContentType = "text/plain";
        await context.HttpContext.Response.WriteAsync("Too many requests", cancellationToken);
    };

    options.AddFixedWindowLimiter(policyName: "Fixed", limiterOptions => {
        limiterOptions.PermitLimit = rateOptions.PermitLimit;
        limiterOptions.Window = rateOptions.Window;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

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

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler(_ => { });

app.MapControllers().RequireRateLimiting("Fixed");

app.Run();

public partial class Program { }