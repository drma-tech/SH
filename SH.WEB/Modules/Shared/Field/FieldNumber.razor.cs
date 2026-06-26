using Microsoft.AspNetCore.Components;

namespace SH.WEB.Modules.Shared.Field;

public partial class FieldNumber<T> : FormBase<T>
{
    [Parameter] public T? Value { get; set; }
    [Parameter] public EventCallback<T> ValueChanged { get; set; }
    [Parameter] public int Rows { get; set; }

    [Parameter] public string? CssIcon { get; set; }
    [Parameter] public bool Required { get; set; }
    [Parameter] public bool Visible { get; set; } = true;

    [Parameter] public EventCallback ButtomClicked { get; set; }
    [Parameter] public string? ButtomCssIcon { get; set; }
    [Parameter] public string? ButtomTitle { get; set; }

    private string? Description => For.GetCustomAttribute()?.Description;
}