namespace SH.Shared.Models
{
    public class Room : PrivateMainDocument
    {
        public Room() : base(DocumentType.Room)
        {
        }

        public string? Name { get; set; }
    }
}