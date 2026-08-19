namespace SH.Shared.Enums
{
    public enum Allowed
    {
        [FieldSettings(nameof(Translations.Enum.Allowed.Yes), Icon = "circle-check", ResourceType = typeof(Translations.Enum.Allowed))]
        Yes = 1,

        [FieldSettings(nameof(Translations.Enum.Allowed.No), Icon = "circle-xmark", ResourceType = typeof(Translations.Enum.Allowed))]
        No = 2,

        [FieldSettings(nameof(Translations.Enum.Allowed.Negotiable), Icon = "circle-exclamation", ResourceType = typeof(Translations.Enum.Allowed))]
        Negotiable = 3,
    }
}