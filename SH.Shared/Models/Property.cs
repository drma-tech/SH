namespace SH.Shared.Models
{
    public enum PropertyStructure
    {
        [Custom(Name = "House")]
        House = 1,

        [Custom(Name = "Apartment")]
        Apartment = 2
    }

    public enum OccupancyModel
    {
        [Custom(Name = "Shared With Owner")]
        SharedWithOwner = 1,

        [Custom(Name = "Shared With Tenants")]
        SharedWithTenants = 2
    }

    public enum PropertyFacility
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

    public enum PropertyAmenity
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
        [Custom(Name = "House")]
        Available = 1,

        [Custom(Name = "House")]
        PartiallyOccupied = 2,

        [Custom(Name = "House")]
        Occupied = 3,

        [Custom(Name = "House")]
        Unavailable = 4
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

    public class PropertyModel : PrivateMainDocument
    {
        public PropertyModel() : base(DocumentType.Property)
        {
        }

        public string? CountrySlug { get; set; }
        public string? StateSlug { get; set; }
        public string? CitySlug { get; set; }

        [Custom(Name = "Structure")]
        public PropertyStructure Structure { get; set; } = PropertyStructure.House;

        [Custom(Name = "Model")]
        public OccupancyModel Model { get; set; } = OccupancyModel.SharedWithOwner;

        public PropertyAddress Address { get; set; } = new();

        [Custom(Name = "Title")]
        public string? Title { get; set; }

        [Custom(Name = "Property Description", Placeholder = "Describe any additional observations regarding the property, rooms, rules, etc.")]
        public string? PropertyDesc { get; set; }

        [Custom(Name = "Location Description", Placeholder = "Describe the property's location, nearby tourist attractions, transportation options, etc.")]
        public string? LocationDesc { get; set; }

        [Custom(Name = "Size (sqm)")]
        public int SizeSqm { get; set; }

        public List<PropertyPhoto> Photos { get; set; } = [];
        public List<PropertyFacilityModel> Facilities { get; set; } = [];
        public List<PropertyAmenityModel> Amenities { get; set; } = [];
        public List<AccessibilityFeature> Accessibilities { get; set; } = [];

        [Custom(Name = "Owner Id Verified")]
        public bool OwnerIdVerified { get; set; }

        [Custom(Name = "Owner Email Verified")]
        public bool OwnerEmailVerified { get; set; }

        [Custom(Name = "Owner Phone Verified")]
        public bool OwnerPhoneVerified { get; set; }

        [Custom(Name = "Address Verified")]
        public bool AddressVerified { get; set; }

        [Custom(Name = "Income Verification Required")]
        public bool IncomeVerificationRequired { get; set; }

        [Custom(Name = "Background Check Required")]
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