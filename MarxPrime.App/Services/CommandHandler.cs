using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;

namespace MarxPrime.App.Services;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private readonly IServiceProvider _services;

    public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider services)
    {
        _client = client;
        _commands = commands;
        _services = services;
    }

    public async Task RegisterCommandsAsync()
    {
        _client.MessageReceived += HandleCommandAsync;
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
    }
    
    public async Task DeregisterCommandsAsync()
    {
        _client.MessageReceived += HandleCommandAsync;
        foreach (var module in _commands.Modules)
        {
            await _commands.RemoveModuleAsync(module);   
        }
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        if (messageParam is not SocketUserMessage msg) return;

        var prefixIndex = 0;

        if (!ShouldHandleMessage(msg, ref prefixIndex)) return;

        var context = new SocketCommandContext(_client, msg);
        await _commands.ExecuteAsync(context, prefixIndex, _services);
    }

    private bool ShouldHandleMessage(SocketUserMessage msg, ref int i)
    {
        var isPrefixed = msg.HasCharPrefix('!', ref i);
        var mentionsBot = msg.HasMentionPrefix(GetBotUser, ref i);
        var isBotMessage = msg.Author.IsBot;

        return (isPrefixed || mentionsBot) && !isBotMessage;
    }

    private SocketSelfUser GetBotUser => _client.CurrentUser;
}