using Microsoft.AspNetCore.Components;

namespace SH.WEB.Modules.Shared.Field;

public partial class FieldSelect<TValue, TEnum> : FormBase
    where TEnum : struct, Enum, IConvertible
{
    [Parameter] public bool Multiple { get; set; }
    [Parameter] public bool ShowGroup { get; set; }
    [Parameter] public bool ShowHelper { get; set; } = true;
    [Parameter] public bool ShowDescription { get; set; } = true;
    [Parameter] public bool ShowDataSelectDescription { get; set; } = true;
    [Parameter] public string? HelpLink { get; set; }
    [Parameter] public string? CustomInfo { get; set; }
    [Parameter] public string? CustomWarning { get; set; }

    [Parameter] public EventCallback ButtomClicked { get; set; }
    [Parameter] public string? ButtomCssIcon { get; set; }
    [Parameter] public string? ButtomTitle { get; set; }
    [Parameter] public bool ButtomDisabled { get; set; }

    [Parameter] public TValue SelectedValue { get; set; } = default!;
    [Parameter] public EventCallback<TValue> SelectedValueChanged { get; set; }

    [Parameter] public HashSet<TEnum> SelectedValues { get; set; } = [];
    [Parameter] public EventCallback<HashSet<TEnum>> SelectedValuesChanged { get; set; }

    [Parameter] public Func<EnumFieldObject<TEnum>, object> Order { get; set; } = o => o.Value;
    [Parameter] public Func<EnumFieldObject<TEnum>, bool> Filter { get; set; } = o => true;

    public IReadOnlyList<EnumFieldObject<TEnum>> EnumList { get; set; } = [];
    public IReadOnlyList<EnumFieldObject<TEnum>> EnumListRaw { get; set; } = [];
    public IReadOnlyList<string> EnumListGroup { get; set; } = [];    

    protected override void OnInitialized()
    {
        EnumList = EnumHelper.GetList<TEnum>();

        EnumListRaw = [.. EnumList.Where(Filter).OrderBy(Order)];

        if (ShowGroup)
        {
            EnumListGroup = [.. EnumListRaw.Select(s => s.Group ?? "").Order().Distinct()];
        }
    }

    private static string GetMultiSelectionText(IReadOnlyList<string> selectedValues)
    {
        return string.Join(", ", selectedValues.Select(x => x.ParseToEnum<TEnum>().GetFieldSettings().Name));
    }
}
