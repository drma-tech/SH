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

        [FieldSettings("First Name")]
        public string? FirstName { get; set; }

        [FieldSettings("Last Name")]
        public string? LastName { get; set; }

        [FieldSettings("Nationality")]
        public Country? Nationality { get; set; }

        [FieldSettings("Age")]
        public int? Age { get; set; }

        [FieldSettings("Gender")]
        public Gender? Gender { get; set; }

        [FieldSettings("Occupation")]
        public Occupation? Occupation { get; set; }

        [FieldSettings("When are you usually at home?")]
        public HomeSchedule? HomeSchedule { get; set; }

        [FieldSettings("Languages spoken")]
        public HashSet<Language> Languages { get; set; } = [];

        [FieldSettings("About Me")]
        public string? AboutMe { get; set; }

        [FieldSettings("Profile Picture")]
        public string? ProfilePicture { get; set; }

        //Household Profile

        [FieldSettings("Smokes")]
        public bool Smokes { get; set; }

        [FieldSettings("Drinks Alcohol")]
        public bool DrinksAlcohol { get; set; }

        [FieldSettings("Lives with pets")]
        public bool LivesWithPets { get; set; }

        [FieldSettings("Lives with children")]
        public bool LivesWithChildren { get; set; }

        [FieldSettings("Lives As Couple")]
        public bool LivesAsCouple { get; set; }

        [FieldSettings("Hosts Visitors")]
        public bool HostsVisitors { get; set; }

        //Social Media

        [FieldSettings("Instagram")]
        public string? Instagram { get; set; }

        [FieldSettings("LinkedIn")]
        public string? LinkedIn { get; set; }

        [FieldSettings("Facebook")]
        public string? Facebook { get; set; }

        [FieldSettings("Other Social Media")]
        public string? OtherSocialMedia { get; set; }

        //Verifications

        [FieldSettings("Id Verified")]
        public bool IdVerified { get; set; }

        [FieldSettings("Email Verified")]
        public bool EmailVerified { get; set; }

        [FieldSettings("Phone Verified")]
        public bool PhoneVerified { get; set; }
    }
}