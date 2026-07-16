namespace SH.Shared.Enums
{
    public enum RoomStatus
    {
        [FieldSettings("Available", ResourceType = typeof(Translations.Enum.RoomStatus))]
        Available = 1,

        [FieldSettings("Occupied", ResourceType = typeof(Translations.Enum.RoomStatus))]
        Occupied = 2,

        [FieldSettings("Unavailable", ResourceType = typeof(Translations.Enum.RoomStatus))]
        Unavailable = 3
    }
}