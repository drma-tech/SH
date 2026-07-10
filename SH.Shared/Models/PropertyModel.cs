namespace SH.Shared.Models
{
    public enum PropertyStructure
    {
        [Custom(Name = "House")]
        House = 1,

        [Custom(Name = "Apartment")]
        Apartment = 2
    }

    public enum LivingArrangement
    {
        [Custom(Name = "Shared With Owner")]
        SharedWithOwner = 1,

        [Custom(Name = "Shared With Tenants")]
        SharedWithTenants = 2
    }

    public enum PropertyUserRole
    {
        [Custom(Name = "Owner")]
        Owner = 1,

        [Custom(Name = "Manager")]
        Manager = 2,

        [Custom(Name = "Real Estate")]
        RealEstate = 3
    }

    public enum PropertyFacility //shared by all tenants in property
    {
        [Custom(Name = "Pool")]
        Pool = 1,

        [Custom(Name = "Gym")]
        Gym = 2,

        [Custom(Name = "Parking")]
        Parking = 3,

        [Custom(Name = "Laundry Room")]
        LaundryRoom = 4,

        [Custom(Name = "Playground")]
        Playground = 5,

        [Custom(Name = "Coworking")]
        Coworking = 6,

        [Custom(Name = "Grill Area")]
        GrillArea = 7,

        [Custom(Name = "Elevator")]
        Elevator = 8,

        [Custom(Name = "Custom")]
        Custom = 99
    }

    public enum PropertyAmenity //shared by all tenants in property
    {
        [Custom(Name = "Doorman")]
        Doorman = 1,

        [Custom(Name = "Security")]
        Security = 2,

        [Custom(Name = "Custom")]
        Custom = 99
    }

    public enum RoomStatus
    {
        [Custom(Name = "Available")]
        Available = 1,

        [Custom(Name = "Occupied")]
        Occupied = 2,

        [Custom(Name = "Unavailable")]
        Unavailable = 3
    }

    public enum AccessibilityFeature
    {
        [Custom(Name = "Wheelchair Accessible")]
        WheelchairAccessible = 1,

        [Custom(Name = "Step Free Access")]
        StepFreeAccess = 2,

        [Custom(Name = "Elevator Accessible")]
        ElevatorAccessible = 3
    }

    public class PropertyModel : ProtectedMainDocument
    {
        public PropertyModel() : base(DocumentType.Profile)
        {
        }

        [Custom(Name = "Structure")]
        public PropertyStructure Structure { get; set; } = PropertyStructure.House;

        [Custom(Name = "Living Arrangement")]
        public LivingArrangement LivingArrangement { get; set; } = LivingArrangement.SharedWithOwner;

        public List<PropertyMember> Members { get; set; } = [];

        public PropertyAddress Address { get; set; } = new();

        [Custom(Name = "Title", Placeholder = "Property name, building name, or short listing title")]
        public string? Title { get; set; }

        [Custom(Name = "Description", Placeholder = "Overview of the property, rules, and what makes it unique")]
        public string? Description { get; set; }

        [Custom(Name = "Location Description", Placeholder = "What’s around the property and how easy it is to get around")]
        public string? LocationDesc { get; set; }

        [Custom(Name = "CheckIn Instructions", Placeholder = "How to access the property, keys or codes, and arrival steps")]
        public string? CheckInInstructions { get; set; }

        [Custom(Name = "Languages spoken")]
        public HashSet<Language> Languages { get; set; } = [];

        [Custom(Name = "Size (sqm)")]
        public int? SizeSqm { get; set; }

        public List<PropertyPhoto> Photos { get; set; } = [];
        public List<PropertyFacilityModel> Facilities { get; set; } = [];
        public List<PropertyAmenityModel> Amenities { get; set; } = [];
        public List<AccessibilityFeature> Accessibilities { get; set; } = [];

        [Custom(Name = "Address Verified")]
        public bool AddressVerified { get; set; }

        [Custom(Name = "Social Media Required", Description = "Requires the candidate to have registered at least one social media account.")]
        public bool SocialRequired { get; set; }

        [Custom(Name = "ID Verification Required", Description = "Requires the candidate to have completed identity verification.")]
        public bool IdVerificationRequired { get; set; }

        [Custom(Name = "Income Verification Required", Description = "Requires the candidate to have completed income verification.")]
        public bool IncomeVerificationRequired { get; set; }

        [Custom(Name = "Background Check Required", Description = "It requires the candidate to have undergone a background check.")]
        public bool BackgroundCheckRequired { get; set; }

        public List<RoomShort> Rooms { get; set; } = [];
    }

    public class PropertyAddress
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [Custom(Name = "Label", Description = "What will be shown to the potential tenant")]
        public string? Label { get; set; }

        [Custom(Name = "Country Code")]
        public string? CountryCode { get; set; }

        [Custom(Name = "Country Name")]
        public string? CountryName { get; set; }

        [Custom(Name = "State / County")]
        public string? State { get; set; }

        [Custom(Name = "City")]
        public string? City { get; set; }

        [Custom(Name = "District")]
        public string? District { get; set; }

        [Custom(Name = "Street")]
        public string? Street { get; set; }

        [Custom(Name = "House Number")]
        public string? HouseNumber { get; set; }

        [Custom(Name = "Postal Code")]
        public string? PostalCode { get; set; }

        [Custom(Name = "Unit")]
        public string? Unit { get; set; }

        [Custom(Name = "Hide Exact Address")]
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

        [Custom(Name = "Id Verified")]
        public bool IdVerified { get; set; }

        [Custom(Name = "Email Verified")]
        public bool EmailVerified { get; set; }

        [Custom(Name = "Phone Verified")]
        public bool PhoneVerified { get; set; }

        public PropertyUserRole Role { get; set; } //Owner / Manager / Real Estate
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