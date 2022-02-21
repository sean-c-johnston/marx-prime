using Discord.Commands;

namespace MarxPrime.App.CommandModules;

public class GarfieldModule : ModuleBase<SocketCommandContext>
{
    private const string Path = "Content/garfield.png";
    
    [Command("meme")]
    [Summary("Garfield")]
    public async Task GarfieldMeme() => await Context.Channel.SendFileAsync(Path, "meme");
}