namespace SH.Shared.Enums
{
    public enum PaymentType
    {
        [FieldSettings(nameof(Translations.Enum.PaymentType.Rent), ResourceType = typeof(Translations.Enum.PaymentType))]
        Rent = 1,

        [FieldSettings(nameof(Translations.Enum.PaymentType.Deposit), ResourceType = typeof(Translations.Enum.PaymentType))]
        Deposit = 2,

        [FieldSettings(nameof(Translations.Enum.PaymentType.Bills), ResourceType = typeof(Translations.Enum.PaymentType))]
        Bills = 3,

        [FieldSettings(nameof(Translations.Enum.PaymentType.Custom), ResourceType = typeof(Translations.Enum.PaymentType))]
        Custom = 99
    }
}