namespace SH.Shared.Enums
{
    public enum PropertyStructure
    {
        [FieldSettings(nameof(Translations.Enum.PropertyStructure.House), ResourceType = typeof(Translations.Enum.PropertyStructure))]
        House = 1,

        [FieldSettings(nameof(Translations.Enum.PropertyStructure.Apartment), ResourceType = typeof(Translations.Enum.PropertyStructure))]
        Apartment = 2
    }
}