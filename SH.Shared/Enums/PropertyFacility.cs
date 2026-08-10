namespace SH.Shared.Enums
{
    public enum PropertyFacility
    {
        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Pool), ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Pool = 1,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Gym), ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Gym = 2,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Parking), ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Parking = 3,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.LaundryRoom), ResourceType = typeof(Translations.Enum.PropertyFacility))]
        LaundryRoom = 4,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Playground), ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Playground = 5,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Coworking), ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Coworking = 6,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.GrillArea), ResourceType = typeof(Translations.Enum.PropertyFacility))]
        GrillArea = 7,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Elevator), ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Elevator = 8,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Custom), ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Custom = 99,
    }
}