namespace SH.Shared.Models
{
    public enum UsageType
    {
        [Custom(Name = "Private")]
        Private = 1,

        [Custom(Name = "Shared")]
        Shared = 2
    }

    public enum RoomFacility
    {
        Bathroom = 1,
        Balcony = 2,
        Kitchen = 3,
        Parking = 4,
        Custom = 99
    }

    public enum RoomAmenity
    {
        Heating = 1,
        AirConditioning = 2,
        Closet = 3,
        Workspace = 4,
        LockableRoom = 5,
        WiFi = 6,
        WashingMachine = 7,
        Dryer = 8,
        FullyEquippedKitchen = 9,
        Furnished = 10,
        Tv = 11,
        Custom = 99
    }

    public enum RoomService
    {
        Maintenance = 1,
        Cleaning = 2,
        Breakfast = 3,
        Laundry = 4,
        Custom = 99
    }

    public enum RoomServicePrice
    {
        Included = 1,
        Extra = 2,
    }

    public enum Allowed
    {
        [Custom(Name = "Yes")]
        Yes = 1,

        [Custom(Name = "Negotiable")]
        Negotiable = 2,

        [Custom(Name = "No")]
        No = 3,
    }

    public enum Gender
    {
        Male = 2,
        Female = 3,
        NonBinary = 4
    }

    public enum Tenant
    {
        Students = 1,
        Professionals = 2,
        RemoteWorkers = 3
    }

    public enum Cost
    {
        Rent = 1,
        Deposit = 2,
        Utilities = 3,
        Custom = 99
    }

    public enum AvailabilityMode
    {
        Auto = 1,
        Manual = 2
    }

    public enum RequestStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        Cancelled = 4
    }

    public enum OccupationStatus
    {
        Scheduled = 1,
        Active = 2,
        Expired = 3
    }

    public class RoomModel : PrivateMainDocument
    {
        public RoomModel() : base(DocumentType.Room)
        {
        }

        [Custom(Name = "Title")]
        public string? Title { get; set; }

        [Custom(Name = "Description")]
        public string? Description { get; set; }

        [Custom(Name = "Size (sqm)")]
        public int SizeSqm { get; set; }

        [Custom(Name = "Room Usage")]
        public UsageType UsageType { get; set; } = UsageType.Private;

        public List<RoomPhoto> Photos { get; set; } = [];
        public List<RoomFacilityModel> Facilities { get; set; } = [];
        public List<RoomAmenityModel> Amenities { get; set; } = [];
        public List<ServiceModel> Services { get; set; } = [];

        //Preferences / Rules
        [Custom(Name = "Smoking")]
        public Allowed Smoking { get; set; }

        [Custom(Name = "Pets")]
        public Allowed Pets { get; set; }

        [Custom(Name = "Couples")]
        public Allowed Couples { get; set; }

        [Custom(Name = "Children")]
        public Allowed Children { get; set; }

        [Custom(Name = "Alcohol")]
        public Allowed Alcohol { get; set; }

        [Custom(Name = "Visits")]
        public Allowed Visits { get; set; }

        public HashSet<Gender> Genders { get; set; } = [];
        public HashSet<Tenant> Tenants { get; set; } = [];
        public int? MinimumAge { get; set; }
        public int? MaximumAge { get; set; }

        //Rental Terms / Costs

        public string Currency { get; set; } = "USD";
        public List<CostModel> Costs { get; set; } = [];
        public int MinimumStay { get; set; } = 1;
        public int? MaximumStay { get; set; }

        //Management

        public int? MaxOccupancy { get; set; } // null = private, 2 ou mais = shared
        public int? CurrentOccupancy { get; set; }

        public RoomStatus Status { get; set; }
        public AvailabilityMode AvailabilityMode { get; set; } //auto = calcula de acordo com Occupations, manual = definido pelo dono do apt
        public DateTime? AvailableFrom { get; set; } //todo: calcular em toda mudanca de ocupacao

        public HashSet<Request> Requests { get; set; } = [];
        public HashSet<Occupation> Occupations { get; set; } = [];
    }

    public class RoomPhoto
    {
        public string? UrlPath { get; set; }
        public string? Caption { get; set; }
        public int Order { get; set; }
    }

    public class RoomFacilityModel
    {
        public RoomFacility Facility { get; set; }
        public UsageType UsageType { get; set; }
        public string? CustomName { get; set; }
    }

    public class RoomAmenityModel
    {
        public RoomAmenity Amenity { get; set; }
        public string? CustomName { get; set; }
    }

    public class ServiceModel
    {
        public RoomService Service { get; set; }
        public RoomServicePrice Price { get; set; }
        public string? CustomName { get; set; }
    }

    public class CostModel
    {
        public Cost Cost { get; set; }
        public decimal Amount { get; set; }
        public string? CustomName { get; set; }
    }

    public class Request
    {
        public string? UserId { get; set; } //todo: user premium = Fast-Track
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
    }

    public class Occupation
    {
        public string? UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public OccupationStatus Status { get; set; } = OccupationStatus.Scheduled;
    }
}