namespace SH.Shared.Enums
{
    public enum RoomFacility
    {
        [FieldSettings(nameof(Translations.Enum.RoomFacility.Bathroom), ResourceType = typeof(Translations.Enum.RoomFacility))]
        Bathroom = 1,

        [FieldSettings(nameof(Translations.Enum.RoomFacility.Kitchen), ResourceType = typeof(Translations.Enum.RoomFacility))]
        Kitchen = 2,

        [FieldSettings(nameof(Translations.Enum.RoomFacility.Parking), ResourceType = typeof(Translations.Enum.RoomFacility))]
        Parking = 3,

        [FieldSettings(nameof(Translations.Enum.RoomFacility.Balcony), ResourceType = typeof(Translations.Enum.RoomFacility))]
        Balcony = 4,

        [FieldSettings(nameof(Translations.Enum.RoomFacility.StorageSpace), ResourceType = typeof(Translations.Enum.RoomFacility))]
        StorageSpace = 5,

        [FieldSettings(nameof(Translations.Enum.RoomFacility.Custom), ResourceType = typeof(Translations.Enum.RoomFacility))]
        Custom = 99
    }
}