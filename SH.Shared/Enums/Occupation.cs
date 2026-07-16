namespace SH.Shared.Enums
{
    public enum Occupation
    {
        [FieldSettings(nameof(Translations.Enum.Occupation.Student), ResourceType = typeof(Translations.Enum.Occupation))]
        Student = 1,

        [FieldSettings(nameof(Translations.Enum.Occupation.Employed), ResourceType = typeof(Translations.Enum.Occupation))]
        Employed = 2,

        [FieldSettings(nameof(Translations.Enum.Occupation.Retired), ResourceType = typeof(Translations.Enum.Occupation))]
        Retired = 3,

        [FieldSettings(nameof(Translations.Enum.Occupation.Unemployed), ResourceType = typeof(Translations.Enum.Occupation))]
        Unemployed = 4,
    }
}