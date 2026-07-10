namespace SH.Shared.Models
{
    public enum AccessType
    {
        [Custom(Name = "Private")]
        Private = 1,

        [Custom(Name = "Shared")]
        Shared = 2
    }

    public enum RoomFacility
    {
        [Custom(Name = "Bathroom")]
        Bathroom = 1,

        [Custom(Name = "Kitchen")]
        Kitchen = 2,

        [Custom(Name = "Parking")]
        Parking = 3,

        [Custom(Name = "Balcony")]
        Balcony = 4,

        [Custom(Name = "Storage")]
        Storage = 5,

        [Custom(Name = "Custom")]
        Custom = 99
    }

    public enum RoomAmenity
    {
        [Custom(Name = "WiFi")]
        WiFi = 1,

        [Custom(Name = "Furnished")]
        Furnished = 2,

        [Custom(Name = "Closet")]
        Closet = 3,

        [Custom(Name = "Lockable Room")]
        LockableRoom = 4,

        [Custom(Name = "Workspace")]
        Workspace = 5,

        [Custom(Name = "Heating")]
        Heating = 6,

        [Custom(Name = "Air Conditioning")]
        AirConditioning = 7,

        [Custom(Name = "Washing Machine")]
        WashingMachine = 8,

        [Custom(Name = "Dryer")]
        Dryer = 9,

        [Custom(Name = "Private Refrigerator")]
        PrivateRefrigerator = 10,

        [Custom(Name = "TV")]
        TV = 11,

        [Custom(Name = "Dishwasher")]
        Dishwasher = 12,

        [Custom(Name = "Custom")]
        Custom = 99
    }

    public enum RoomService
    {
        [Custom(Name = "Room Cleaning")]
        RoomCleaning = 1,

        [Custom(Name = "Laundry")]
        Laundry = 2,

        [Custom(Name = "Meals")]
        Meals = 3,

        [Custom(Name = "Breakfast")]
        Breakfast = 4,

        [Custom(Name = "Pickup / Transportation")]
        Transportation = 5,

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

        [Custom(Name = "No")]
        No = 2,

        [Custom(Name = "Negotiable")]
        Negotiable = 3
    }

    public enum Gender
    {
        [Custom(Name = "Male")]
        Male = 1,

        [Custom(Name = "Female")]
        Female = 2,

        [Custom(Name = "Non-Binary")]
        NonBinary = 3
    }

    public enum Occupation
    {
        [Custom(Name = "Student")]
        Student = 1,

        [Custom(Name = "Employed")]
        Employed = 2,

        [Custom(Name = "Self Employed")]
        SelfEmployed = 3,

        [Custom(Name = "Retired")]
        Retired = 4,

        [Custom(Name = "Unemployed")]
        Unemployed = 5,
    }

    public enum StayDuration
    {
        [Custom(Name = "Less Than 1 Month")]
        _LessThan1Month = 1,

        [Custom(Name = "1-3 Months")]
        _1To3Months = 2,

        [Custom(Name = "3-6 Months")]
        _3To6Months = 3,

        [Custom(Name = "6-12 Months")]
        _6To12Months = 4,

        [Custom(Name = "1 year+")]
        _MoreThan1Year = 5,
    }

    public enum StayInterval
    {
        [Custom(Name = "Week")]
        Week = 1,

        [Custom(Name = "Month")]
        Month = 3,
    }

    public enum PaymentInterval
    {
        [Custom(Name = "Once")]
        Once = 1,

        [Custom(Name = "Weekly")]
        Weekly = 2,

        [Custom(Name = "Every Two Weeks")]
        EveryTwoWeeks = 3,

        [Custom(Name = "Monthly")]
        Monthly = 4
    }

    public enum PaymentType
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

    public class RoomModel : ProtectedMainDocument
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
        public int? SizeSqm { get; set; }

        [Custom(Name = "Access Type")]
        public AccessType AccessType { get; set; } = AccessType.Private;

        public List<RoomPhoto> Photos { get; set; } = [];

        public List<RoomFacilityModel> Facilities { get; set; } =
        [
            new() { Facility = RoomFacility.Bathroom, AccessType = AccessType.Shared },
            new() { Facility = RoomFacility.Kitchen, AccessType = AccessType.Shared }
        ];

        public List<RoomAmenityModel> Amenities { get; set; } =
        [
            new() { Amenity = RoomAmenity.WiFi },
            new() { Amenity = RoomAmenity.Furnished },
            new() { Amenity = RoomAmenity.Closet },
            new() { Amenity = RoomAmenity.LockableRoom }
        ];

        public List<ServiceModel> Services { get; set; } = [];

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

        [Custom(Name = "Preferred Genders")]
        public HashSet<Gender> PreferredGenders { get; set; } = [];

        [Custom(Name = "Preferred Occupations")]
        public HashSet<Occupation> PreferredOccupations { get; set; } = [];

        [Custom(Name = "Minimum Age")]
        public int? MinimumAge { get; set; }

        [Custom(Name = "Maximum Age")]
        public int? MaximumAge { get; set; }

        //Rental Terms / Costs

        [Custom(Name = "Currency")]
        public Currency Currency { get; set; } = Currency.USD;

        [Custom(Name = "Period")]
        public int? PeriodStay { get; set; } = 1;

        [Custom(Name = "Interval")]
        public StayInterval IntervalStay { get; set; } = StayInterval.Month;

        [Custom(Name = "Minimum Stay")]
        public string MinimumStay => $"{PeriodStay} {IntervalStay.GetName()}";

        [Custom(Name = "Costs")]
        public List<ChargeModel> Charges { get; set; } = [];

        //Management

        [Custom(Name = "Capacity")]
        public int Capacity { get; set; } = 1;

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

    public class ChargeModel
    {
        public PaymentType Type { get; set; }
        public PaymentInterval Frequency { get; set; }
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