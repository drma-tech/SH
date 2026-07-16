namespace SH.Shared.Enums
{
    public enum Allowed
    {
        [FieldSettings(nameof(Translations.Enum.Allowed.Yes), ResourceType = typeof(Translations.Enum.Allowed))]
        Yes = 1,

        [FieldSettings(nameof(Translations.Enum.Allowed.No), ResourceType = typeof(Translations.Enum.Allowed))]
        No = 2,

        [FieldSettings(nameof(Translations.Enum.Allowed.Negotiable), ResourceType = typeof(Translations.Enum.Allowed))]
        Negotiable = 3
    }
}