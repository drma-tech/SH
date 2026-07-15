namespace SH.Shared.Enums
{
    public enum RoomAmenity
    {
        [FieldSettings("WiFi", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        WiFi = 1,

        [FieldSettings("Furnished", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Furnished = 2,

        [FieldSettings("Closet", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Closet = 3,

        [FieldSettings("LockableRoom", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        LockableRoom = 4,

        [FieldSettings("Workspace", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Workspace = 5,

        [FieldSettings("Heating", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Heating = 6,

        [FieldSettings("AirConditioning", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        AirConditioning = 7,

        [FieldSettings("WashingMachine", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        WashingMachine = 8,

        [FieldSettings("Dryer", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Dryer = 9,

        [FieldSettings("PrivateRefrigerator", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        PrivateRefrigerator = 10,

        [FieldSettings("TV", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        TV = 11,

        [FieldSettings("Dishwasher", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Dishwasher = 12,

        [FieldSettings("Custom", ResourceType = typeof(Translations.Enum.RoomAmenity))]
        Custom = 99
    }
}