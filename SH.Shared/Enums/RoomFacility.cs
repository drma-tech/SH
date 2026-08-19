namespace SH.Shared.Enums
{
    public enum RoomFacility
    {
        [FieldSettings(nameof(Translations.Enum.RoomFacility.Bathroom), Icon = "shower", ResourceType = typeof(Translations.Enum.RoomFacility))]
        Bathroom = 1,

        [FieldSettings(nameof(Translations.Enum.RoomFacility.Kitchen), Icon = "kitchen-set", ResourceType = typeof(Translations.Enum.RoomFacility))]
        Kitchen = 2,

        [FieldSettings(nameof(Translations.Enum.RoomFacility.Parking), Icon = "square-parking", ResourceType = typeof(Translations.Enum.RoomFacility))]
        Parking = 3,

        [FieldSettings(nameof(Translations.Enum.RoomFacility.Balcony), Icon = "seedling", ResourceType = typeof(Translations.Enum.RoomFacility))]
        Balcony = 4,

        [FieldSettings(nameof(Translations.Enum.RoomFacility.StorageSpace), Icon = "warehouse", ResourceType = typeof(Translations.Enum.RoomFacility))]
        StorageSpace = 5,

        [FieldSettings(nameof(Translations.Enum.RoomFacility.Custom), Icon = "sliders", ResourceType = typeof(Translations.Enum.RoomFacility))]
        Custom = 99,
    }
}