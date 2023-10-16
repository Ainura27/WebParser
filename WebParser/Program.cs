using WebParser.Extensions;
using WebParser.Models.Configurations;
using WebParser.Repository;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<MainConfiguration>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<InitializeDatabase>();
builder.Services.AddServices();
builder.Services.AddScoped<IPostRepository, PostRepository>();
var app = builder.Build();
app.MigrateDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
