using System.Globalization;

namespace MarxPrime.App.Services;

public class DayService : IDayService
{
    public DayOfWeek GetCurrentDayOfWeek()
    {
        return DateTimeOffset.Now.DayOfWeek;
    }

    public DayOfWeek GetDayOfWeek(string dayName)
    {
        var index = Array.FindIndex(
            CultureInfo.InvariantCulture.DateTimeFormat.AbbreviatedDayNames, 
            x => string.Equals(x, dayName, StringComparison.InvariantCultureIgnoreCase));
        
        var isAbbreviatedDayName = index >= 0;

        if (isAbbreviatedDayName)
        {
            return (DayOfWeek) index;
        }

        var isValidDay = Enum.TryParse<DayOfWeek>(dayName, ignoreCase: true, out var parsedDay);
        if (isValidDay) return parsedDay;

        return (DayOfWeek)(-1);
    }
}

public interface IDayService
{
    DayOfWeek GetDayOfWeek(string dayName);
    DayOfWeek GetCurrentDayOfWeek();
}