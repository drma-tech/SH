using Microsoft.JSInterop;

namespace SH.WEB.Core.Javascript
{
    public class SliderJs(IJSRuntime js) : JsModuleBase(js, "./js/slider.js")
    {
        public Task InitLists(string id, CancellationToken cancellationToken, int? size = null, bool refresh = false) => InvokeVoid("slider.initLists", cancellationToken, id, size, refresh);
    }
}