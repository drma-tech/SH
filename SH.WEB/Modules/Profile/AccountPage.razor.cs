using SH.WEB.Api.Module.Cosmos.Authenticated;

namespace SH.WEB.Modules.Profile
{
    public partial class AccountPage
    {
        private RenderControlState<ProfileModel?> ActionsProfile { get; set; } = new RenderControlState<ProfileModel?>(null, obj => obj == null);
        private ProfileModel? profile;

        protected override async Task LoadStaticDataAsync()
        {
            profile = await ProfileApi.Get([ActionsProfile], true, Cts.Token);
        }

        private async Task Delete(ProfileProperty property)
        {
            try
            {
                if (await DialogService.ConfirmDelete())
                {
                    profile = await ProfileApi.DeleteProperty(property.PropertyId, Cts.Token);
                    await ActionsProfile.FinishLoading.Invoke(profile);
                }
            }
            catch (Exception ex)
            {
                await ProcessException(ex);
            }
        }
    }
}