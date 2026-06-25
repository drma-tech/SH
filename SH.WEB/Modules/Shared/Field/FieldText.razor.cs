using Microsoft.AspNetCore.Components;

namespace SH.WEB.Modules.Shared.Field;

public partial class FieldText : FormBase<string>
{
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public int Rows { get; set; }

    [Parameter] public string? CssIcon { get; set; }
    [Parameter] public bool Required { get; set; }
    [Parameter] public bool Visible { get; set; } = true;

    [Parameter] public EventCallback ButtomClicked { get; set; }
    [Parameter] public string? ButtomCssIcon { get; set; }
    [Parameter] public string? ButtomTitle { get; set; }

    private string? Description => For.GetCustomAttribute()?.Description;
}