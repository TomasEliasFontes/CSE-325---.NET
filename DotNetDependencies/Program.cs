using Humanizer;

Console.WriteLine("Quantities:");
HumanizeQuantity();

Console.WriteLine("\nDate/Time Manipulation:");
HumanizeDates();

static void HumanizeQuantity()
{
    Console.WriteLine("case".ToQuantity(0)); // "no cases"
    Console.WriteLine("case".ToQuantity(1)); // "1 case"
    Console.WriteLine("case".ToQuantity(5)); // "5 cases"
}

static void HumanizeDates()
{
    Console.WriteLine(DateTime.UtcNow.AddHours(-24).Humanize()); // "yesterday"
    Console.WriteLine(DateTime.UtcNow.AddHours(-2).Humanize()); // "2 hours ago"
    Console.WriteLine(TimeSpan.FromDays(1).Humanize()); // "1 day"
    Console.WriteLine(TimeSpan.FromDays(16).Humanize()); // "16 days"
}