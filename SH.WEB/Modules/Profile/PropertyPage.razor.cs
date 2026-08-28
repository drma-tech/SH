using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;
using SH.Shared.Models.Property;
using SH.Shared.Models.Property.Core;
using SH.Shared.Requests;
using FluentValidation;

namespace SH.WEB.Modules.Profile
{
    public partial class PropertyPage
    {
        [Parameter] public required string propertyId { get; set; }

        private RenderControlState<PropertyModel?> PropertyState { get; set; } = new RenderControlState<PropertyModel?>(null, obj => obj == null);
        private PropertyModel? model;

        private RenderControlState<ISet<PropertyFacilityModel>> FacilitiesState { get; set; } = new RenderControlState<ISet<PropertyFacilityModel>>(new HashSet<PropertyFacilityModel>(), p => p.Empty());
        private RenderControlState<ISet<PropertyAmenityModel>> AmenitiesState { get; set; } = new RenderControlState<ISet<PropertyAmenityModel>>(new HashSet<PropertyAmenityModel>(), p => p.Empty());
        private RenderControlState<ISet<AccessibilityFeature>> AccessibilitiesState { get; set; } = new RenderControlState<ISet<AccessibilityFeature>>(new HashSet<AccessibilityFeature>(), p => p.Empty());

        private string? searchAddress { get; set; }

        public PropertyFacility? PropertyFacility { get; set; }
        public string? PropertyFacilityCustomName { get; set; }
        public PropertyAmenity? PropertyAmenity { get; set; }
        public string? PropertyAmenityCustomName { get; set; }
        public AccessibilityFeature? PropertyAccessibility { get; set; }

        private HereJson? here;

        public ICollection<PhotoRequest> PhotoRequests { get; set; } = [];

        private MudForm? _form;
        private bool IsDirty { get; set; }

        private void OnFieldChanged(FormFieldChangedEventArgs args)
        {
            IsDirty = true;
        }

        protected override IReadOnlyList<string?> GetParameterKey()
        {
            return [propertyId];
        }

        protected override async Task LoadParameterDataAsync()
        {
            if (Navigation.Uri.Contains("/add/", StringComparison.OrdinalIgnoreCase))
            {
                model ??= new(propertyId);
                model.Members.Add(new PropertyMember { UserId = AppStateStatic.UserId, Role = PropertyUserRole.Owner });

                await PropertyState.FinishLoading.Invoke(model);
            }
            else
            {
                model = await PropertyApi.Get(propertyId, [PropertyState], Cts.Token) ?? throw new NotificationException("property not found");
            }

            await FacilitiesState.FinishLoading.Invoke(model.Facilities);
            await AmenitiesState.FinishLoading.Invoke(model.Amenities);
            await AccessibilitiesState.FinishLoading.Invoke(model.Accessibilities);
        }

        private async Task SearchClick()
        {
            here = await MapApi.HereGeocode(searchAddress, Cts.Token);
            HereRoot? root = null;

            if (here?.items.Empty() ?? true)
            {
                await ShowError("Your search did not return any valid address.");
            }
            else if (here.items.Count > 1)
            {
                await DialogService.SelectAddressPopup(here.items, new EventCallbackFactory().Create(this, async (HereRoot? result) => { await PopulateAddress(result); }));
            }
            else
            {
                root = here.items.FirstOrDefault();
                await PopulateAddress(root);
            }
        }

        private async Task PopulateAddress(HereRoot? root)
        {
            model!.Address = new PropertyAddress
            {
                Latitude = root?.position?.lat ?? 0,
                Longitude = root?.position?.lng ?? 0,
                Label = root?.address?.label,
                CountryCode = root?.address?.countryCode,
                CountryName = root?.address?.GetCountry(),
                State = root?.address?.GetState(),
                City = root?.address?.GetCity(),
                District = root?.address?.district,
                Street = root?.address?.street,
                PostalCode = root?.address?.postalCode,
                HouseNumber = root?.address?.houseNumber,
            };

            var country = root?.address?.countryCode.ParseToEnum<CountryExtended>();
            model.Languages = DataHelper.GetLanguages((Country?)country).ToHashSet();

            model.CountryCode = model.Address.CountryCode.ToSlug();
            model.StateCode = model.Address.State.ToSlug();
            model.CityCode = model.Address.City.ToSlug();
        }

