namespace SH.Shared.Enums
{
    public enum RoomServicePrice
    {
        [FieldSettings(nameof(Translations.Enum.RoomServicePrice.Included), ResourceType = typeof(Translations.Enum.RoomServicePrice))]
        Included = 1,

        [FieldSettings(nameof(Translations.Enum.RoomServicePrice.Extra), ResourceType = typeof(Translations.Enum.RoomServicePrice))]
        Extra = 2,
    }
}