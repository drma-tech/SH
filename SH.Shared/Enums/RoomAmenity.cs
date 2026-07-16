namespace SH.Shared.Enums
{
    public enum RoomAmenity
    {
        [FieldSettings(nameof(Translations.Enum.RoomAmenity.WiFi), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        WiFi = 1,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Furnished), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Furnished = 2,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Closet), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Closet = 3,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.LockableRoom), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        LockableRoom = 4,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Workspace), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Workspace = 5,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Heating), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Heating = 6,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.AirConditioning), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        AirConditioning = 7,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.WashingMachine), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        WashingMachine = 8,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Dryer), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Dryer = 9,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.PrivateRefrigerator), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        PrivateRefrigerator = 10,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.TV), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        TV = 11,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Dishwasher), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Dishwasher = 12,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Custom), ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Custom = 99
    }
}