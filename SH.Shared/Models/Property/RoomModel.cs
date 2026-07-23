using SH.Shared.Core;

namespace SH.Shared.Models.Property
{
    public enum StayDuration
    {
        [FieldSettings("Less Than 1 Month")]
        _LessThan1Month = 1,

        [FieldSettings("1-3 Months")]
        _1To3Months = 2,

        [FieldSettings("3-6 Months")]
        _3To6Months = 3,

        [FieldSettings("6-12 Months")]
        _6To12Months = 4,

        [FieldSettings("1 year+")]
        _MoreThan1Year = 5,
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

    public class RoomModel : CosmosDocument
    {
        public RoomModel()
        {
        }

        //Basic Data

        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? SizeSqm { get; set; }
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

        public Allowed Smoking { get; set; } = Allowed.Negotiable;

        public Allowed Pets { get; set; } = Allowed.Negotiable;

        public Allowed Couples { get; set; } = Allowed.Negotiable;

        public Allowed Children { get; set; } = Allowed.Negotiable;

        public Allowed Alcohol { get; set; } = Allowed.Negotiable;

        public Allowed Visits { get; set; } = Allowed.Negotiable;

        public HashSet<Gender> PreferredGenders { get; set; } = [];

        public HashSet<Occupation> PreferredOccupations { get; set; } = [];

        public int? MinimumAge { get; set; }

        public int? MaximumAge { get; set; }

        //Rental Terms / Costs

        public Currency Currency { get; set; } = Currency.USD;

        public int? PeriodStay { get; set; } = 1;

        public StayInterval IntervalStay { get; set; } = StayInterval.Month;

        public string MinimumStay => $"{PeriodStay} {IntervalStay.GetFieldSettings().Name}";

        public List<ChargeModel> Costs { get; set; } = [];

        //Management

        public int Capacity { get; set; } = 1;

        public int? CurrentOccupancy { get; set; }

        //[FieldSettings("Availability Status")]
        public RoomStatus Status { get; set; } = RoomStatus.Available;

        public AvailabilityMode AvailabilityMode { get; set; } = AvailabilityMode.Auto; //auto = calcula de acordo com Occupations, manual = definido pelo dono do apt

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