namespace SH.Shared.Enums
{
    public enum StayInterval
    {
        [FieldSettings(nameof(Translations.Enum.StayInterval.Week), ResourceType = typeof(Translations.Enum.StayInterval))]
        Week = 2,

        [FieldSettings(nameof(Translations.Enum.StayInterval.Month), ResourceType = typeof(Translations.Enum.StayInterval))]
        Month = 3,
    }
}