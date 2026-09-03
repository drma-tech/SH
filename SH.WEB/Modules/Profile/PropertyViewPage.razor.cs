using Microsoft.AspNetCore.Components;
using SH.Shared.Models.Property;

namespace SH.WEB.Modules.Profile
{
    public partial class PropertyViewPage
    {
        [Parameter] public required string propertyId { get; set; }

        private string? Title = "Title";
        private string? PhotoUrl;

        private RenderControlState<PropertyModel?> PropertyState { get; set; } = new RenderControlState<PropertyModel?>(null, obj => obj == null);
        private RenderControlState<List<string>> ReviewsState { get; set; } = new RenderControlState<List<string>>([], list => list.Empty());

        protected override IReadOnlyList<string?> GetParameterKey()
        {
            return [propertyId];
        }

        protected override async Task LoadParameterDataAsync()
        {
            var property = await PropertyPublicApi.View(propertyId, [PropertyState], Cts.Token);

            Title = property?.Title;
            PhotoUrl = property?.Photos.FirstOrDefault()?.PhotoUrl;

            await ReviewsState.FinishLoading.Invoke([]);
        }
    }
}