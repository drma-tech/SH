using SH.WEB.Modules.Help.Core;

namespace SH.WEB.Core
{
    public static class AppInfo
    {
        public static string CompanyName { get; set; } = "DRMA Tech";
        public static string CompanyWebSite { get; set; } = $"https://www.drma-tech.com";

        public static string Title { get; set; } = "Shared Home";
        public static string Domain { get; set; } = "shared-home";
        public static string WebSite { get; set; } = $"https://www.{Domain}.com";
        public static int Year { get; set; } = 2026;

        public static readonly string? _windowsId = "";
        public static readonly string? _googleId = "";
        public static readonly string? _appleId = "";
        public static readonly string? _huaweiId = "";
        public static readonly string? _xiaomiId = "";
        public static readonly string? _amazonId = "";

        public static readonly StoreLink[] Stores =
        [
            new(Platform.windows, "Microsoft Store", $"https://apps.microsoft.com/detail/{_windowsId}", "/logo/microsoft-store.webp" ),
            new(Platform.play, "Google Play", $"https://play.google.com/store/apps/details?id={_googleId}", "/logo/google-play.webp" ),
            new(Platform.ios, "App Store", $"https://apps.apple.com/us/app/{_appleId}", "/logo/app-store.webp" ),
            new(Platform.huawei, "Huawei AppGallery", $"https://appgallery.huawei.com/app/{_huaweiId}", "/logo/huawei.webp" ),
            new(Platform.xiaomi, "Xiaomi GetApps", $"https://global.app.mi.com/details?id={_xiaomiId}", "/logo/xiaomi.webp" ),
            new(Platform.amazon, "Amazon Appstore", $"https://www.amazon.com/gp/product/{_amazonId}", "/logo/amazon.webp" )
        ];

        public static readonly ProductLink[] Products =
        [
            new("Streaming Discovery", "Discover Movies and Series on Streaming Platforms", "https://www.streamingdiscovery.com", "/logo/streamingdiscovery.webp", true ),
            new("Modern Matchmaker", "Find a compatible partner through Smart Matchmaking", "https://www.modern-matchmaker.com", "/logo/modern-matchmaker.webp", true ),
            new("My Next Spot", "Find the Best Cities and Countries to Live or Travel", "https://www.my-next-spot.com", "/logo/next-spot.webp", true ),
            new("Web Standards", "Web Standards Generator for Websites and PWAs", "https://www.web-standards.com", "/logo/webstandards.webp", false ),
            //new("Shared Home", "Find your next home and be part of a community", "https://www.shared-home.com", "/logo/shared-home.webp", true ),
       ];
    }
}
