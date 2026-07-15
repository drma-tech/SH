namespace SH.Shared.Models
{
    public class PropertyModel : ProtectedMainDocument
    {
        public PropertyModel() : base(DocumentType.Profile)
        {
        }

        public PropertyStructure Structure { get; set; } = PropertyStructure.House;

        public SharedWith SharedWith { get; set; } = SharedWith.Owner;

        public List<PropertyMember> Members { get; set; } = [];

        public PropertyAddress Address { get; set; } = new();

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? LocationDesc { get; set; }

        public string? CheckInInstructions { get; set; }

        public HashSet<Language> Languages { get; set; } = [];

        public int? SizeSqm { get; set; }

        public List<PropertyPhoto> Photos { get; set; } = [];
        public List<PropertyFacilityModel> Facilities { get; set; } = [];
        public List<PropertyAmenityModel> Amenities { get; set; } = [];
        public List<AccessibilityFeature> Accessibilities { get; set; } = [];

        public bool AddressVerified { get; set; }

        public bool SocialRequired { get; set; }
        public bool IdVerificationRequired { get; set; }
        public bool IncomeVerificationRequired { get; set; }
        public bool BackgroundCheckRequired { get; set; }

        public List<RoomShort> Rooms { get; set; } = [];
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

    public class PropertyPhoto
    {
        public string? UrlPath { get; set; }
        public string? Caption { get; set; }
        public int Order { get; set; }
    }

    public class PropertyMember
    {
        public string? UserId { get; set; }
        public bool IdVerified { get; set; }
        public bool EmailVerified { get; set; }
        public bool PhoneVerified { get; set; }
        public PropertyUserRole Role { get; set; }
    }

    public class PropertyFacilityModel
    {
        public PropertyFacility Facility { get; set; }
        public string? CustomName { get; set; }
    }

    public class PropertyAmenityModel
    {
        public PropertyAmenity Amenity { get; set; }
        public string? CustomName { get; set; }
    }

    public class RoomShort
    {
        public string RoomId { get; set; } = Guid.NewGuid().ToString();
        public string? Title { get; set; }
        public RoomStatus Status { get; set; }
        public decimal? MonthlyPrice { get; set; } //rent + utilities + others
        public double? Rating { get; set; }
    }
}