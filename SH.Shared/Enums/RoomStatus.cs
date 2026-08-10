namespace SH.Shared.Enums
{
    public enum RoomStatus
    {
        [FieldSettings(nameof(Translations.Enum.RoomStatus.Available), ResourceType = typeof(Translations.Enum.RoomStatus))]
        Available = 1,

        [FieldSettings(nameof(Translations.Enum.RoomStatus.Occupied), ResourceType = typeof(Translations.Enum.RoomStatus))]
        Occupied = 2,

        [FieldSettings(nameof(Translations.Enum.RoomStatus.Unavailable), ResourceType = typeof(Translations.Enum.RoomStatus))]
        Unavailable = 3,
    }
}