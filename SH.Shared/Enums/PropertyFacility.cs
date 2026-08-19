namespace SH.Shared.Enums
{
    public enum PropertyFacility
    {
        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Pool), Icon = "person-swimming", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Pool = 1,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Gym), Icon = "dumbbell", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Gym = 2,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Parking), Icon = "square-parking", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Parking = 3,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.LaundryRoom), Icon = "jug-detergent", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        LaundryRoom = 4,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Playground), Icon = "children", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Playground = 5,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Coworking), Icon = "laptop", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Coworking = 6,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.GrillArea), Icon = "fire-burner", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        GrillArea = 7,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Elevator), Icon = "elevator", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Elevator = 8,

        [FieldSettings(nameof(Translations.Enum.PropertyFacility.Custom), Icon = "sliders", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Custom = 99,
    }
}