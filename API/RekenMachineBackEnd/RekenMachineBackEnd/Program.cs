using RekenMachineBackEnd;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<Service>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseAuthorization();
app.MapControllers();

app.Run();
