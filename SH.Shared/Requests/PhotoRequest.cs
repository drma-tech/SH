namespace SH.Shared.Requests;

public class PhotoRequest : RequestBase
{
    public required string PropertyId { get; set; }
    public string? RoomId { get; set; }
    public string? PhotoId { get; set; }
    public string? Caption { get; set; }
    public byte[] Buffer { get; set; } = [];
}