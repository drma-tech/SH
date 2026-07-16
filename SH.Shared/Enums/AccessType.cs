namespace SH.Shared.Enums
{
    public enum AccessType
    {
        [FieldSettings(nameof(Translations.Enum.AccessType.Private), ResourceType = typeof(Translations.Enum.AccessType))]
        Private = 1,

        [FieldSettings(nameof(Translations.Enum.AccessType.Shared), ResourceType = typeof(Translations.Enum.AccessType))]
        Shared = 2
    }
}