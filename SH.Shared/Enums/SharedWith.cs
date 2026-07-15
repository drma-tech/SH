namespace SH.Shared.Enums
{
    public enum SharedWith
    {
        [FieldSettings("Owner", ResourceType = typeof(Translations.Enum.SharedWith))]
        Owner = 1,

        [FieldSettings("OtherTenants", ResourceType = typeof(Translations.Enum.SharedWith))]
        OtherTenants = 2
    }
}