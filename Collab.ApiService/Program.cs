using Collab.ApiService;
using Collab.ApiService.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddTransient<
    Collab.ApiService.Interfaces.IUserRepository,
    Collab.ApiService.Repositories.UserRepository
>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "API service is running. Dispath requests to /api/v1/{entity}/{method}.");
app.MapGet("/api/v1", () => "API service is running.");

app.UseHttpsRedirection();
app.MapDefaultEndpoints();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    DataSeeder.SeedData(context, services); 
}

app.Run();