        private async Task AddFacility()
        {
            if (PropertyFacility.HasValue)
            {
                model!.Facilities.Add(new PropertyFacilityModel { Facility = PropertyFacility.Value, CustomName = PropertyFacilityCustomName });
                PropertyFacility = null;
                PropertyFacilityCustomName = null;
                await _form!.FieldChanged.InvokeAsync();
            }
        }

        private async Task RemoveFacility(MudChip<PropertyFacilityModel> chip)
        {
            model!.Facilities.Remove(chip.Value!);
            await _form!.FieldChanged.InvokeAsync();
        }

        private async Task AddAmenity()
        {
            if (PropertyAmenity.HasValue)
            {
                model!.Amenities.Add(new PropertyAmenityModel { Amenity = PropertyAmenity.Value, CustomName = PropertyAmenityCustomName });
                PropertyAmenity = null;
                PropertyAmenityCustomName = null;
                await _form!.FieldChanged.InvokeAsync();
            }
        }

        private async Task RemoveAmenity(MudChip<PropertyAmenityModel> chip)
        {
            model!.Amenities.Remove(chip.Value!);
            await _form!.FieldChanged.InvokeAsync();
        }

        private async Task AddAccessibility()
        {
            if (PropertyAccessibility.HasValue)
            {
                model!.Accessibilities.Add(PropertyAccessibility.Value);
                PropertyAccessibility = null;
                await _form!.FieldChanged.InvokeAsync();
            }
        }

        private async Task RemoveAccessibility(MudChip<AccessibilityFeature> chip)
        {
            model!.Accessibilities.Remove(chip.Value!);
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
                var request = new PhotoRequest { PropertyId = propertyId, Buffer = buffer, Caption = caption };

                if (Navigation.Uri.Contains("/add/", StringComparison.OrdinalIgnoreCase))
                {
                    PhotoRequests.Add(request);
                }
                else
                {
                    model = await StoragePropertyApi.UploadPhotoProperty(request, Cts.Token);
                }

                await _form!.FieldChanged.InvokeAsync();

                await PropertyState.FinishLoading.Invoke(model);
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

                var validator = new PropertyValidation();
                var result = await validator.ValidateAsync(model, options => options.IncludeRuleSets("OFF"), cancellation: Cts.Token);

                if (result.IsValid)
                {
                    await PropertyState.StartProcessing.Invoke(null);

                    if (Navigation.Uri.Contains("/add/", StringComparison.OrdinalIgnoreCase))
                    {
                        model = await PropertyApi.Add(model, Cts.Token);

                        foreach (var item in PhotoRequests)
                        {
                            model = await StoragePropertyApi.UploadPhotoProperty(item, Cts.Token);
                        }
                    }
                    else
                    {
                        model = await PropertyApi.Update(model, Cts.Token);
                    }

                    await PropertyState.FinishProcessing.Invoke(model);

                    IsDirty = false; StateHasChanged();

                    await ShowSuccess(Translations.Notification.OperationCompleted);

                    Navigation.NavigateTo($"/{Culture}/account");
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

        private async Task DeletePhoto(PropertyPhoto item)
        {
            model = await StoragePropertyApi.DeletePhotoGallery(new PhotoRequest { PropertyId = propertyId, PhotoId = item.PhotoId }, Cts.Token);
            await PropertyState.FinishLoading.Invoke(model);
        }

        private async Task Delete(RoomShort room)
        {
            if (await DialogService.ConfirmDelete())
            {
                model = await PropertyApi.DeleteRoom(model!.Identity.RawId, room.RoomId, Cts.Token);
                await PropertyState.FinishLoading.Invoke(model);
            }
        }

        private async Task AddRoom()
        {
            if (Navigation.Uri.Contains("/add/", StringComparison.OrdinalIgnoreCase))
            {
                await ShowInfo("Save the property before adding a new room.");
                return;
            }

            Navigation.NavigateTo($"/{Culture}/room/add/{propertyId}/{Guid.NewGuid()}");
        }
    }
}