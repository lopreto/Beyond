using Beyond.TodoProject.Application.Interfaces;
using Beyond.TodoProject.Application.Services;
using Beyond.TodoProject.Domain.Aggregates;
using Beyond.TodoProject.Domain.Interfaces;
using Beyond.TodoProject.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Se registra como Singleton para mantener la instancia durante toda la vida de la aplicación.
// En un contexto con acceso a base de datos u otros recursos por solicitud, se debería usar Scoped en su lugar.
builder.Services.AddSingleton<ITodoList, TodoList>();
builder.Services.AddScoped<ITodoListService, TodoListService>();
builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();

builder.Services.AddLogging(config =>
{
	config.AddConsole();
});

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
