#if UNITY_ANDROID
namespace AtmosplayAds.Android
{
    internal static class Utils
    {

        public const string RewardVideoClassName = "com.atmosplay.ads.RewardVideo";
        public const string UnityRewardVideoAdListenerClassName = "com.atmosplay.ads.UnityRewardVideoAdListener";

        public const string InterstitialClassName = "com.atmosplay.ads.Interstitial";
        public const string UnityInterstitialAdListenerClassName = "com.atmosplay.ads.UnityInterstitialAdListener";

        public const string BannerClassName = "com.atmosplay.ads.Banner";
        public const string UnityBannerAdListenerClassName = "com.atmosplay.ads.UnityBannerAdListener";

        public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";

        public const string BundleClassName = "android.os.Bundle";
        public const string DateClassName = "java.util.Date";
    }
}
#endif