namespace SH.Shared.Enums
{
    public enum PropertyAmenity
    {
        [FieldSettings(nameof(Translations.Enum.PropertyAmenity.Doorman), Icon = "user-check", ResourceType = typeof(Translations.Enum.PropertyAmenity))]
        Doorman = 1,

        [FieldSettings(nameof(Translations.Enum.PropertyAmenity.Security), Icon = "user-shield", ResourceType = typeof(Translations.Enum.PropertyAmenity))]
        Security = 2,

        [FieldSettings(nameof(Translations.Enum.PropertyAmenity.Custom), Icon = "sliders", ResourceType = typeof(Translations.Enum.PropertyAmenity))]
        Custom = 99,
    }
}