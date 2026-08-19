using SH.Shared.Core.Types;

using Json = System.Text.Json.Serialization;
using Nsoft = Newtonsoft.Json;

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
        Cancelled = 4,
    }

    public enum OccupationStatus
    {
        Scheduled = 1,
        Active = 2,
        Expired = 3,
    }

    public class RoomModel(string? id) : PropertyDocument(new PropertyIdentity(PropertyType.Room, id))
    {
        //Basic Data

        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? SizeSqm { get; set; }
        public AccessType AccessType { get; set; } = AccessType.Private;

        public ISet<RoomFacilityModel> Facilities { get; set; } = new HashSet<RoomFacilityModel>()
        {
            new() { Facility = RoomFacility.Bathroom, AccessType = AccessType.Shared },
            new() { Facility = RoomFacility.Kitchen, AccessType = AccessType.Shared },
        };

        public ISet<RoomAmenityModel> Amenities { get; set; } = new HashSet<RoomAmenityModel>()
        {
            new() { Amenity = RoomAmenity.WiFi },
            new() { Amenity = RoomAmenity.Furnished },
            new() { Amenity = RoomAmenity.Closet },
            new() { Amenity = RoomAmenity.LockableRoom },
        };

        public ISet<ServiceModel> Services { get; set; } = new HashSet<ServiceModel>();

        //Preferences / Rules

        public Allowed Smoking { get; set; } = Allowed.Negotiable;
        public Allowed Pets { get; set; } = Allowed.Negotiable;
        public Allowed Couples { get; set; } = Allowed.Negotiable;
        public Allowed Children { get; set; } = Allowed.Negotiable;
        public Allowed Alcohol { get; set; } = Allowed.Negotiable;
        public Allowed Visits { get; set; } = Allowed.Negotiable;
        public ISet<Gender> PreferredGenders { get; set; } = new HashSet<Gender>();
        public ISet<Occupation> PreferredOccupations { get; set; } = new HashSet<Occupation>();
        public int? MinimumAge { get; set; }
        public int? MaximumAge { get; set; }

        //Rental Terms / Costs

        public Currency Currency { get; set; } = Currency.USD;
        public int? PeriodStay { get; set; } = 1;
        public StayInterval IntervalStay { get; set; } = StayInterval.Month;
        public string MinimumStay => $"{PeriodStay} {IntervalStay.GetFieldSettings().Name}";
        public ISet<ChargeModel> Costs { get; set; } = new HashSet<ChargeModel>();

        //Management

        public int Capacity { get; set; } = 1;
        public int? CurrentOccupancy { get; set; }
        public RoomStatus Status { get; set; } = RoomStatus.Available;
        public AvailabilityMode AvailabilityMode { get; set; } = AvailabilityMode.Auto; //auto = calcula de acordo com Occupations, manual = definido pelo dono do apt
        public DateTime AvailableFrom { get; set; } = DateTime.UtcNow; //todo: calcular em toda mudanca de ocupacao

        public ISet<RoomPhoto> Photos { get; set; } = new HashSet<RoomPhoto>();

        public void SanitizeOpenTextFields()
        {
            Title = Title?.RemoveUnsafeControlChars()?.NormalizeToNfc()?.Trim();
            Description = Description?.RemoveUnsafeControlChars()?.NormalizeToNfc()?.Trim();
        }

        public decimal GetMonthlyCosts()
        {
            decimal total = 0;

            foreach (var item in Costs)
            {
                if (item.Frequency == PaymentInterval.Weekly)
                    total += item.Amount * 4;
                else if (item.Frequency == PaymentInterval.EveryTwoWeeks)
                    total += item.Amount * 2;
                else if (item.Frequency == PaymentInterval.Monthly)
                    total += item.Amount;
            }

            return total;
        }

        protected override object?[] EqualityValues => [Id];
    }

    public class RoomPhoto : EqualityBase<RoomPhoto>
    {
        public string? PhotoId { get; set; }
        public string? Caption { get; set; }
        public int Order { get; set; }

        [Json.JsonIgnore]
        [Nsoft.JsonIgnore]
        public string? PhotoUrl => PhotoId.NotEmpty() ? $"{ImageHelper.BlobPath}/{ImageHelper.GetPhotoContainer(ImageHelper.PhotoType.Gallery)}/{PhotoId}.jpg" : null;

        protected override object?[] EqualityValues => [PhotoId];
    }

    public class RoomFacilityModel : EqualityBase<RoomFacilityModel>
    {
        public RoomFacility Facility { get; set; }
        public AccessType AccessType { get; set; }
        public string? CustomName { get; set; }

        protected override object?[] EqualityValues => [Facility, CustomName];
    }

    public class RoomAmenityModel : EqualityBase<RoomAmenityModel>
    {
        public RoomAmenity Amenity { get; set; }
        public string? CustomName { get; set; }

        protected override object?[] EqualityValues => [Amenity, CustomName];
    }

    public class ServiceModel : EqualityBase<ServiceModel>
    {
        public RoomService Service { get; set; }
        public RoomServicePrice Price { get; set; }
        public string? CustomName { get; set; }

        protected override object?[] EqualityValues => [Service, CustomName];
    }

    public class ChargeModel : EqualityBase<ChargeModel>
    {
        public PaymentType Type { get; set; }
        public PaymentInterval Frequency { get; set; }
        public decimal Amount { get; set; }
        public string? CustomName { get; set; }

        protected override object?[] EqualityValues => [Type, CustomName];
    }

    public class RoomProposal(string id) : MainDocument(new MainIdentity(MainType.Proposal, id))
    {
        public string? RoomId { get; set; } = id;
        public string? UserId { get; set; } //todo: user premium = Fast-Track
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public decimal RentPrice { get; set; }
        public ProposalStatus Status { get; set; } = ProposalStatus.Pending; //Pending / Approved / Rejected / Cancelled
        public string[] Events { get; set; } = [];

        protected override object?[] EqualityValues => [Id];
    }

    public class RoomOccupation(string id) : MainDocument(new MainIdentity(MainType.Occupation, id))
    {
        public string? RoomId { get; set; }
        public string? UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public OccupationStatus Status { get; set; } = OccupationStatus.Scheduled; //Scheduled / Active / Expired
        public string[] Events { get; set; } = [];

        protected override object?[] EqualityValues => [Id];
    }
}