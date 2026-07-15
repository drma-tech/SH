namespace SH.Shared.Enums
{
    public enum StayInterval
    {
        [FieldSettings("Week", ResourceType = typeof(Translations.Enum.StayInterval))]
        Week = 1,

        [FieldSettings("Month", ResourceType = typeof(Translations.Enum.StayInterval))]
        Month = 3,
    }
}