namespace SH.Shared.Enums
{
    public enum Occupation
    {
        [FieldSettings("Student", ResourceType = typeof(Translations.Enum.Occupation))]
        Student = 1,

        [FieldSettings("Employed", ResourceType = typeof(Translations.Enum.Occupation))]
        Employed = 2,

        [FieldSettings("Retired", ResourceType = typeof(Translations.Enum.Occupation))]
        Retired = 3,

        [FieldSettings("Unemployed", ResourceType = typeof(Translations.Enum.Occupation))]
        Unemployed = 4,
    }
}