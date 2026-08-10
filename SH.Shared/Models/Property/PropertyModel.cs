using SH.Shared.Core;
using SH.Shared.Core.Types;

namespace SH.Shared.Models.Property
{
    public class PropertyModel(string? id) : PropertyDocument(new PropertyIdentity(PropertyType.Property, id))
    {
        public PropertyStructure Structure { get; set; } = PropertyStructure.House;

        public SharedWith SharedWith { get; set; } = SharedWith.Owner;

        public ISet<PropertyMember> Members { get; set; } = new HashSet<PropertyMember>();

        public PropertyAddress Address { get; set; } = new();

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? LocationDesc { get; set; }

        public string? CheckInInstructions { get; set; }

        public IReadOnlyCollection<Language> Languages { get; set; } = [];

        public int? SizeSqm { get; set; }

        public ISet<PropertyFacilityModel> Facilities { get; set; } = new HashSet<PropertyFacilityModel>();
        public ISet<PropertyAmenityModel> Amenities { get; set; } = new HashSet<PropertyAmenityModel>();
        public ISet<AccessibilityFeature> Accessibilities { get; set; } = new HashSet<AccessibilityFeature>();

        public bool AddressVerified { get; set; }

        public bool SocialRequired { get; set; }
        public bool IdVerificationRequired { get; set; }
        public bool IncomeVerificationRequired { get; set; }
        public bool BackgroundCheckRequired { get; set; }

        public ISet<PropertyPhoto> Photos { get; set; } = new HashSet<PropertyPhoto>();
        public ISet<RoomShort> Rooms { get; set; } = new HashSet<RoomShort>();

        public void SanitizeOpenTextFields()
        {
            Title = Title?.RemoveUnsafeControlChars()?.NormalizeToNfc()?.Trim();
            Description = Description?.RemoveUnsafeControlChars()?.NormalizeToNfc()?.Trim();
            LocationDesc = LocationDesc?.RemoveUnsafeControlChars()?.NormalizeToNfc()?.Trim();
            CheckInInstructions = CheckInInstructions?.RemoveUnsafeControlChars()?.NormalizeToNfc()?.Trim();
        }

        protected override object?[] EqualityValues => [Id];
    }

    public class PropertyAddress
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Label { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? Unit { get; set; }
        public bool HideExactAddress { get; set; }
    }

    public class PropertyPhoto : EqualityBase<PropertyPhoto>
    {
        public string? UrlPath { get; set; }
        public string? Caption { get; set; }
        public int Order { get; set; }

        protected override object?[] EqualityValues => [UrlPath];
    }

    public class PropertyMember : EqualityBase<PropertyMember>
    {
        public string? UserId { get; set; }
        public PropertyUserRole Role { get; set; }

        protected override object?[] EqualityValues => [UserId];
    }

    public class PropertyFacilityModel : EqualityBase<PropertyFacilityModel>
    {
        public PropertyFacility Facility { get; set; }
        public string? CustomName { get; set; }

        protected override object?[] EqualityValues => [Facility, CustomName];
    }

    public class PropertyAmenityModel : EqualityBase<PropertyAmenityModel>
    {
        public PropertyAmenity Amenity { get; set; }
        public string? CustomName { get; set; }

        protected override object?[] EqualityValues => [Amenity, CustomName];
    }

    public class RoomShort : EqualityBase<RoomShort>
    {
        public string? RoomId { get; set; }
        public string? Title { get; set; }
        public string? UrlPath { get; set; }
        public RoomStatus Status { get; set; } = RoomStatus.Available;
        public decimal? MonthlyPrice { get; set; } //rent + utilities + others
        public double? Rating { get; set; }

        protected override object?[] EqualityValues => [RoomId];
    }
}