using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load Ocelot Routes from JSON
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("Configuration/ocelot.json", optional: false, reloadOnChange: true);

// Dynamically load other route configuration files (except ocelot.json)
//foreach (var file in Directory.GetFiles("Configuration", "*.json"))
//{
//    if (!file.EndsWith("ocelot.json"))
//    {
//        builder.Configuration.AddJsonFile(file, optional: false, reloadOnChange: true);
//    }
//}

builder.Services.AddOcelot(builder.Configuration);

builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAll");
app.UseOcelot().Wait();

app.UseOcelot().ConfigureAwait(false).GetAwaiter().OnCompleted(() =>
{
    Console.WriteLine("Request handled by Ocelot");
});

app.Run();