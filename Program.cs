// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Hosting;
using NetCord;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;
using NetCord.Rest;


var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDiscordGateway(options =>
{
    options.Token = builder.Configuration["Discord:Token"]!;
}).AddApplicationCommands();

var host = builder.Build();

host.AddUserCommand("Username", (User user) => user.Username)
    .AddMessageCommand("Length", (RestMessage m) => m.Content.Length.ToString());

// Add commands from modules
host.AddModules(typeof(Program).Assembly);

await host.RunAsync();

