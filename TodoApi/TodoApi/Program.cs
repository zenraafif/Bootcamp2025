using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Logging.AddConsole();

// USE Memory database
//builder.Services.AddDbContext<TodoContext>(opt =>
//    opt.UseInMemoryDatabase("TodoList"));

// USE SQLite database
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseSqlite("Data Source=TodoList.db"));

builder.Services.AddEndpointsApiExplorer();

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Todo API",
        Version = "v1"
    });
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //Swagger
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
