using Microsoft.AspNetCore.Components;

namespace SH.WEB.Modules.Shared.Field;

public partial class FieldSwitch : FormBase<bool>
{
    [Parameter] public bool Value { get; set; }
    [Parameter] public EventCallback<bool> ValueChanged { get; set; }

    [Parameter] public string? CssIcon { get; set; }
    [Parameter] public bool Visible { get; set; } = true;

    private string? Description => For.GetCustomAttribute()?.Description;
}