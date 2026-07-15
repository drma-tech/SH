namespace SH.Shared.Enums
{
    public enum PropertyStructure
    {
        [FieldSettings("House", ResourceType = typeof(Translations.Enum.PropertyStructure))]
        House = 1,

        [FieldSettings("Apartment", ResourceType = typeof(Translations.Enum.PropertyStructure))]
        Apartment = 2
    }
}