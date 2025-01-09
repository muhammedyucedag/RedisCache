using RedisCache.Interfaces;
using RedisCache.Services;
using Scalar.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

IConfiguration configuration = builder.Configuration;
var redisConnection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis") ?? string.Empty);

builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);
builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapControllers();
}

app.MapScalarApiReference(options =>
{
    options
        .WithTitle("RedisCache Api")
        .WithDownloadButton(true)
        .WithTheme(ScalarTheme.DeepSpace)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        //.WithOpenApiRoutePattern("..v1.openapi.json");
        .WithOpenApiRoutePattern("/swagger/v1/swagger.json");
});

//app.Map("/", () => Results.Redirect("/scalar/v1"));
app.Map("/", () => Results.Redirect("/swagger"));

app.Run();