namespace SH.Shared.Enums
{
    public enum RoomService
    {
        [FieldSettings(nameof(Translations.Enum.RoomService.RoomCleaning), Icon = "broom", ResourceType = typeof(Translations.Enum.RoomService))]
        RoomCleaning = 1,

        [FieldSettings(nameof(Translations.Enum.RoomService.Laundry), Icon = "shirt", ResourceType = typeof(Translations.Enum.RoomService))]
        Laundry = 2,

        [FieldSettings(nameof(Translations.Enum.RoomService.Meals), Icon = "burger", ResourceType = typeof(Translations.Enum.RoomService))]
        Meals = 3,

        [FieldSettings(nameof(Translations.Enum.RoomService.Breakfast), Icon = "mug-hot", ResourceType = typeof(Translations.Enum.RoomService))]
        Breakfast = 4,

        [FieldSettings(nameof(Translations.Enum.RoomService.AirportPickup), Icon = "plane-arrival", ResourceType = typeof(Translations.Enum.RoomService))]
        AirportPickup = 5,

        [FieldSettings(nameof(Translations.Enum.RoomService.Custom), Icon = "sliders", ResourceType = typeof(Translations.Enum.RoomService))]
        Custom = 99,
    }
}