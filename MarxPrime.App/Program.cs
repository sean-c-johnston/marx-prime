using Discord.Commands;
using Discord.WebSocket;
using MarxPrime.App;
using MarxPrime.App.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>()
            .AddSingleton<IDayService, DayService>()
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<CommandService>()
            .AddSingleton<CommandHandler>();
    })
    .Build();

await host.RunAsync();