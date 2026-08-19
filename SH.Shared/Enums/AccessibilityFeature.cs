namespace SH.Shared.Enums
{
    public enum AccessibilityFeature
    {
        [FieldSettings(nameof(Translations.Enum.AccessibilityFeature.WheelchairAccessible), Icon = "wheelchair", ResourceType = typeof(Translations.Enum.AccessibilityFeature))]
        WheelchairAccessible = 1,

        [FieldSettings(nameof(Translations.Enum.AccessibilityFeature.StepFreeEntrance), Icon = "wheelchair-move", ResourceType = typeof(Translations.Enum.AccessibilityFeature))]
        StepFreeEntrance = 2,

        [FieldSettings(nameof(Translations.Enum.AccessibilityFeature.AccessibleElevator), Icon = "elevator", ResourceType = typeof(Translations.Enum.AccessibilityFeature))]
        AccessibleElevator = 3,
    }
}