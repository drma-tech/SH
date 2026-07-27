using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;
using Polly;
using Polly.Extensions.Http;
using SH.WEB.Core.Auth;
using SH.WEB.Modules.Auth.Core;
using SH.WEB.Modules.Profile.Core;
using SH.WEB.Modules.Subscription.Core;
using System.Globalization;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.UseSentry(options =>
{
    options.Dsn = "https://42509737a1905acfacb225afeceda501@o4510938040041472.ingest.us.sentry.io/4511616754057216";
    options.DiagnosticLevel = SentryLevel.Warning;
    options.Environment = builder.HostEnvironment.Environment;

    options.TracePropagationTargets = []; //Disable tracing because it breaks communication with external APIs.

    options.SetBeforeSend(evt =>
    {
        const string error = "error";

        evt.Release = $"sh-blazor@{AppStateStatic.Version ?? error}";

        evt.SetTag("custom.version", AppStateStatic.Version ?? error);
        evt.SetTag("custom.platform", AppStateStatic.GetSavedPlatform()?.ToString() ?? error);

        evt.SetExtra("browser_name", AppStateStatic.BrowserName ?? error);
        evt.SetExtra("browser_version", AppStateStatic.BrowserVersion ?? error);
        evt.SetExtra("operation_system", AppStateStatic.OperatingSystem ?? error);

        return evt;
    });
});

builder.Logging.SetMinimumLevel(LogLevel.Warning);

if (builder.RootComponents.Empty())
{
    builder.RootComponents.Add<SH.WEB.App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");
}

ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress, builder.Configuration);

var app = builder.Build();

var nav = app.Services.GetService<NavigationManager>();
var js = app.Services.GetRequiredService<IJSRuntime>();

await ConfigureCulture(nav, js);

AppStateStatic.Version = await AppStateStatic.GetAppVersion(js, CancellationToken.None);
AppStateStatic.BrowserName = await js.Utils().GetBrowserName(CancellationToken.None);
AppStateStatic.BrowserVersion = await js.Utils().GetBrowserVersion(CancellationToken.None);
AppStateStatic.OperatingSystem = await js.Utils().GetOperatingSystem(CancellationToken.None);

await js.Utils().SetStorage("app-version", AppStateStatic.Version, JavascriptContext.Default.String, CancellationToken.None);
_ = await AppStateStatic.GetPlatform(js, CancellationToken.None);
await js.Services().InitGoogleAnalytics(AppStateStatic.Version, CancellationToken.None);
await js.Services().InitUserBack(AppStateStatic.Version, CancellationToken.None);

await app.RunAsync();

static void ConfigureServices(IServiceCollection collection, string baseAddress, IConfiguration configuration)
{
    ConfigurePrerendering();

    collection.AddMudServices(config =>
    {
        config.SnackbarConfiguration.PreventDuplicates = false;
        config.SnackbarConfiguration.VisibleStateDuration = 10000;
    });

    collection.AddPWAUpdater();
    collection.AddScoped<AppVersionHandler>();

    var isLocal = baseAddress.Contains("localhost") || baseAddress.Contains("127.0.0.1");
    Uri? webUri = null;
    Uri? apiUri = null;

    if (isLocal)
    {
        webUri = new Uri(baseAddress);
        apiUri = new Uri(configuration["DownstreamApi:BaseUrl"] ?? throw new UnhandledException("BaseUrl not defined"));
    }
    else
    {
        var builder = new UriBuilder(baseAddress);

        //force apex domain
        //if (builder.Host.StartsWith("www.", StringComparison.OrdinalIgnoreCase))
        //{
        //    builder.Host = builder.Host[4..];
        //}

        webUri = builder.Uri;
        apiUri = new Uri(webUri, "api/");
    }

    //Local (json files and other assets, not the API)
    collection.AddHttpClient("Local", c => { c.BaseAddress = webUri; });

    //Anonymous
    collection.AddHttpClient("Anonymous", (service, options) => { options.BaseAddress = apiUri; options.Timeout = TimeSpan.FromSeconds(30); })
        .AddHttpMessageHandler<AppVersionHandler>()
        .AddPolicyHandler(request => request.Method == HttpMethod.Get ? GetRetryPolicy() : Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>());

    //Authenticated
    collection.AddScoped<AuthenticationStateProvider, SupabaseAuthStateProvider>();
    collection.AddScoped<CustomAuthorizationHandler>();
    collection.AddHttpClient("Authenticated", (service, options) => { options.BaseAddress = apiUri; options.Timeout = TimeSpan.FromSeconds(30); })
        .AddHttpMessageHandler<CustomAuthorizationHandler>()
        .AddHttpMessageHandler<AppVersionHandler>()
        .AddPolicyHandler(request => request.Method == HttpMethod.Get ? GetRetryPolicy() : Policy.NoOpAsync().AsAsyncPolicy<HttpResponseMessage>());

    collection.AddAuthorizationCore();

    ConfigureApi(collection);
}

static void ConfigurePrerendering()
{
    const string loading = "loading";

    AppStateStatic.Version = loading;
    AppStateStatic.BrowserName = loading;
    AppStateStatic.BrowserVersion = loading;
    AppStateStatic.OperatingSystem = loading;
}

static void ConfigureApi(IServiceCollection collection)
{
    collection.AddScoped<PrincipalApi>();
    collection.AddScoped<PrincipalImportApi>();
    collection.AddScoped<LoginApi>();
    collection.AddScoped<PublicLoginApi>();
    collection.AddScoped<MapApi>();

    collection.AddScoped<PaymentConfigurationApi>();
    collection.AddScoped<PaymentAuthApi>();
    collection.AddScoped<PaymentPublicApi>();
    collection.AddScoped<IpInfoApi>();
    collection.AddScoped<PropertyApi>();
    collection.AddScoped<RoomApi>();
    collection.AddScoped<ProfileApi>();
}

static async Task ConfigureCulture(NavigationManager? nav, IJSRuntime js)
{
    //app language

    var uri = new Uri(nav!.Uri);

    var appLanguage = await ExtensionMethodsWeb.GetRouteLanguage(js, uri.AbsolutePath);

    if (appLanguage.NotEmpty())
    {
        CultureInfo cultureInfo;

        try
        {
            cultureInfo = new CultureInfo(appLanguage);
        }
        catch (Exception)
        {
            cultureInfo = CultureInfo.CurrentCulture;
        }

        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }
}

//https://github.com/App-vNext/Polly/wiki/Polly-and-HttpClientFactory
static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError() // 408,5xx
        .WaitAndRetryAsync([TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2)]);
}
