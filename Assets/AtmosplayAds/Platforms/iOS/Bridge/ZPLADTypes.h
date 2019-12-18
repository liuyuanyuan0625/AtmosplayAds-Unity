/// Base type representing a Atmosplay* pointer
typedef const void *AtmosplayTypeRef;
/// Type representing a Unity interstitial client
typedef const void *AtmosplayTypeInterstitialClientRef;
/// Type representing a Unity rewarded video client
typedef const void *AtmosplayTypeRewardedVideoClientRef;
/// Type representing a AtmosplayInterstitialRef
typedef const void *AtmosplayTypeInterstitialRef;
/// Type representing a AtmosplayRewardedVideoRef
typedef const void *AtmosplayTypeRewardedVideoRef;
/// Callback for when a interstitial ad request was successfully loaded.
typedef void (*AtmosplayInterstitialDidReceivedAdCallback)(
    AtmosplayTypeInterstitialClientRef *interstitialClient);
/// Callback for when a interstitial ad request failed.
typedef void (*AtmosplayInterstitialDidFailToReceiveAdWithErrorCallback)(
    AtmosplayTypeInterstitialClientRef *interstitialClient, const char *error);
/// Callback for when a interstitial video has started to play.
typedef void (*AtmosplayInterstitialDidStartPlayingCallback)(
    AtmosplayTypeInterstitialClientRef *interstitialClient);
/// Callback for when a interstitial "INSTALL" button is clicked.
typedef void (*AtmosplayInterstitiaDidClickCallback)(
    AtmosplayTypeInterstitialClientRef *interstitialClient);
/// Callback for when a interstitial video is closed
typedef void (*AtmosplayInterstitialDidCloseCallback)(
    AtmosplayTypeInterstitialClientRef *interstitialClient);
/// Callback for when a interstitial completes end.
typedef void (*AtmosplayInterstitialDidCompleteCallback)(
    AtmosplayTypeInterstitialClientRef *interstitialClient);

#pragma mark:RewardVideo
/// Callback for when a reward video ad request was successfully loaded.
typedef void (*ZPLADRewardVideoDidReceivedAdCallback)(
    ZPLADTypeRewardVideoClientRef *rewardVideoClient);

/// Callback for when a reward video ad request failed.
typedef void (*ZPLADRewardVideoDidFailToReceiveAdWithErrorCallback)(
    ZPLADTypeRewardVideoRef *rewardVideoClient, const char *error);

/// Callback for when a reward video video has started to play.
typedef void (*ZPLADRewardVideoVideoDidStartPlayingCallback)(
    ZPLADTypeRewardVideoRef *rewardVideoClient);

/// Callback for when a reward video "INSTALL" button is clicked.
typedef void (*ZPLADRewardVideoDidClickCallback)(
    ZPLADTypeRewardVideoRef *rewardVideoClient);

typedef void (*ZPLADRewardVideoDidRewardCallback)(
    ZPLADTypeRewardVideoRef *rewardVideoClient);

/// Callback for when a reward video video is closed
typedef void (*ZPLADRewardVideoVideoDidCloseCallback)(
    ZPLADTypeRewardVideoRef *rewardVideoClient);

/// Callback for when a reward video completes end.
typedef void (*ZPLADRewardVideoDidCompleteCallback)(
    ZPLADTypeRewardVideoRef *rewardVideoClient);

#pragma mark: banner
/// Type representing a Unity banner client
typedef const void *AtmosplayTypeBannerClientRef;

/// Type representing a AtmosplayAdsBanner
typedef const void *AtmosplayTypeBannerRef;

#pragma mark - banner ads callback
/// Callback for when a banner ad request was successfully loaded.
typedef void (*AtmosplayBannerDidReceiveAdCallback)(AtmosplayTypeBannerClientRef *bannerClient);

/// Callback for when a banner ad request failed.
typedef void (*AtmosplayBannerDidFailToReceiveAdWithErrorCallback)(AtmosplayTypeBannerClientRef *bannerClient, const char *error);

/// Callback for when a full screen view is about to be presented as a result of a banner click.
typedef void (*AtmosplayBannerDidClickCallback)(AtmosplayTypeBannerClientRef *bannerClient);
