using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Bot;

public class ExampleModule : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("ping", "Pong!")]
    public static string Ping() => "Pong!";
}