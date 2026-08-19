namespace SH.Shared.Enums
{
    public enum Gender
    {
        [FieldSettings(nameof(Translations.Enum.Gender.Man), Icon = "person", ResourceType = typeof(Translations.Enum.Gender))]
        Man = 1,

        [FieldSettings(nameof(Translations.Enum.Gender.Woman), Icon = "person-dress", ResourceType = typeof(Translations.Enum.Gender))]
        Woman = 2,

        [FieldSettings(nameof(Translations.Enum.Gender.NonBinary), Icon = "person-half-dress", ResourceType = typeof(Translations.Enum.Gender))]
        NonBinary = 3,
    }
}