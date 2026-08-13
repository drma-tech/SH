using SH.Shared.Core.Types;

namespace SH.Shared.Models.Index
{
    public class CountryIndex(string? id) : MainDocument(new MainIdentity(MainType.CountryIndex, id))
    {
        public ISet<CountryIndexItem> Countries { get; set; } = new HashSet<CountryIndexItem>();

        protected override object?[] EqualityValues => [Id];
    }

    public class CountryIndexItem(string? CountryCode, string? CountryName) : EqualityBase<CountryIndexItem>
    {
        public string? CountryCode { get; set; } = CountryCode;
        public string? CountryName { get; set; } = CountryName;

        protected override object?[] EqualityValues => [CountryCode];
    }
}