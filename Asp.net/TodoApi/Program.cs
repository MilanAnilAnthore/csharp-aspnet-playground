using TodoApi.Repository;
using TodoCliApp.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

{
    // Add services to the container.
    builder.Services.AddControllers()
            .AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
             });
    builder.Services.AddOpenApi();
    builder.Services.AddScoped<ITodoRepository, JsonTodoRepository>();
    builder.Services.AddScoped<IServiceRepository, TodoService>();
}

var app = builder.Build();

{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.Run();



