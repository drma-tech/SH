namespace SH.Shared.Models
{
    public enum AccessType
    {
        [Custom(Name = "Private")]
        Private = 1,

        [Custom(Name = "Shared")]
        Shared = 2
    }

    public enum RoomFacility //exclusive or shared between property residents (Private / Shared)
    {
        [Custom(Name = "Bathroom")]
        Bathroom = 1,

        [Custom(Name = "Balcony")]
        Balcony = 2,

        [Custom(Name = "Kitchen")]
        Kitchen = 3,

        [Custom(Name = "Parking")]
        Parking = 4,

        [Custom(Name = "Custom")]
        Custom = 99
    }

    public enum RoomAmenity
    {
        [Custom(Name = "Heating")]
        Heating = 1,

        [Custom(Name = "Air Conditioning")]
        AirConditioning = 2,

        [Custom(Name = "Closet")]
        Closet = 3,

        [Custom(Name = "Workspace")]
        Workspace = 4,

        [Custom(Name = "Lockable Room")]
        LockableRoom = 5,

        [Custom(Name = "WiFi")]
        WiFi = 6,

        [Custom(Name = "Washing Machine")]
        WashingMachine = 7,

        [Custom(Name = "Dryer")]
        Dryer = 8,

        [Custom(Name = "Furnished")]
        Furnished = 9,

        [Custom(Name = "Tv")]
        Tv = 10,

        [Custom(Name = "Custom")]
        Custom = 99
    }

    public enum RoomService
    {
        [Custom(Name = "Maintenance")]
        Maintenance = 1,

        [Custom(Name = "Cleaning")]
        Cleaning = 2,

        [Custom(Name = "Breakfast")]
        Breakfast = 3,

        [Custom(Name = "Laundry")]
        Laundry = 4,

        [Custom(Name = "Custom")]
        Custom = 99
    }

    public enum RoomServicePrice
    {
        [Custom(Name = "Included")]
        Included = 1,

        [Custom(Name = "Extra")]
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
        [Custom(Name = "Male")]
        Male = 2,

        [Custom(Name = "Female")]
        Female = 3,

        [Custom(Name = "Non-Binary")]
        NonBinary = 4
    }

    public enum Tenant
    {
        [Custom(Name = "Students")]
        Students = 1,

        [Custom(Name = "Professionals")]
        Professionals = 2,

        [Custom(Name = "Remote Workers")]
        RemoteWorkers = 3
    }

    public enum Cost
    {
        [Custom(Name = "Rent")]
        Rent = 1,

        [Custom(Name = "Deposit")]
        Deposit = 2,

        [Custom(Name = "Utilities")]
        Utilities = 3,

        [Custom(Name = "Custom")]
        Custom = 99
    }

    public enum AvailabilityMode
    {
        [Custom(Name = "Auto")]
        Auto = 1,

        [Custom(Name = "Manual")]
        Manual = 2
    }

    public enum ProposalStatus
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

        //Basic Data

        [Custom(Name = "Title", Placeholder = "Room name or type (e.g. Master bedroom, Single room, Ensuite room)")]
        public string? Title { get; set; }

        [Custom(Name = "Description", Placeholder = "Details about the room, furniture, and living conditions")]
        public string? Description { get; set; }

        [Custom(Name = "Size (sqm)")]
        public int SizeSqm { get; set; }

        [Custom(Name = "Access Type")]
        public AccessType AccessType { get; set; } = AccessType.Private;

        public List<RoomPhoto> Photos { get; set; } = [];
        public List<RoomFacilityModel> Facilities { get; set; } = []; //Private / Shared
        public List<RoomAmenityModel> Amenities { get; set; } = [];
        public List<ServiceModel> Services { get; set; } = []; //Included / Extra

        //Preferences / Rules

        [Custom(Name = "Smoking")]
        public Allowed Smoking { get; set; } = Allowed.Negotiable;

        [Custom(Name = "Pets")]
        public Allowed Pets { get; set; } = Allowed.Negotiable;

        [Custom(Name = "Couples")]
        public Allowed Couples { get; set; } = Allowed.Negotiable;

        [Custom(Name = "Children")]
        public Allowed Children { get; set; } = Allowed.Negotiable;

        [Custom(Name = "Alcohol")]
        public Allowed Alcohol { get; set; } = Allowed.Negotiable;

        [Custom(Name = "Visits")]
        public Allowed Visits { get; set; } = Allowed.Negotiable;

        [Custom(Name = "Allowed Genders")]
        public HashSet<Gender> AllowedGenders { get; set; } = [];

        [Custom(Name = "Allowed Tenants")]
        public HashSet<Tenant> AllowedTenants { get; set; } = [];

        [Custom(Name = "Minimum Age")]
        public int? MinimumAge { get; set; }

        [Custom(Name = "Maximum Age")]
        public int? MaximumAge { get; set; }

        //Rental Terms / Costs

        [Custom(Name = "Currency")]
        public string Currency { get; set; } = "USD";

        [Custom(Name = "Costs")]
        public List<CostModel> Costs { get; set; } = [];

        [Custom(Name = "Minimum Stay (Months)")]
        public int MinimumStay { get; set; } = 1;

        [Custom(Name = "Maximum Stay (Months)")]
        public int? MaximumStay { get; set; }

        //Management

        [Custom(Name = "Max Occupancy")]
        public int? MaxOccupancy { get; set; } // null = private, 2 ou mais = shared

        [Custom(Name = "Current Occupancy")]
        public int? CurrentOccupancy { get; set; }

        [Custom(Name = "Status")]
        public RoomStatus Status { get; set; } = RoomStatus.Available;

        [Custom(Name = "Availability Mode")]
        public AvailabilityMode AvailabilityMode { get; set; } = AvailabilityMode.Auto; //auto = calcula de acordo com Occupations, manual = definido pelo dono do apt

        [Custom(Name = "Available From")]
        public DateTime AvailableFrom { get; set; } = DateTime.UtcNow; //todo: calcular em toda mudanca de ocupacao
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
        public AccessType AccessType { get; set; }
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

    public class RoomProposal : PrivateMainDocument
    {
        public RoomProposal() : base(DocumentType.Proposal)
        {
        }

        public string? RoomId { get; set; }
        public string? UserId { get; set; } //todo: user premium = Fast-Track
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public decimal RentPrice { get; set; }
        public ProposalStatus Status { get; set; } = ProposalStatus.Pending; //Pending / Approved / Rejected / Cancelled
        public string[] Events { get; set; } = [];
    }

    public class RoomOccupation : PrivateMainDocument
    {
        public RoomOccupation() : base(DocumentType.Occupation)
        {
        }

        public string? RoomId { get; set; }
        public string? UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public OccupationStatus Status { get; set; } = OccupationStatus.Scheduled; //Scheduled / Active / Expired
        public string[] Events { get; set; } = [];
    }
}