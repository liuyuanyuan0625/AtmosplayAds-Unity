using AtmosplayAds.Api;
using AtmosplayAds.Common;
using UnityEngine;

namespace AtmosplayAds
{
    public class AtmosplayAdsClientFactory
    {
        public static IRewardVideoClient BuildRewardVideoClient(string adAppId, string adUnitId)
        {
#if UNITY_ANDROID
            return new Android.RewardVideoClient(adAppId);
#elif UNITY_IPHONE
            return new iOS.RewardVideoClient(adAppId, adUnitId);
#else
            return new DummyClient();
#endif
        }

        public static IInterstitialClient BuildInterstitialClient(string adAppId, string adUnitId)
        {
#if UNITY_ANDROID
            return new Android.InterstitialClient(adAppId);
#elif UNITY_IPHONE
            return new iOS.InterstitialClient(adAppId, adUnitId);
#else
            return new Common.DummyClient();
#endif
        }
        public static IBannerClient BuildBannerClient()
        {
#if UNITY_ANDROID
            return new Android.BannerClient();
#elif UNITY_IPHONE
            return new iOS.BannerClient();
#else
            return new Common.DummyClient();
#endif
        }
    }
}