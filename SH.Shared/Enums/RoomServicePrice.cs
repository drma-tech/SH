namespace SH.Shared.Enums
{
    public enum RoomServicePrice
    {
        [FieldSettings("Included", ResourceType = typeof(Translations.Enum.RoomServicePrice))]
        Included = 1,

        [FieldSettings("Extra", ResourceType = typeof(Translations.Enum.RoomServicePrice))]
        Extra = 2,
    }
}