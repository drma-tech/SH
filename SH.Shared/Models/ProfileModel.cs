namespace SH.Shared.Models
{
    public enum HomeSchedule
    {
        [Custom(Name = "Day")]
        Day = 1,

        [Custom(Name = "Night")]
        Night = 2,

        [Custom(Name = "Mostly Home")]
        MostlyHome = 3,

        [Custom(Name = "Variable")]
        Variable = 4
    }

    public class ProfileModel : ProtectedMainDocument
    {
        public ProfileModel() : base(DocumentType.Profile)
        {
        }

        //Basic Data

        [Custom(Name = "First Name")]
        public string? FirstName { get; set; }

        [Custom(Name = "Last Name")]
        public string? LastName { get; set; }

        [Custom(Name = "Nationality")]
        public Country? Nationality { get; set; }

        [Custom(Name = "Age")]
        public int? Age { get; set; }

        [Custom(Name = "Gender")]
        public Gender? Gender { get; set; }

        [Custom(Name = "Occupation")]
        public Occupation? Occupation { get; set; }

        [Custom(Name = "When are you usually at home?")]
        public HomeSchedule? HomeSchedule { get; set; }

        [Custom(Name = "Languages spoken")]
        public HashSet<Language> Languages { get; set; } = [];

        [Custom(Name = "About Me")]
        public string? AboutMe { get; set; }

        [Custom(Name = "Profile Picture")]
        public string? ProfilePicture { get; set; }

        //Household Profile

        [Custom(Name = "Smokes")]
        public bool Smokes { get; set; }

        [Custom(Name = "Drinks Alcohol")]
        public bool DrinksAlcohol { get; set; }

        [Custom(Name = "Lives with pets")]
        public bool LivesWithPets { get; set; }

        [Custom(Name = "Lives with children")]
        public bool LivesWithChildren { get; set; }

        [Custom(Name = "Lives As Couple")]
        public bool LivesAsCouple { get; set; }

        [Custom(Name = "Hosts Visitors")]
        public bool HostsVisitors { get; set; }

        //Social Media

        [Custom(Name = "Instagram")]
        public string? Instagram { get; set; }

        [Custom(Name = "LinkedIn")]
        public string? LinkedIn { get; set; }

        [Custom(Name = "Facebook")]
        public string? Facebook { get; set; }

        [Custom(Name = "Other Social Media")]
        public string? OtherSocialMedia { get; set; }

        //Verifications

        [Custom(Name = "Id Verified")]
        public bool IdVerified { get; set; }

        [Custom(Name = "Email Verified")]
        public bool EmailVerified { get; set; }

        [Custom(Name = "Phone Verified")]
        public bool PhoneVerified { get; set; }
    }
}