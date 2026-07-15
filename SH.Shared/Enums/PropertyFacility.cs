namespace SH.Shared.Enums
{
    public enum PropertyFacility
    {
        [FieldSettings("Pool", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Pool = 1,

        [FieldSettings("Gym", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Gym = 2,

        [FieldSettings("Parking", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Parking = 3,

        [FieldSettings("LaundryRoom", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        LaundryRoom = 4,

        [FieldSettings("Playground", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Playground = 5,

        [FieldSettings("Coworking", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Coworking = 6,

        [FieldSettings("GrillArea", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        GrillArea = 7,

        [FieldSettings("Elevator", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Elevator = 8,

        [FieldSettings("Custom", ResourceType = typeof(Translations.Enum.PropertyFacility))]
        Custom = 99
    }
}