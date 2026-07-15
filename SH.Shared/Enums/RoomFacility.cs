namespace SH.Shared.Enums
{
    public enum RoomFacility
    {
        [FieldSettings("Bathroom", ResourceType = typeof(Translations.Enum.RoomFacility))]
        Bathroom = 1,

        [FieldSettings("Kitchen", ResourceType = typeof(Translations.Enum.RoomFacility))]
        Kitchen = 2,

        [FieldSettings("Parking", ResourceType = typeof(Translations.Enum.RoomFacility))]
        Parking = 3,

        [FieldSettings("Balcony", ResourceType = typeof(Translations.Enum.RoomFacility))]
        Balcony = 4,

        [FieldSettings("StorageSpace", ResourceType = typeof(Translations.Enum.RoomFacility))]
        StorageSpace = 5,

        [FieldSettings("Custom", ResourceType = typeof(Translations.Enum.RoomFacility))]
        Custom = 99
    }
}