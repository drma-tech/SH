using Microsoft.AspNetCore.Components;

namespace SH.WEB.Modules.Shared.Field;

public partial class FieldText : FormBase
{
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public int Rows { get; set; }

    [Parameter] public EventCallback ButtomClicked { get; set; }
    [Parameter] public string? ButtomCssIcon { get; set; }
    [Parameter] public string? ButtomTitle { get; set; }
    [Parameter] public bool ButtomDisabled { get; set; }

    private bool _processing;

    private async Task ButtomClickedHandle()
    {
        _processing = true;

        await ButtomClicked.InvokeAsync(null);

        await Task.Delay(10000);
        _processing = false;
    }
}
