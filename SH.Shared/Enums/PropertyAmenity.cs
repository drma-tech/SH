namespace SH.Shared.Enums
{
    public enum PropertyAmenity
    {
        [FieldSettings(nameof(Translations.Enum.PropertyAmenity.Doorman), ResourceType = typeof(Translations.Enum.PropertyAmenity))]
        Doorman = 1,

        [FieldSettings(nameof(Translations.Enum.PropertyAmenity.Security), ResourceType = typeof(Translations.Enum.PropertyAmenity))]
        Security = 2,

        [FieldSettings(nameof(Translations.Enum.PropertyAmenity.Custom), ResourceType = typeof(Translations.Enum.PropertyAmenity))]
        Custom = 99
    }
}