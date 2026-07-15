namespace SH.Shared.Enums
{
    public enum Allowed
    {
        [FieldSettings("Yes", ResourceType = typeof(Translations.Enum.Allowed))]
        Yes = 1,

        [FieldSettings("No", ResourceType = typeof(Translations.Enum.Allowed))]
        No = 2,

        [FieldSettings("Negotiable", ResourceType = typeof(Translations.Enum.Allowed))]
        Negotiable = 3
    }
}