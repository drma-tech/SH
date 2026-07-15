namespace SH.Shared.Enums
{
    public enum Gender
    {
        [FieldSettings("Man", ResourceType = typeof(Translations.Enum.Gender))]
        Man = 1,

        [FieldSettings("Woman", ResourceType = typeof(Translations.Enum.Gender))]
        Woman = 2,

        [FieldSettings("NonBinary", ResourceType = typeof(Translations.Enum.Gender))]
        NonBinary = 3
    }
}