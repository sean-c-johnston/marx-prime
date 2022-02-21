using Discord;
using Discord.WebSocket;
using MarxPrime.App.Services;

namespace MarxPrime.App;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly DiscordSocketClient _client;
    private readonly CommandHandler _handler;

    public Worker(ILogger<Worker> logger, DiscordSocketClient client, CommandHandler handler)
    {
        _logger = logger;
        _client = client;
        _handler = handler;
    }
    
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting discord bot");
        
        var botToken = Environment.GetEnvironmentVariable("DISCBOT_TOKEN");
        if (botToken == null) 
            throw new Exception("DISCBOT_TOKEN environment variable was not found.");
        
        _client.Log += LogAsync;
    
        await _client.LoginAsync(TokenType.Bot, botToken);
        await _client.StartAsync();
        
        await _handler.RegisterCommandsAsync();
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _client.Log -= LogAsync;
        await _handler.DeregisterCommandsAsync();
        await _client.LogoutAsync();
        await _client.DisposeAsync();
        _logger.LogInformation("Discord bot stopping");
    }

    private Task LogAsync(LogMessage msg)
    {
        _logger.LogInformation(msg.Message, DateTimeOffset.Now);
        return Task.CompletedTask;
    }
}