namespace SH.Shared.Enums
{
    public enum AvailabilityMode
    {
        [FieldSettings(nameof(Translations.Enum.AvailabilityMode.Auto), ResourceType = typeof(Translations.Enum.AvailabilityMode))]
        Auto = 1,

        [FieldSettings(nameof(Translations.Enum.AvailabilityMode.Manual), ResourceType = typeof(Translations.Enum.AvailabilityMode))]
        Manual = 2
    }
}