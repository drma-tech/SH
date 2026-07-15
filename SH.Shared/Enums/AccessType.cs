namespace SH.Shared.Enums
{
    public enum AccessType
    {
        [FieldSettings("Private", ResourceType = typeof(Translations.Enum.AccessType))]
        Private = 1,

        [FieldSettings("Shared", ResourceType = typeof(Translations.Enum.AccessType))]
        Shared = 2
    }
}