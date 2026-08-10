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

    [Parameter] public EventCallback ButtomClicked { get; set; }
    [Parameter] public string? ButtomCssIcon { get; set; }
    [Parameter] public string? ButtomTitle { get; set; }
    [Parameter] public bool ButtomDisabled { get; set; }

    [Parameter] public TValue SelectedValue { get; set; } = default!;
    [Parameter] public EventCallback<TValue> SelectedValueChanged { get; set; }

    [Parameter] public IReadOnlyCollection<TEnum> SelectedValues { get; set; } = [];
    [Parameter] public EventCallback<IReadOnlyCollection<TEnum>> SelectedValuesChanged { get; set; }

    [Parameter] public Func<EnumFieldObject<TEnum>, object> Order { get; set; } = o => o.Value;
    [Parameter] public Func<EnumFieldObject<TEnum>, bool> Filter { get; set; } = o => true;

    public IEnumerable<EnumFieldObject<TEnum>> EnumList { get; set; } = [];
    public IEnumerable<EnumFieldObject<TEnum>> EnumListRaw { get; set; } = [];
    public IEnumerable<string> EnumListGroup { get; set; } = [];

    protected override void OnInitialized()
    {
        EnumList = EnumHelper.GetList<TEnum>();

        UpdateLists();
    }

    protected override void OnParametersSet()
    {
        UpdateLists();
    }

    private void UpdateLists()
    {
        EnumListRaw = [.. EnumList.Where(Filter).OrderBy(Order)];

        if (ShowGroup)
        {
            EnumListGroup = [.. EnumListRaw.Select(s => s.Group ?? "").Order(StringComparer.OrdinalIgnoreCase).Distinct(StringComparer.OrdinalIgnoreCase)];
        }
        else
        {
            EnumListGroup = [];
        }
    }

    private static string GetMultiSelectionText(IReadOnlyList<string> selectedValues)
    {
        return string.Join(", ", selectedValues.Select(x => x.ParseToEnum<TEnum>().GetFieldSettings().Name));
    }
}
