namespace SH.Shared.Enums
{
    public enum PropertyAmenity
    {
        [FieldSettings("Doorman", ResourceType = typeof(Translations.Enum.PropertyAmenity))]
        Doorman = 1,

        [FieldSettings("Security", ResourceType = typeof(Translations.Enum.PropertyAmenity))]
        Security = 2,

        [FieldSettings("Custom", ResourceType = typeof(Translations.Enum.PropertyAmenity))]
        Custom = 99
    }
}