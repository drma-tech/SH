using SH.Shared.Models.Property;

namespace SH.WEB.Modules.Profile.Core
{
    public static class PropertyModelHelper
    {
        public static IEnumerable<EnumFieldObject<PropertyFacility>> AsField(this ISet<PropertyFacilityModel> facilities)
        {
            foreach (var facility in facilities)
            {
                if (facility.Facility == PropertyFacility.Custom)
                {
                    var field = facility.Facility.GetFieldSettings();
                    field.Name = facility.CustomName ?? "Error";
                    yield return field;
                }
                else
                {
                    yield return facility.Facility.GetFieldSettings();
                }
            }
        }

        public static IEnumerable<EnumFieldObject<PropertyAmenity>> AsField(this ISet<PropertyAmenityModel> amenities)
        {
            foreach (var amenity in amenities)
            {
                if (amenity.Amenity == PropertyAmenity.Custom)
                {
                    var field = amenity.Amenity.GetFieldSettings();
                    field.Name = amenity.CustomName ?? "Error";
                    yield return field;
                }
                else
                {
                    yield return amenity.Amenity.GetFieldSettings();
                }
            }
        }

        public static IEnumerable<EnumFieldObject<AccessibilityFeature>> AsField(this ISet<AccessibilityFeature> accessibilities)
        {
            foreach (var accessibility in accessibilities)
            {
                yield return accessibility.GetFieldSettings();
            }
        }

        public static IEnumerable<EnumFieldObject<Language>> AsField(this IReadOnlyCollection<Language> languages)
        {
            foreach (var language in languages)
            {
                var field = language.GetFieldSettings();
                field.Icon = "language";
                yield return field;
            }
        }
    }
}