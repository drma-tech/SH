using SH.Shared.Core.Types;

namespace SH.Shared.Models.Index
{
    public class StateIndex(string? id) : MainDocument(new MainIdentity(MainType.StateIndex, id))
    {
        public ISet<StateIndexItem> States { get; set; } = new HashSet<StateIndexItem>();

        protected override object?[] EqualityValues => [Id];
    }

    public class StateIndexItem(string? StateCode, string? StateName) : EqualityBase<StateIndexItem>
    {
        public string? StateCode { get; set; } = StateCode;
        public string? StateName { get; set; } = StateName;

        public ISet<CityIndexItem> Cities { get; set; } = new HashSet<CityIndexItem>();

        protected override object?[] EqualityValues => [StateCode];
    }

    public class CityIndexItem(string? CityCode, string? CityName) : EqualityBase<CityIndexItem>
    {
        public string? CityCode { get; set; } = CityCode;
        public string? CityName { get; set; } = CityName;

        protected override object?[] EqualityValues => [CityCode];
    }
}