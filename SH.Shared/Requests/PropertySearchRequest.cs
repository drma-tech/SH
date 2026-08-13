namespace SH.Shared.Requests;

public class PropertySearchRequest : RequestBase
{
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
}