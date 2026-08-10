using Microsoft.AspNetCore.Components;
using MudBlazor;
using SH.WEB.Modules.Auth;
using SH.WEB.Modules.Help;
using SH.WEB.Modules.Profile.Components;
using SH.WEB.Modules.Subscription.Components;
using SH.WEB.Shared;

namespace SH.WEB.Core.Helper;

public static class PopupHelper
{
    public static readonly EventCallbackFactory Factory = new();

    public static async Task SelectAddressPopup(this IDialogService service, IEnumerable<HereRoot> items, EventCallback<HereRoot?> selected)
    {
        var parameters = new DialogParameters<SelectAddressPopup>
        {
            { x => x.Items, items },
            { x => x.Selected, selected },
        };

        await service.ShowAsync<SelectAddressPopup>("Select address", parameters, Options(MaxWidth.Medium));
    }

    public static async Task SelectPicturePopup(this IDialogService service, EventCallback<(byte[], string?)> pictureChanged)
    {
        var parameters = new DialogParameters<SelectPicturePopup>
        {
            { x => x.PictureChanged, pictureChanged },
        };

        await service.ShowAsync<SelectPicturePopup>("Picture", parameters, Options(MaxWidth.Medium));
    }

    public static async Task AccountPopup(this IDialogService service)
    {
        var parameters = new DialogParameters<AccountPopup> { };

        await service.ShowAsync<AccountPopup>(Translations.Module.Auth.MyAccount, parameters, Options(MaxWidth.Small));
    }

    public static async Task SettingsPopup(this IDialogService service)
    {
        await service.ShowAsync<SettingsPopup>(Translations.Module.Help.Settings, Options(MaxWidth.Small));
    }

    public static async Task SubscriptionPopup(this IDialogService service)
    {
        var parameters = new DialogParameters<SubscriptionPopup> { };

        await service.ShowAsync<SubscriptionPopup>(Translations.Module.Subscription.MySubscription, parameters, Options(MaxWidth.Medium));
    }

    public static async Task OnboardingPopup(this IDialogService service, string culture)
    {
        var parameters = new DialogParameters<Onboarding>
        {
            { x => x.Culture, culture },
        };

        await service.ShowAsync<Onboarding>(Translations.Module.Help.WelcomeTo.CustomFormat(AppInfo.Title), parameters, Options(MaxWidth.Medium));
    }

    public static async Task AskReviewPopup(this IDialogService service)
    {
        await service.ShowAsync<AskReview>(Translations.Module.Help.WriteReviewTitle.CustomFormat(AppInfo.Title), Options(MaxWidth.Small, allowClose: false, showHeader: false));
    }

    public static async Task<bool> ConfirmDelete(this IDialogService service, string? customMessage = null)
    {
        return await service.ShowMessageBoxAsync(AppInfo.Title, customMessage ?? Translations.Notification.ConfirmDelete,
            Translations.Button.Ok, Translations.Button.Cancel, cancelText: null, Options(MaxWidth.Small, allowClose: false, showHeader: true)) ?? false;
    }

    public static DialogOptions Options(MaxWidth width, bool allowClose = true, bool showHeader = true)
    {
        return new DialogOptions
        {
            CloseOnEscapeKey = allowClose,
            CloseButton = allowClose,
            BackdropClick = allowClose,
            NoHeader = !showHeader,
            Position = DialogPosition.Center,
            MaxWidth = width,
        };
    }
}
