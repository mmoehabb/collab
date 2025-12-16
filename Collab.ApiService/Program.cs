using Collab.ApiService;
using Collab.ApiService.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi;
using Collab.ApiService.Models;
using Collab.ApiService.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddSignalR(options => {
    options.EnableDetailedErrors = true;
});
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Collab Api",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = Microsoft.OpenApi.ParameterLocation.Header,
        Name = "Authorization",
        Description = "Enter your access token",
        Type = Microsoft.OpenApi.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
});

builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddTransient<
    Collab.ApiService.Interfaces.IUserRepository,
    Collab.ApiService.Repositories.UserRepository
>();

builder.Services.AddTransient<
    Collab.ApiService.Interfaces.IChannelRepository,
    Collab.ApiService.Repositories.ChannelRepository
>();

builder.Services.AddTransient<
    Collab.ApiService.Interfaces.IMessageRepository,
    Collab.ApiService.Repositories.MessageRepository
>();

// Add CORS services
var policyName = "_MyCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(policyName, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader() 
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

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

app.UseCors(policyName);
app.MapHub<ChannelHub>("/hub");
app.MapGet("/", () => "API service is running. Dispath requests to /api/v1/{entity}/{method}.");
app.MapGet("/api/v1", () => "API service is running.");

app.UseHttpsRedirection();
app.MapDefaultEndpoints();
app.MapControllers();
app.MapIdentityApi<User>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    DataSeeder.SeedData(context, services); 
}

app.Run();
