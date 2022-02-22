using System.Globalization;
using Discord.Commands;
using MarxPrime.App.Services;

namespace MarxPrime.App.CommandModules;

public class GarfieldModule : ModuleBase<SocketCommandContext>
{
    private readonly IDayService _dayService;
    private const string BasePath = "Content/Images/Garfield";

    public GarfieldModule(IDayService dayService)
    {
        _dayService = dayService;
    }

    [Command("meme")]
    [Summary("Garfield")]
    public async Task GarfieldMeme([Remainder] string? day = null)
    {
        var dayOfWeek = _dayService.GetCurrentDayOfWeek();

        if (day != null) dayOfWeek = _dayService.GetDayOfWeek(day);

        var response = dayOfWeek switch
        {
            DayOfWeek.Sunday => Context.Channel.SendMessageAsync("It's Sunday. No meme."),
            DayOfWeek.Monday => Context.Channel.SendFileAsync($"{BasePath}/Monday.png", "meme"),
            DayOfWeek.Tuesday => Context.Channel.SendFileAsync($"{BasePath}/Tuesday.png", "meme"),
            DayOfWeek.Wednesday => Context.Channel.SendFileAsync($"{BasePath}/Wednesday.png", "meme"),
            DayOfWeek.Thursday => Context.Channel.SendFileAsync($"{BasePath}/Thursday.png", "meme"),
            DayOfWeek.Friday => Context.Channel.SendMessageAsync("It's Friday. No meme."),
            DayOfWeek.Saturday => Context.Channel.SendMessageAsync("It's Saturday. No meme."),
            _ => Context.Channel.SendMessageAsync("Now you're just making days up."),
        };
        
        await response;
    }
}