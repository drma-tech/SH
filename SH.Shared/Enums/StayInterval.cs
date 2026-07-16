namespace SH.Shared.Enums
{
    public enum StayInterval
    {
        [FieldSettings(nameof(Translations.Enum.StayInterval.Week), ResourceType = typeof(Translations.Enum.StayInterval))]
        Week = 1,

        [FieldSettings(nameof(Translations.Enum.StayInterval.Month), ResourceType = typeof(Translations.Enum.StayInterval))]
        Month = 3,
    }
}