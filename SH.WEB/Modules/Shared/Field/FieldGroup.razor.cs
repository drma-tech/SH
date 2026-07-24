using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace SH.WEB.Modules.Shared.Field;

public partial class FieldGroup<TEnum> : FormBase
    where TEnum : struct, Enum, IConvertible
{
    [Parameter] public string? HelpLink { get; set; }
    [Parameter] public string? CustomInfo { get; set; }
    [Parameter] public string? CustomWarning { get; set; }

    [Parameter] public TEnum SelectedValue { get; set; } = default!;
    [Parameter] public EventCallback<TEnum> SelectedValueChanged { get; set; }

    [Parameter] public IEnumerable<TEnum>? SelectedValues { get; set; } = default!;
    [Parameter] public EventCallback<IEnumerable<TEnum>?> SelectedValuesChanged { get; set; }

    [Parameter] public Func<TEnum, object> Order { get; set; } = o => o;
    [Parameter] public Func<TEnum, bool> Filter { get; set; } = o => true;

    public List<TEnum> EnumList { get; set; } = [];

    [Parameter] public MudForm? MudForm { get; set; }

    protected override void OnInitialized()
    {
        EnumList = [.. EnumHelper.GetValues<TEnum>()];
    }

    private async Task SelectedValueChangedHandle(TEnum value)
    {
        await SelectedValueChanged.InvokeAsync(value);
        if (MudForm != null) await MudForm.FieldChanged.InvokeAsync();
    }

    private async Task SelectedValuesChangedHandle(IEnumerable<TEnum>? values)
    {
        await SelectedValuesChanged.InvokeAsync(values);
        if (MudForm != null) await MudForm.FieldChanged.InvokeAsync();
    }
}