namespace SH.Shared.Enums
{
    public enum AccessibilityFeature
    {
        [FieldSettings("WheelchairAccessible", ResourceType = typeof(Translations.Enum.AccessibilityFeature))]
        WheelchairAccessible = 1,

        [FieldSettings("StepFreeEntrance", ResourceType = typeof(Translations.Enum.AccessibilityFeature))]
        StepFreeEntrance = 2,

        [FieldSettings("AccessibleElevator", ResourceType = typeof(Translations.Enum.AccessibilityFeature))]
        AccessibleElevator = 3
    }
}