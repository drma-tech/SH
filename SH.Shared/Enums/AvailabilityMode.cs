namespace SH.Shared.Enums
{
    public enum AvailabilityMode
    {
        [FieldSettings("Auto", ResourceType = typeof(Translations.Enum.AvailabilityMode))]
        Auto = 1,

        [FieldSettings("Manual", ResourceType = typeof(Translations.Enum.AvailabilityMode))]
        Manual = 2
    }
}