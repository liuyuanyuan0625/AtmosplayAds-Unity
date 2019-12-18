/// Base type representing a ZPLAD* pointer
typedef const void *ZPLADTypeRef;

/// Type representing a Unity interstitial client
typedef const void *ZPLADTypeInterstitialClientRef;

/// Type representing a Unity reward video client
typedef const void *ZPLADTypeRewardVideoClientRef;

/// Type representing a ZPLADInterstitialRef
typedef const void *ZPLADTypeInterstitialRef;

/// Type representing a ZPLADRewardVideoRef
typedef const void *ZPLADTypeRewardVideoRef;

/// Callback for when a interstitial ad request was successfully loaded.
typedef void (*ZPLADInterstitialDidReceivedAdCallback)(
    ZPLADTypeInterstitialClientRef *interstitialClient);

/// Callback for when a interstitial ad request failed.
typedef void (*ZPLADInterstitialDidFailToReceiveAdWithErrorCallback)(
    ZPLADTypeInterstitialClientRef *interstitialClient, const char *error);

/// Callback for when a interstitial video has started to play.
typedef void (*ZPLADInterstitialVideoDidStartPlayingCallback)(
    ZPLADTypeInterstitialClientRef *interstitialClient);

/// Callback for when a interstitial "INSTALL" button is clicked.
typedef void (*ZPLADInterstitiaDidClickCallback)(
    ZPLADTypeInterstitialClientRef *interstitialClient);

/// Callback for when a interstitial video is closed
typedef void (*ZPLADInterstitialVideoDidCloseCallback)(
    ZPLADTypeInterstitialClientRef *interstitialClient);

/// Callback for when a interstitial completes end.
typedef void (*ZPLADInterstitialDidCompleteCallback)(
    ZPLADTypeInterstitialClientRef *interstitialClient);

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
