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

        public static IEnumerable<EnumFieldObject<RoomFacility>> AsField(this ISet<RoomFacilityModel> facilities)
        {
            foreach (var item in facilities)
            {
                var field = item.Facility.GetFieldSettings();

                if (item.Facility == RoomFacility.Custom)
                {
                    field.Name = $"{item.CustomName} ({item.AccessType.GetFieldSettings().Name})";
                    yield return field;
                }
                else
                {
                    field.Name = $"{item.Facility.GetFieldSettings().Name} ({item.AccessType.GetFieldSettings().Name})";
                    yield return field;
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

        public static IEnumerable<EnumFieldObject<RoomAmenity>> AsField(this ISet<RoomAmenityModel> amenities)
        {
            foreach (var item in amenities)
            {
                var field = item.Amenity.GetFieldSettings();

                if (item.Amenity == RoomAmenity.Custom)
                {
                    field.Name = item.CustomName ?? "Error";
                    yield return field;
                }
                else
                {
                    yield return item.Amenity.GetFieldSettings();
                }
            }
        }

        public static IEnumerable<EnumFieldObject<RoomService>> AsField(this ISet<ServiceModel> services)
        {
            foreach (var item in services)
            {
                var field = item.Service.GetFieldSettings();

                if (item.Service == RoomService.Custom)
                {
                    field.Name = $"{item.CustomName} ({item.Price.GetFieldSettings().Name})";
                    yield return field;
                }
                else
                {
                    field.Name = $"{item.Service.GetFieldSettings().Name} ({item.Price.GetFieldSettings().Name})";
                    yield return field;
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