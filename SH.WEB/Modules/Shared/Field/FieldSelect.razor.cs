using Microsoft.AspNetCore.Components;

namespace SH.WEB.Modules.Shared.Field;

public partial class FieldSelect<TValue, TEnum> : FormBase<TValue>
    where TEnum : struct, Enum, IConvertible
{
    [Parameter] public string? Style { get; set; }
    [Parameter] public string? CssIcon { get; set; }
    [Parameter] public bool Required { get; set; }
    [Parameter] public bool Multiple { get; set; }
    [Parameter] public bool ShowGroup { get; set; }
    [Parameter] public bool ShowHelper { get; set; } = true;
    [Parameter] public bool ShowDescription { get; set; } = true;
    [Parameter] public bool ShowDataSelectDescription { get; set; } = true;
    [Parameter] public string? HelpLink { get; set; }
    [Parameter] public string? CustomInfo { get; set; }
    [Parameter] public string? CustomWarning { get; set; }
    [Parameter] public bool Visible { get; set; } = true;

    [Parameter] public EventCallback ButtomClicked { get; set; }
    [Parameter] public string? ButtomCssIcon { get; set; }
    [Parameter] public string? ButtomTitle { get; set; }
    [Parameter] public bool ButtomDisabled { get; set; }

    [Parameter] public TValue SelectedValue { get; set; } = default!;
    [Parameter] public EventCallback<TValue> SelectedValueChanged { get; set; }

    [Parameter] public HashSet<TEnum> SelectedValues { get; set; } = [];
    [Parameter] public EventCallback<HashSet<TEnum>> SelectedValuesChanged { get; set; }

    private string? Description => For?.GetCustomAttribute()?.Description;

    [Parameter] public Func<EnumObject<TEnum>, object> Order { get; set; } = o => o.Value;
    [Parameter] public Func<EnumObject<TEnum>, bool> Filter { get; set; } = o => true;

    public List<EnumObject<TEnum>> EnumList { get; set; } = [];

    protected override void OnInitialized()
    {
        EnumList = EnumHelper.GetList<TEnum>();
    }

    private static string GetMultiSelectionText(IReadOnlyList<string> selectedValues)
    {
        return string.Join(", ", selectedValues.Select(x => x.ParseToEnum<TEnum>().GetName()));
    }
}