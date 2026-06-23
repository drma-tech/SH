namespace SH.Shared.Models
{
    public class Property : PrivateMainDocument
    {
        public Property() : base(DocumentType.Property)
        {
        }

        #region Address

        public string? HereId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string? Label { get; set; }

        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? Unit { get; set; }

        #endregion Address

        public HashSet<RoomShort> Rooms { get; set; } = [];
    }

    public class RoomShort
    {
        public string? RoomId { get; set; }
        public string? RoomName { get; set; }
    }
}