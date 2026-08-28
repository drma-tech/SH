using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using MudBlazor.Utilities;
using SH.Shared.Models.Property;
using SH.Shared.Models.Property.Core;
using SH.Shared.Requests;

namespace SH.WEB.Modules.Profile
{
    public partial class RoomPage
    {
        [Parameter] public required string propertyId { get; set; }
        [Parameter] public required string roomId { get; set; }

        private RenderControlState<RoomModel?> RoomState { get; set; } = new RenderControlState<RoomModel?>(null, obj => obj == null);
        private RoomModel? model;

        private MudForm? _form;
        public bool IsDirty { get; set; }

        public RoomFacility? RoomFacility { get; set; }
        public AccessType RoomFacilityAccess { get; set; } = AccessType.Private;
        public string? RoomFacilityCustomName { get; set; }

        public RoomAmenity? RoomAmenity { get; set; }
        public string? RoomAmenityCustomName { get; set; }
        public RoomService? RoomService { get; set; }
        public RoomServicePrice RoomServicePrice { get; set; } = RoomServicePrice.Included;

        public string? RoomServiceCustomName { get; set; }
        public PaymentType PaymentType { get; set; } = PaymentType.Rent;
        public PaymentInterval Frequency { get; set; } = PaymentInterval.Monthly;
        public decimal? ChargeAmount { get; set; }
        public string? ChargeCustomName { get; set; }

        public ICollection<PhotoRequest> PhotoRequests { get; set; } = [];

        public RenderControlState<ISet<RoomFacilityModel>> ActionsFacilities { get; set; } = new RenderControlState<ISet<RoomFacilityModel>>(new HashSet<RoomFacilityModel>(), list => list.Empty());
        public RenderControlState<ISet<RoomAmenityModel>> ActionsAmenities { get; set; } = new RenderControlState<ISet<RoomAmenityModel>>(new HashSet<RoomAmenityModel>(), list => list.Empty());
        public RenderControlState<ISet<ServiceModel>> ActionsServices { get; set; } = new RenderControlState<ISet<ServiceModel>>(new HashSet<ServiceModel>(), list => list.Empty());
        public RenderControlState<ISet<ChargeModel>> ActionsCharges { get; set; } = new RenderControlState<ISet<ChargeModel>>(new HashSet<ChargeModel>(), list => list.Empty());

        private void OnFieldChanged(FormFieldChangedEventArgs args)
        {
            IsDirty = true;
        }

        protected override IReadOnlyList<string?> GetParameterKey()
        {
            return [propertyId, roomId];
        }

        protected override async Task LoadParameterDataAsync()
        {
            if (Navigation.Uri.Contains("/add/", StringComparison.OrdinalIgnoreCase))
            {
                model ??= new(roomId);
                await RoomState.FinishLoading.Invoke(model);
            }
            else
            {
                model = await RoomApi.Get(propertyId, roomId, [RoomState], Cts.Token) ?? throw new NotificationException("room not found");
            }

            await ActionsFacilities.FinishLoading.Invoke(model.Facilities);
            await ActionsAmenities.FinishLoading.Invoke(model.Amenities);
            await ActionsServices.FinishLoading.Invoke(model.Services);
            await ActionsCharges.FinishLoading.Invoke(model.Costs);
        }

        private async Task AddFacility()
        {
            if (RoomFacility.HasValue)
            {
                model!.Facilities.Add(new RoomFacilityModel { Facility = RoomFacility.Value, AccessType = RoomFacilityAccess, CustomName = RoomFacilityCustomName });
                RoomFacility = null;
                RoomFacilityAccess = AccessType.Private;
                RoomFacilityCustomName = null;
                await _form!.FieldChanged.InvokeAsync();
            }
        }

        private async Task RemoveFacility(MudChip<RoomFacilityModel> chip)
        {
            model!.Facilities.Remove(chip.Value!);
            await _form!.FieldChanged.InvokeAsync();
        }

        private async Task AddAmenity()
        {
            if (RoomAmenity.HasValue)
            {
                model!.Amenities.Add(new RoomAmenityModel { Amenity = RoomAmenity.Value, CustomName = RoomAmenityCustomName });
                RoomAmenity = null;
                RoomAmenityCustomName = null;
                await _form!.FieldChanged.InvokeAsync();
            }
        }

