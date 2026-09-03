using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace SH.WEB.Modules.Shared.Field;

public partial class FieldActive : FormBase
{
    [Parameter] public bool Value { get; set; }
    [Parameter] public EventCallback<bool> ValueChanged { get; set; }
    [Parameter] public string? WhyImportant { get; set; }

    [Parameter] public string? CustomButtonText { get; set; }

    [Parameter] public EventCallback<MouseEventArgs> Click { get; set; }
    [Parameter] public int? Sparks { get; set; }
}