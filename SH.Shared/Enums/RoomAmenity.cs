namespace SH.Shared.Enums
{
    public enum RoomAmenity
    {
        [FieldSettings(nameof(Translations.Enum.RoomAmenity.WiFi), Icon = "wifi", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        WiFi = 1,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Furnished), Icon = "couch", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Furnished = 2,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Closet), Icon = "shirt", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Closet = 3,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.LockableRoom), Icon = "house-lock", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        LockableRoom = 4,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Workspace), Icon = "house-laptop", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Workspace = 5,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Heating), Icon = "fire", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Heating = 6,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.AirConditioning), Icon = "snowflake", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        AirConditioning = 7,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.WashingMachine), Icon = "jug-detergent", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        WashingMachine = 8,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Dryer), Icon = "sun", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Dryer = 9,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.PrivateRefrigerator), Icon = "glass-water", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        PrivateRefrigerator = 10,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.TV), Icon = "tv", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        TV = 11,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Dishwasher), Icon = "bowl-food", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Dishwasher = 12,

        [FieldSettings(nameof(Translations.Enum.RoomAmenity.Custom), Icon = "sliders", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Custom = 99,
    }
}