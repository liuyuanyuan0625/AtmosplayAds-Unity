#if UNITY_IOS

using System;
using System.Runtime.InteropServices;

namespace AtmosplayAds.iOS
{
    internal enum AtmosplayBannerAdSize
    {
        /// iPhone and iPod Touch ad size. Typically 320x50.
        kAtmosplayAdsBanner320x50 = 1 << 0,
        /// Leaderboard size for the iPad. Typically 728x90.
        kAtmosplayAdsBanner728x90 = 1 << 1,
        /// An ad size that spans the full width of the application in portrait orientation. The height is
        /// typically 50 pixels on an iPhone/iPod UI, and 90 pixels tall on an iPad UI.
        kAtmosplayAdsSmartBannerPortrait = 1 << 3,
        /// An ad size that spans the full width of the application in landscape orientation. The height is
        /// typically 32 pixels on an iPhone/iPod UI, and 90 pixels tall on an iPad UI.
        kAtmosplayAdsSmartBannerLandscape = 1 << 4
     }
     
    class Externs
    {

        #region Common externs
        [DllImport("__Internal")]
        internal static extern IntPtr AtmosplayAdsRelease(IntPtr obj);
        #endregion

        #region Interstitial externs
        [DllImport("__Internal")]
        internal static extern IntPtr AtmosplayAdsCreateInterstitial(IntPtr interstitialClient, string adAppId, string adUnitId);

        [DllImport("__Internal")]
        internal static extern void AtmosplayAdsSetInterstitialAdCallbacks(
            IntPtr interstitial,
            InterstitialClient.AtmosplayInterstitialDidReceivedAdCallback adReceivedCallback,
            InterstitialClient.AtmosplayInterstitialDidFailToReceiveAdWithErrorCallback adFailedCallback,
            InterstitialClient.AtmosplayInterstitialDidStartPlayingCallback videoDidStartCallback,
            InterstitialClient.AtmosplayInterstitiaDidClickCallback didClickCallback,
            InterstitialClient.AtmosplayInterstitialDidCloseCallback videoDidCloseCallback,
            InterstitialClient.AtmosplayInterstitialDidCompleteCallback didCompleteCallback
        );

        [DllImport("__Internal")]
        internal static extern void AtmosplayAdsRequestInterstitial(IntPtr interstitial);

        [DllImport("__Internal")]
        internal static extern bool ZPLADInterstitialReady(IntPtr interstitial);

        [DllImport("__Internal")]
        internal static extern void ZPLADShowInterstitial(IntPtr interstitial);

        [DllImport("__Internal")]
        internal static extern void ZPLADSetInterstitialAutoload(IntPtr interstitial, bool autoload);

        [DllImport("__Internal")]
        internal static extern void ZPLADSetInterstitialChannelId(IntPtr interstitial, string channelId);
        #endregion


        #region RewardVideo externs
        [DllImport("__Internal")]
        internal static extern IntPtr ZPLADCreateRewardVideo(IntPtr rewardVideoClient, string adAppId, string adUnitId);

        [DllImport("__Internal")]
        internal static extern void ZPLADSetRewardVideoAdCallbacks(
            IntPtr interstitial,
            RewardVideoClient.AtmosplayRewardedVideoDidReceivedAdCallback adReceivedCallback,
            RewardVideoClient.AtmosplayRewardedVideoDidFailToReceiveAdWithErrorCallback adFailedCallback,
            RewardVideoClient.AtmosplayRewardedVideoDidStartPlayingCallback videoDidStartCallback,
            RewardVideoClient.AtmosplayRewardedVideoDidClickCallback didClickCallback,
            RewardVideoClient.AtmosplayRewardedVideoDidRewardCallback didRewardCallback,
            RewardVideoClient.AtmosplayRewardedVideoDidCloseCallback videoDidCloseCallback,
            RewardVideoClient.AtmosplayRewardedVideoDidCompleteCallback didCompleteCallback
        );

        [DllImport("__Internal")]
        internal static extern void ZPLADRequestRewardVideo(IntPtr rewardVideo);

        [DllImport("__Internal")]
        internal static extern bool ZPLADRewardVideoReady(IntPtr rewardVideo);

        [DllImport("__Internal")]
        internal static extern void ZPLADShowRewardVideo(IntPtr rewardVideo);

        [DllImport("__Internal")]
        internal static extern void ZPLADSetRewardVideoAutoload(IntPtr rewardVideo, bool autoload);

        [DllImport("__Internal")]
        internal static extern void ZPLADSetRewardVideoChannelId(IntPtr rewardVideo, string channelId);
        #endregion
        #region banner externs
        // banner
        [DllImport("__Internal")]
        internal static extern IntPtr InitAtmosplayBannerAd(IntPtr bannerView, string adAppId, string adUnitId);
        [DllImport("__Internal")]
        internal static extern void RequestBannerAd(IntPtr bannerView);
        [DllImport("__Internal")]
        internal static extern void ShowBannerView(IntPtr bannerView);
        [DllImport("__Internal")]
        internal static extern void HideBannerView(IntPtr bannerView);
        [DllImport("__Internal")]
        internal static extern void DestroyBannerView(IntPtr bannerView);
        [DllImport("__Internal")]
        internal static extern void SetBannerAdSize(IntPtr bannerView, AtmosplayBannerAdSize bannerSize);
        [DllImport("__Internal")]
        internal static extern void SetBannerPosition(IntPtr bannerView, int position);
        [DllImport("__Internal")]
        internal static extern void SetBannerChannelID(IntPtr bannerView, string channelId);
        [DllImport("__Internal")]
        internal static extern void SetBannerCallbacks(
           IntPtr bannerView,
           BannerClient.AtmosplayBannerDidReceiveAdCallback adReceivedCallback,
           BannerClient.AtmosplayBannerDidFailToReceiveAdWithErrorCallback adFailedCallback,
           BannerClient.AtmosplayBannerDidClickCallback adClickedCallback);


        #endregion
    }
}
#endif