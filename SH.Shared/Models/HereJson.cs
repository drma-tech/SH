using SH.Shared.Core;

namespace SH.Shared.Models;

public class Position
{
    public double? lat { get; set; }
    public double? lng { get; set; }
}

/// <summary>
///     note: language is the same as the searched location
/// </summary>
public class HereAddress
{
    public string? label { get; set; }
    public string? countryCode { get; set; }
    public string? countryName { get; set; }
    public string? stateCode { get; set; }
    public string? state { get; set; }
    public string? county { get; set; }
    public string? city { get; set; }
    public string? district { get; set; }
    public string? street { get; set; }
    public string? postalCode { get; set; }
    public string? houseNumber { get; set; }

    public string? GetCountry()
    {
        if (countryName.Empty()) throw new NotificationException("country not found");

        return countryName;
    }

    public string? GetState()
    {
        return state ?? county ?? countryName;
    }

    public string? GetCity()
    {
        if (city.Empty()) throw new NotificationException("city not found");

        return city;
    }
}

public class HereRoot
{
    public HereAddress? address { get; set; }
    public Position? position { get; set; }
}

public class HereJson
{
    public List<HereRoot> items { get; set; } = [];
}