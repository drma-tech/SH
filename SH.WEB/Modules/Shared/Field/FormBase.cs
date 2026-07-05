using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace SH.WEB.Modules.Shared.Field;

public class FormBase<TValue> : ComponentBase
{
    [Parameter] public Expression<Func<TValue>>? For { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool ReadOnly { get; set; }
    [Parameter] public bool ShowLabel { get; set; } = true;
}