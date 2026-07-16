namespace SH.Shared.Enums
{
    public enum AccessibilityFeature
    {
        [FieldSettings(nameof(Translations.Enum.AccessibilityFeature.WheelchairAccessible), ResourceType = typeof(Translations.Enum.AccessibilityFeature))]
        WheelchairAccessible = 1,

        [FieldSettings(nameof(Translations.Enum.AccessibilityFeature.StepFreeEntrance), ResourceType = typeof(Translations.Enum.AccessibilityFeature))]
        StepFreeEntrance = 2,

        [FieldSettings(nameof(Translations.Enum.AccessibilityFeature.AccessibleElevator), ResourceType = typeof(Translations.Enum.AccessibilityFeature))]
        AccessibleElevator = 3
    }
}