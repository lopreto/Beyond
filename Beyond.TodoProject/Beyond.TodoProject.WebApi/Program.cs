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

// Se registra como Singleton para mantener la instancia durante toda la vida de la aplicaci�n.
// En un contexto con acceso a base de datos u otros recursos por solicitud, se deber�a usar Scoped en su lugar.
builder.Services.AddSingleton<ITodoList, TodoList>();
builder.Services.AddScoped<ITodoListService, TodoListService>();
builder.Services.AddSingleton<ITodoListRepository, TodoListRepository>();

builder.Services.AddLogging(config =>
{
	config.AddConsole();
});

builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
