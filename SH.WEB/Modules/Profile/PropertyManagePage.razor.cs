using FluentValidation;
using Microsoft.AspNetCore.Components;
using SH.Shared.Models.Property;
using SH.Shared.Models.Property.Core;
using SH.Shared.Translations.Model;
using SH.WEB.Api.Module.Cosmos.Authenticated;

namespace SH.WEB.Modules.Profile
{
    public partial class PropertyManagePage
    {
        [Parameter, EditorRequired] public string propertyId { get; set; }

        private RenderControlState<PropertyModel?> PropertyState { get; set; } = new RenderControlState<PropertyModel?>(initialValue: null, obj => obj == null);
        private RenderControlState<ProfileModel?> ActionsProfile { get; set; } = new RenderControlState<ProfileModel?>(initialValue: null, obj => obj == null);
        private RenderControlState<IEnumerable<RoomProposal>> ActionsProposal { get; set; } = new RenderControlState<IEnumerable<RoomProposal>>(initialValue: [], list => list.Empty());

        private ProfileProperty? property => ActionsProfile.Instance?.Properties?.Single(p => string.Equals(p.PropertyId, propertyId, StringComparison.OrdinalIgnoreCase));
        private RoomShort? selectedRoom { get; set; }

        protected override async Task LoadStaticDataAsync()
        {
            await PropertyApi.Get(propertyId, [PropertyState], Cts.Token);
            await ProfileApi.Get([ActionsProfile], setNewVersion: true, Cts.Token);

            var room = PropertyState.Instance?.Rooms.FirstOrDefault();

            await OnRoomChanged(room);
        }

        private async Task IsPublicChanged(bool value)
        {
            try
            {
                if (value)
                {
                    var model = await PropertyApi.Get(propertyId, [], Cts.Token) ?? throw new NotificationException("property not found");

                    var ProfileValidator = new PropertyValidation();

                    var validation = await ProfileValidator.ValidateAsync(model, options => options.IncludeAllRuleSets(), Cts.Token);

                    if (!validation.IsValid)
                    {
                        throw new NotificationException(validation.Errors.FirstOrDefault()?.ErrorMessage ?? "Invalid property data.");
                    }

                    await ProfileApi.PropertyPublic(propertyId, [ActionsProfile], Cts.Token);
                    await ShowSuccess("Your property is now public and can be made available to our clients.");
                }
                else
                {
                    await ProfileApi.PropertyPrivate(propertyId, [ActionsProfile], Cts.Token);
                    await ShowInfo("Your property is now in private mode and will not be seen or shared with anyone.");
                }
            }
            catch (Exception ex)
            {
                await ProcessException(ex);
            }
        }

        private async Task VerifyAddress()
        {
            await ShowWarning(Translations.Notification.FeatureUnderDevelopment);
        }

        private async Task OnRoomChanged(RoomShort? room)
        {
            try
            {
                selectedRoom = room;

                //if (room != null)
                //{
                //    var list = await RoomProposalApi.GetListAsync(room.RoomId, [], Cts.Token);
                //}
            }
            catch (Exception ex)
            {
                await ProcessException(ex);
            }
        }
    }
}