namespace SH.Shared.Enums
{
    public enum Occupation
    {
        [FieldSettings(nameof(Translations.Enum.Occupation.Student), Icon = "user-graduate", ResourceType = typeof(Translations.Enum.Occupation))]
        Student = 1,

        [FieldSettings(nameof(Translations.Enum.Occupation.Employed), Icon = "briefcase", ResourceType = typeof(Translations.Enum.Occupation))]
        Employed = 2,

        [FieldSettings(nameof(Translations.Enum.Occupation.Retired), Icon = "person-walking-luggage", ResourceType = typeof(Translations.Enum.Occupation))]
        Retired = 3,

        [FieldSettings(nameof(Translations.Enum.Occupation.Unemployed), Icon = "house-user", ResourceType = typeof(Translations.Enum.Occupation))]
        Unemployed = 4,
    }
}