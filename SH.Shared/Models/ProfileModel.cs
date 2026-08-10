using SH.Shared.Core.Types;
using SH.Shared.Models.Auth;

namespace SH.Shared.Models
{
    public enum HomeSchedule
    {
        [FieldSettings("Day")]
        Day = 1,

        [FieldSettings("Night")]
        Night = 2,

        [FieldSettings("Mostly Home")]
        MostlyHome = 3,

        [FieldSettings("Variable")]
        Variable = 4,
    }

    public class ProfileModel(string? id) : MainDocument(new MainIdentity(MainType.Profile, id))
    {
        //Basic Data

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Country? Nationality { get; set; }
        public int? Age { get; set; }
        public Gender? Gender { get; set; }
        public Occupation? Occupation { get; set; }

        //[FieldSettings("When are you usually at home?")]
        public HomeSchedule? HomeSchedule { get; set; }

        //[FieldSettings("Languages spoken")]
        public IEnumerable<Language> Languages { get; set; } = [];

        public string? AboutMe { get; set; }
        public string? ProfilePicture { get; set; }

        //Household Profile

        public bool Smokes { get; set; }
        public bool DrinksAlcohol { get; set; }
        public bool LivesWithPets { get; set; }
        public bool LivesWithChildren { get; set; }
        public bool LivesAsCouple { get; set; }
        public bool HostsVisitors { get; set; }

        //Social Media

        public string? Instagram { get; set; }
        public string? LinkedIn { get; set; }
        public string? Facebook { get; set; }
        public string? OtherSocialMedia { get; set; }

        //Verifications

        public bool IdVerified { get; set; }
        public bool EmailVerified { get; set; }
        public bool PhoneVerified { get; set; }

        public ISet<ProfileProperty> Properties { get; set; } = new HashSet<ProfileProperty>();
        public ISet<ProfileGuest> Guests { get; set; } = new HashSet<ProfileGuest>();

        protected override object?[] EqualityValues => [Id];
    }

    public class ProfileProperty : EqualityBase<AuthSubscription>
    {
        public string? PropertyId { get; set; }
        public string? Title { get; set; }
        public string? UrlPath { get; set; }
        public bool IsPublic { get; set; }
        public PropertyUserRole Role { get; set; }

        protected override object?[] EqualityValues => [PropertyId];
    }

    public class ProfileGuest : EqualityBase<AuthSubscription>
    {
        public string? PropertyId { get; set; }

        protected override object?[] EqualityValues => [PropertyId];
    }
}