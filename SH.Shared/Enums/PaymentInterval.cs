namespace SH.Shared.Enums
{
    public enum PaymentInterval
    {
        [FieldSettings("Once", ResourceType = typeof(Translations.Enum.PaymentInterval))]
        Once = 1,

        [FieldSettings("Weekly", ResourceType = typeof(Translations.Enum.PaymentInterval))]
        Weekly = 2,

        [FieldSettings("EveryTwoWeeks", ResourceType = typeof(Translations.Enum.PaymentInterval))]
        EveryTwoWeeks = 3,

        [FieldSettings("Monthly", ResourceType = typeof(Translations.Enum.PaymentInterval))]
        Monthly = 4
    }
}