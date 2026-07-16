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
        Variable = 4
    }

    public class ProfileModel : ProtectedMainDocument
    {
        public ProfileModel() : base(DocumentType.Profile)
        {
        }

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
        public HashSet<Language> Languages { get; set; } = [];

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
    }
}