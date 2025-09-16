using HomeServer.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure database path and register DbContext with SQLite
builder.Services.AddDbContext<TodoListDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Add essential services for controllers and API documentation
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register application services
builder.Services.AddScoped<TodoEntity>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

// Ensure the database is created and migrations are applied at startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoListDbContext>();
    //db.Database.Migrate(); // Applies any pending migrations (only for development, for production use CI/CD)
}

// Configure middleware
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
