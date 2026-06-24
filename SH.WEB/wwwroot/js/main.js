const ua = navigator.userAgent;

window.browser = window?.bowser?.getParser
    ? window.bowser.getParser(ua)
    : null;

const botUAs = ["google", "baidu", "bingbot", "duckduckbot", "teoma", "slurp", "yandex", "toutiao", "bytespider", "applebot", "crawler"];

export const isBot = botUAs.some(bot => ua.toLowerCase().includes(bot)) || navigator.webdriver;

function testBrowserVersion(rules, ignore = false, fallback = false) {
    if (ignore) return false;

    if (!window.browser) return fallback;

    try {
        return window.browser.satisfies(rules);
    } catch {
        return fallback;
    }
}

//browser versions not compatible with SIMD
export const hideBlazorIndex = testBrowserVersion(
    {
        chrome: "<91", //may 21
        edge: "<91", //may 21
        firefox: "<89", //may 21
        safari: "<16.4", //mar 23
        opera: "<77", //jun 21
    },
    /Mediapartners-Google/i.test(ua),
    false // uncertain environment → allow
);

//probably a bot, so doesnt support sw
export const disableServiceWorker = testBrowserVersion(
    {
        chrome: "<134", //special case (usually bots)
        edge: "<91", //may 21
        firefox: "<89", //may 21
        safari: "<16.4", //mar 23
        opera: "<77", //jun 21
    },
    false,
    true // uncertain environment → disable
);

export const isLocalhost = location.host.includes("localhost");
export const isDev = location.hostname.includes("develop");
export const isWebview = /webtonative/i.test(ua);
export const isPrintScreen = location.href.includes("printscreen");

export const servicesConfig = {
    AnalyticsCode: "G-YNG1YC3PNV",
    ClarityKey: "xboxojdpc4",
    UserBackToken: "A-A2J4M5NKCbDp1QyQe7ogemmmq",
    SentryDsn: "https://42509737a1905acfacb225afeceda501@o4510938040041472.ingest.us.sentry.io/4511616754057216",
};

export const supabaseConfig = {
    projectUrl: "https://llwdeaieehfqabqgsbjg.supabase.co",
    supabaseKey: "sb_publishable_eTtEX1u9zYlSJF0Rl25W-g_M_Dl4O6a",
};

export const baseApiUrl = isLocalhost ? "http://localhost:7071" : "";