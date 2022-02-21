using Discord.Commands;

namespace MarxPrime.App.CommandModules;

public class PingModule : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    [Summary("Ping bot to check if alive.")]
    public Task Ping() => ReplyAsync("pong!");
}