using Discord.Commands;
using MarxPrime.App.Services;

namespace MarxPrime.App.CommandModules;

public class PickleModule : ModuleBase<SocketCommandContext>
{
    private readonly IDayService _dayService;
    private const string BasePath = "Content/Images/Garfield";

    public PickleModule(IDayService dayService)
    {
        _dayService = dayService;
    }

    [Command("pickle")]
    [Summary("Tom 'Longman' Mooney. !pickle name reason")]
    public async Task GarfieldMeme(string? name = null, string? job = null, string? reason = null)
    {
        if (name == null || job == null || reason == null)
        {
            name = "rick sanchez";
            job = "mad scientist";
            reason = "so he didn't have to go to a therapy session with his family";
        }
        
        await Context.Channel.SendMessageAsync(GetMessage(name, job, reason));
    }

    private string GetMessage(string name, string job, string reason)
    {
        var splitName = name.Split(" ");
        var isMultipartName = splitName.Length > 1;
        
        var firstname = isMultipartName ? splitName[0] : name;
        
        return $"If you aren't familar with pickle {firstname}, hes the pickle version of {name} a {job} who turned himself into a pickle {reason} and instead had an amazing adventure that day as a pickle.";
    }
}