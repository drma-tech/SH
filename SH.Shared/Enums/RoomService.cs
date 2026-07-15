namespace SH.Shared.Enums
{
    public enum RoomService
    {
        [FieldSettings("RoomCleaning", ResourceType = typeof(Translations.Enum.RoomService))]
        RoomCleaning = 1,

        [FieldSettings("Laundry", ResourceType = typeof(Translations.Enum.RoomService))]
        Laundry = 2,

        [FieldSettings("Meals", ResourceType = typeof(Translations.Enum.RoomService))]
        Meals = 3,

        [FieldSettings("Breakfast", ResourceType = typeof(Translations.Enum.RoomService))]
        Breakfast = 4,

        [FieldSettings("AirportPickup", ResourceType = typeof(Translations.Enum.RoomService))]
        AirportPickup = 5,

        [FieldSettings("Custom", ResourceType = typeof(Translations.Enum.RoomService))]
        Custom = 99
    }
}