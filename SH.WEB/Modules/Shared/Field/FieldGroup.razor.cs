using Microsoft.AspNetCore.Components;

namespace SH.WEB.Modules.Shared.Field;

public partial class FieldGroup<TValue, TEnum> : FormBase<TValue>
    where TEnum : struct, Enum, IConvertible
{
    [Parameter] public string? CssIcon { get; set; }

    [Parameter] public string? HelpLink { get; set; }
    [Parameter] public string? CustomInfo { get; set; }
    [Parameter] public string? CustomWarning { get; set; }
    [Parameter] public bool Visible { get; set; } = true;

    [Parameter] public TValue SelectedValue { get; set; } = default!;
    [Parameter] public EventCallback<TValue> SelectedValueChanged { get; set; }

    [Parameter] public Func<EnumObject<TEnum>, object> Order { get; set; } = o => o.Value;
    [Parameter] public Func<EnumObject<TEnum>, bool> Filter { get; set; } = o => true;

    public List<EnumObject<TEnum>> EnumList { get; set; } = [];

    protected override void OnInitialized()
    {
        EnumList = EnumHelper.GetList<TEnum>();
    }
}