/// Base type representing a Atmosplay* pointer
typedef const void *AtmosplayTypeRef;

#pragma mark - Interstitial
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

#pragma mark - RewardedVideo
/// Callback for when a rewarded video ad request was successfully loaded.
typedef void (*AtmosplayRewardedVideoDidReceivedAdCallback)(
    AtmosplayTypeRewardedVideoClientRef *rewardedVideoClient);

/// Callback for when a rewarded video ad request failed.
typedef void (*AtmosplayRewardedVideoDidFailToReceiveAdWithErrorCallback)(
    AtmosplayTypeRewardedVideoRef *rewardedVideoClient, const char *error);
/// Callback for when a rewarded video video has started to play.
typedef void (*AtmosplayRewardedVideoDidStartPlayingCallback)(
    AtmosplayTypeRewardedVideoRef *rewardedVideoClient);
/// Callback for when a rewarded video "INSTALL" button is clicked.
typedef void (*AtmosplayRewardedVideoDidClickCallback)(
    AtmosplayTypeRewardedVideoRef *rewardedVideoClient);
typedef void (*AtmosplayRewardedVideoDidRewardCallback)(
    AtmosplayTypeRewardedVideoRef *rewardedVideoClient);
/// Callback for when a rewarded video video is closed
typedef void (*AtmosplayRewardedVideoDidCloseCallback)(
    AtmosplayTypeRewardedVideoRef *rewardedVideoClient);
/// Callback for when a rewarded video complete end.
typedef void (*AtmosplayRewardedVideoDidCompleteCallback)(
    AtmosplayTypeRewardedVideoRef *rewardedVideoClient);

#pragma mark - Banner
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

#pragma mark - Float Ad
/// Type representing a Unity float ad client
typedef const void *AtmosplayTypeFloatAdClientRef;

/// Type representing a AtmosplayAdsFloat
typedef const void *AtmosplayTypeFloatAdRef;

#pragma mark - float ads callback
typedef void (*AtmosplayFloatAdDidReceiveAdCallback)(AtmosplayTypeFloatAdClientRef *floatAdClient);

typedef void (*AtmosplayFloatAdDidFailToReceiveAdWithErrorCallback)(AtmosplayTypeFloatAdClientRef *floatAdClient, const char *error);

typedef void (*AtmosplayFloatAdDidClickCallback)(AtmosplayTypeFloatAdClientRef *floatAdClientt);

typedef void (*AtmosplayFloatAdDidStartedCallback)(AtmosplayTypeFloatAdClientRef *floatAdClientt);

typedef void (*AtmosplayFloatAdDidFinishedCallback)(AtmosplayTypeFloatAdClientRef *floatAdClientt);

typedef void (*AtmosplayFloatAdDidRewardedCallback)(AtmosplayTypeFloatAdClientRef *floatAdClientt);

typedef void (*AtmosplayFloatAdDidClosedCallback)(AtmosplayTypeFloatAdClientRef *floatAdClientt);


#pragma mark - Window Ad
typedef const void *AtmosplayTypeWindowAdClientRef;

typedef const void *AtmosplayTypeWindowAdRef;

#pragma mark - window ads callback
typedef void (*AtmosplayWindowAdDidReceiveAdCallback)(AtmosplayTypeWindowAdClientRef *windowAdClient);

typedef void (*AtmosplayWindowAdDidFailToReceiveAdWithErrorCallback)(AtmosplayTypeWindowAdClientRef *windowAdClient, const char *error);

typedef void (*AtmosplayWindowAdDidClickCallback)(AtmosplayTypeWindowAdClientRef *windowAdClient);

typedef void (*AtmosplayWindowAdDidStartedCallback)(AtmosplayTypeWindowAdClientRef *windowAdClient);

typedef void (*AtmosplayWindowAdDidFinishedCallback)(AtmosplayTypeWindowAdClientRef *windowAdClient);

typedef void (*AtmosplayWindowAdDidFailToShowCallback)(AtmosplayTypeWindowAdClientRef *windowAdClient);

typedef void (*AtmosplayWindowAdDidClosedCallback)(AtmosplayTypeWindowAdClientRef *windowAdClient);