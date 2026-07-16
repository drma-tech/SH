namespace SH.Shared.Enums
{
    public enum SharedWith
    {
        [FieldSettings(nameof(Translations.Enum.SharedWith.Owner), ResourceType = typeof(Translations.Enum.SharedWith))]
        Owner = 1,

        [FieldSettings(nameof(Translations.Enum.SharedWith.OtherTenants), ResourceType = typeof(Translations.Enum.SharedWith))]
        OtherTenants = 2
    }
}