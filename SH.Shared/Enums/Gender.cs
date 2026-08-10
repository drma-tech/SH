namespace SH.Shared.Enums
{
    public enum Gender
    {
        [FieldSettings(nameof(Translations.Enum.Gender.Man), ResourceType = typeof(Translations.Enum.Gender))]
        Man = 1,

        [FieldSettings(nameof(Translations.Enum.Gender.Woman), ResourceType = typeof(Translations.Enum.Gender))]
        Woman = 2,

        [FieldSettings(nameof(Translations.Enum.Gender.NonBinary), ResourceType = typeof(Translations.Enum.Gender))]
        NonBinary = 3,
    }
}