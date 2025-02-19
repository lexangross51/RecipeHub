using RecipeHub.WebServer;

var builder = Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(b => b.UseStartup<Startup>());

var app = builder.Build();
app.Run();