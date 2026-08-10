namespace SH.Shared.Enums
{
    public enum RoomService
    {
        [FieldSettings(nameof(Translations.Enum.RoomService.RoomCleaning), ResourceType = typeof(Translations.Enum.RoomService))]
        RoomCleaning = 1,

        [FieldSettings(nameof(Translations.Enum.RoomService.Laundry), ResourceType = typeof(Translations.Enum.RoomService))]
        Laundry = 2,

        [FieldSettings(nameof(Translations.Enum.RoomService.Meals), ResourceType = typeof(Translations.Enum.RoomService))]
        Meals = 3,

        [FieldSettings(nameof(Translations.Enum.RoomService.Breakfast), ResourceType = typeof(Translations.Enum.RoomService))]
        Breakfast = 4,

        [FieldSettings(nameof(Translations.Enum.RoomService.AirportPickup), ResourceType = typeof(Translations.Enum.RoomService))]
        AirportPickup = 5,

        [FieldSettings(nameof(Translations.Enum.RoomService.Custom), ResourceType = typeof(Translations.Enum.RoomService))]
        Custom = 99,
    }
}