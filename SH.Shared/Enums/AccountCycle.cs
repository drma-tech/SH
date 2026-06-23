namespace SH.Shared.Enums;

public enum AccountCycle
{
    [Custom(Name = "Weekly", Description = "Week", ResourceType = typeof(Shared.Resources.Enum.AccountCycle))]
    Weekly = 1,

    [Custom(Name = "Monthly", Description = "Month", ResourceType = typeof(Shared.Resources.Enum.AccountCycle))]
    Monthly = 2,

    [Custom(Name = "Yearly", Description = "Year", ResourceType = typeof(Shared.Resources.Enum.AccountCycle))]
    Yearly = 3
}