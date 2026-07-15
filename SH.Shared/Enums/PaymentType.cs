namespace SH.Shared.Enums
{
    public enum PaymentType
    {
        [FieldSettings("Rent", ResourceType = typeof(Translations.Enum.PaymentType))]
        Rent = 1,

        [FieldSettings("Deposit", ResourceType = typeof(Translations.Enum.PaymentType))]
        Deposit = 2,

        [FieldSettings("Bills", ResourceType = typeof(Translations.Enum.PaymentType))]
        Bills = 3,

        [FieldSettings("Custom", ResourceType = typeof(Translations.Enum.PaymentType))]
        Custom = 99
    }
}