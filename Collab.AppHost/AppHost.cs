var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.Collab_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddJavaScriptApp("angular", "../Collab.Web", runScriptName: "start")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithExternalHttpEndpoints()
    .WithUrl("http://localhost:4200");

builder.Build().Run();