        private async Task RemoveAmenity(MudChip<RoomAmenityModel> chip)
        {
            model!.Amenities.Remove(chip.Value!);
            await _form!.FieldChanged.InvokeAsync();
        }

        private async Task AddService()
        {
            if (RoomService.HasValue)
            {
                model!.Services.Add(new ServiceModel { Service = RoomService.Value, Price = RoomServicePrice, CustomName = RoomServiceCustomName });
                RoomService = null;
                RoomServicePrice = RoomServicePrice.Included;
                RoomServiceCustomName = null;
                await _form!.FieldChanged.InvokeAsync();
            }
        }

        private async Task RemoveService(MudChip<ServiceModel> chip)
        {
            model!.Services.Remove(chip.Value!);
            await _form!.FieldChanged.InvokeAsync();
        }

        private async Task AddCharge()
        {
            if (ChargeAmount.HasValue)
            {
                model!.Costs.Add(new ChargeModel { Type = PaymentType, Frequency = Frequency, Amount = ChargeAmount.Value, CustomName = ChargeCustomName });
                PaymentType = PaymentType.Rent;
                Frequency = PaymentInterval.Monthly;
                ChargeAmount = null;
                ChargeCustomName = null;
                await _form!.FieldChanged.InvokeAsync();
            }
        }

        private async Task RemoveCharge(MudChip<ChargeModel> chip)
        {
            model!.Costs.Remove(chip.Value!);
            await _form!.FieldChanged.InvokeAsync();
        }

        private async Task SelectPicture()
        {
            await DialogService.SelectPicturePopup(new EventCallbackFactory().Create(this, async ((byte[], string?) result) => await PictureChanged(result.Item1, result.Item2)));
        }

        private async Task PictureChanged(byte[] buffer, string? caption)
        {
            try
            {
                var request = new PhotoRequest { PropertyId = propertyId, RoomId = roomId, Buffer = buffer, Caption = caption };

                if (Navigation.Uri.Contains("/add/", StringComparison.OrdinalIgnoreCase))
                {
                    PhotoRequests.Add(request);
                }
                else
                {
                    model = await StorageRoomApi.UploadPhotoRoom(request, Cts.Token);
                }

                await _form!.FieldChanged.InvokeAsync();

                await RoomState.FinishLoading.Invoke(model);
            }
            catch (Exception ex)
            {
                await ProcessException(ex);
            }
        }

        private async Task SaveHandle()
        {
            if (model == null) throw new InvalidOperationException("property is null");

            try
            {
                model.SanitizeOpenTextFields();

                var validator = new RoomValidation();

                var result = await validator.ValidateAsync(model, Cts.Token);

                if (result.IsValid)
                {
                    await RoomState.StartProcessing.Invoke(null);

                    if (Navigation.Uri.Contains("/add/", StringComparison.OrdinalIgnoreCase))
                    {
                        model = await RoomApi.Add(propertyId, model, Cts.Token);

                        foreach (var item in PhotoRequests)
                        {
                            model = await StorageRoomApi.UploadPhotoRoom(item, Cts.Token);
                        }
                    }
                    else
                    {
                        model = await RoomApi.Update(propertyId, model, Cts.Token);
                    }

                    await RoomState.FinishProcessing.Invoke(model);

                    IsDirty = false; StateHasChanged();

                    await ShowSuccess(Translations.Notification.OperationCompleted);

                    await JsRuntime.InvokeVoidAsync("history.back", cancellationToken: Cts.Token);
                }
                else
                {
                    var message = result.Errors[0].ErrorMessage;

                    await ShowWarning(message);
                }
            }
            catch (Exception ex)
            {
                await ProcessException(ex);
            }
        }

        private async Task DeletePhoto(RoomPhoto item)
        {
            model = await StorageRoomApi.DeletePhotoGallery(new PhotoRequest { PropertyId = propertyId, RoomId = roomId, PhotoId = item.PhotoId }, Cts.Token);
            await RoomState.FinishLoading.Invoke(model);
        }
    }
}