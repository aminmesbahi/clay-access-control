using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Db");
// Add services to the container.
builder.Services.AddDbContext<AccessControlDbContext>(x => x.UseSqlite(connectionString));
builder.Services.AddScoped<IAccessControlService, AccessControlService>();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers()
    .AddJsonOptions(ops =>
    {
        ops.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        ops.JsonSerializerOptions.WriteIndented = true;
        ops.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        ops.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        ops.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    setup.EnableAnnotations();
    setup.SwaggerDoc("v1",
        new OpenApiInfo { Description = "Access Controller API", Title = "Clay Access Control", Version = "v1" });
});

var app = builder.Build();

await EnsureDbAsync(app.Services);

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task EnsureDbAsync(IServiceProvider sp)
{
    await using var db = sp.CreateScope().ServiceProvider.GetRequiredService<AccessControlDbContext>();
    await db.Database.MigrateAsync();
}