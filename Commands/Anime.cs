using NetCord;
using NetCord.Gateway;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Bot;

public class AnimeModule : ApplicationCommandModule<ApplicationCommandContext>
{
    [SlashCommand("anime", "Searches an Anime")]
    public async Task SearchAnimeAsync(string name)
    {
        Dictionary<string, string> animes = new Dictionary<string, string>();
        animes["naruto"] = "naruto is an anime";
        if (animes.TryGetValue(name, out string? animeresponse))
        {
            await Context.Interaction.SendResponseAsync(InteractionCallback.Message(animeresponse));
        }
        else
        {
            await Context.Interaction.SendResponseAsync(InteractionCallback.Message($"Anime {name} not found"));
        }
    }

}