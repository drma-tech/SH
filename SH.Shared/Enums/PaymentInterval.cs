namespace SH.Shared.Enums
{
    public enum PaymentInterval
    {
        [FieldSettings(nameof(Translations.Enum.PaymentInterval.Once), ResourceType = typeof(Translations.Enum.PaymentInterval))]
        Once = 1,

        [FieldSettings(nameof(Translations.Enum.PaymentInterval.Weekly), ResourceType = typeof(Translations.Enum.PaymentInterval))]
        Weekly = 2,

        [FieldSettings(nameof(Translations.Enum.PaymentInterval.EveryTwoWeeks), ResourceType = typeof(Translations.Enum.PaymentInterval))]
        EveryTwoWeeks = 3,

        [FieldSettings(nameof(Translations.Enum.PaymentInterval.Monthly), ResourceType = typeof(Translations.Enum.PaymentInterval))]
        Monthly = 4,
    }
}