#import "AtmosplayInterstitialBridge.h"
#import "AtmosplayRewardedVideoBridge.h"
#import "AtmosplayTypes.h"
#import "AtmosplayObjectCache.h"
#import "AtmosplayBannerBridge.h"
#import "AtmosplayFloatAdBridge.h"

static NSString *AtmosplayAdsStringFromUTF8String(const char *bytes) { return bytes ? @(bytes) : nil; }

#pragma mark - Interstitial method
/// Creates a AtmosplayInterstitialBridge and return its reference
AtmosplayTypeInterstitialRef AtmosplayAdsCreateInterstitial(AtmosplayTypeInterstitialClientRef *interstitialClient,
                                                       const char *adAppID,
                                                       const char *adUnitID) {
    AtmosplayInterstitialBridge *interstitial = [[AtmosplayInterstitialBridge alloc]
                      initWithInterstitialClientReference:interstitialClient
                                       adAppId: AtmosplayAdsStringFromUTF8String(adAppID)
                                       adUnitId: AtmosplayAdsStringFromUTF8String(adUnitID)];
    AtmosplayObjectCache *cache = [AtmosplayObjectCache sharedInstance];
    [cache.references setObject:interstitial forKey:[interstitial atmosplayAds_referenceKey]];
    return (__bridge AtmosplayTypeInterstitialRef)interstitial;
}
/// Sets the interstitial callback methods to be invoked during interstitial ad events.
void AtmosplayAdsSetInterstitialAdCallbacks(
        AtmosplayTypeInterstitialClientRef interstitialAd,
        AtmosplayInterstitialDidReceivedAdCallback adReceivedCallback,
        AtmosplayInterstitialDidFailToReceiveAdWithErrorCallback adFailedCallback,
        AtmosplayInterstitialDidStartPlayingCallback videoDidStartCallback,
        AtmosplayInterstitiaDidClickCallback adClickedCallback,
        AtmosplayInterstitialDidCloseCallback videoDidCloseCallback,
        AtmosplayInterstitialDidCompleteCallback adDidCompleteCallback) {
    AtmosplayInterstitialBridge *internalInterstitialAd = (__bridge AtmosplayInterstitialBridge *)interstitialAd;
    internalInterstitialAd.adReceivedCallback = adReceivedCallback;
    internalInterstitialAd.adFailedCallback = adFailedCallback;
    internalInterstitialAd.videoDidStartCallback = videoDidStartCallback;
    internalInterstitialAd.adClickedCallback = adClickedCallback;
    internalInterstitialAd.adDidCloseCallback = videoDidCloseCallback;
    internalInterstitialAd.videoDidCompleteCallback = adDidCompleteCallback;
}
/// Makes an interstitial ad request.
void AtmosplayAdsRequestInterstitial(AtmosplayTypeInterstitialRef interstitial) {    
    AtmosplayInterstitialBridge *internalInterstitial = (__bridge AtmosplayInterstitialBridge *)interstitial;
    [internalInterstitial loadAd];
}
/// Returns YES if the AtmosplayInterstitialBridge is ready to be show.
BOOL AtmosplayAdsInterstitialReady(AtmosplayTypeInterstitialRef interstitial) {
    AtmosplayInterstitialBridge *internalInterstitial = (__bridge AtmosplayInterstitialBridge *)interstitial;
    return [internalInterstitial isReady];
}
/// Shows the AtmosplayInterstitialBridge.
void AtmosplayAdsShowInterstitial(AtmosplayTypeInterstitialRef interstitial) {
    AtmosplayInterstitialBridge *internalInterstitial = (__bridge AtmosplayInterstitialBridge *)interstitial;
    [internalInterstitial show];
}
/// Sets AtmosplayInterstitialBridge autoload next ad.
void AtmosplayAdsSetInterstitialAutoload(AtmosplayTypeInterstitialRef interstitial, BOOL autoload) {
    AtmosplayInterstitialBridge *internalInterstitial = (__bridge AtmosplayInterstitialBridge *)interstitial;
    [internalInterstitial setAutoload:autoload];
}
/// Sets AtmosplayInterstitialBridge channel id.
void AtmosplayAdsSetInterstitialChannelId(AtmosplayTypeInterstitialRef interstitial, const char *channelId) {
    AtmosplayInterstitialBridge *internalInterstitial = (__bridge AtmosplayInterstitialBridge *)interstitial;
    [internalInterstitial setChannelId:AtmosplayAdsStringFromUTF8String(channelId)];
}

#pragma mark - RewardedVideo method
/// Creates a AtmosplayRewardedVideoBridge and return its reference
AtmosplayTypeRewardedVideoRef AtmosplayAdsCreateRewardedVideo(AtmosplayTypeRewardedVideoClientRef *rewardedVideoClient,
                                                 const char *adAppID,
                                                 const char *adUnitID) {
    AtmosplayRewardedVideoBridge *rewardedVideo = [[AtmosplayRewardedVideoBridge alloc]
                                       initWithRewardedVideoClientReference:rewardedVideoClient
                                       adAppId: AtmosplayAdsStringFromUTF8String(adAppID)
                                       adUnitId: AtmosplayAdsStringFromUTF8String(adUnitID)];
    AtmosplayObjectCache *cache = [AtmosplayObjectCache sharedInstance];
    [cache.references setObject:rewardedVideo forKey:[rewardedVideo atmosplayAds_referenceKey]];
    return (__bridge AtmosplayTypeRewardedVideoRef)rewardedVideo;
}

/// Sets the rewarded callback methods to be invoked during interstitial ad events.
void AtmosplayAdsSetRewardedVideoAdCallbacks(
        AtmosplayTypeRewardedVideoClientRef rewardedVideoAd,
        AtmosplayRewardedVideoDidReceivedAdCallback adReceivedCallback,
        AtmosplayRewardedVideoDidFailToReceiveAdWithErrorCallback adFailedCallback,
        AtmosplayRewardedVideoDidStartPlayingCallback videoDidStartCallback,
        AtmosplayRewardedVideoDidClickCallback adClickedCallback,
        AtmosplayRewardedVideoDidCloseCallback videoDidCloseCallback,
        AtmosplayRewardedVideoDidRewardCallback adDidRewardCallback,
        AtmosplayRewardedVideoDidCompleteCallback adDidCompleteCallback) {
    AtmosplayRewardedVideoBridge *internalRewardedVideoAd = (__bridge AtmosplayRewardedVideoBridge *)rewardedVideoAd;
    internalRewardedVideoAd.adReceivedCallback = adReceivedCallback;
    internalRewardedVideoAd.adFailedCallback = adFailedCallback;
    internalRewardedVideoAd.videoDidStartCallback = videoDidStartCallback;
    internalRewardedVideoAd.adClickedCallback = adClickedCallback;
    internalRewardedVideoAd.adRewardCallback = adDidRewardCallback;
    internalRewardedVideoAd.adDidCloseCallback = videoDidCloseCallback;
    internalRewardedVideoAd.videoDidCompleteCallback = adDidCompleteCallback;
}
/// Makes an rewarded video ad request.
void AtmosplayAdsRequestRewardedVideo(AtmosplayTypeRewardedVideoRef rewardedVideo) {
    AtmosplayRewardedVideoBridge *internalRewardedVideo = (__bridge AtmosplayRewardedVideoBridge *)rewardedVideo;
    [internalRewardedVideo loadAd];
}
/// Returns YES if the AtmosplayRewardedVideoBridge is ready to be show.
BOOL AtmosplayRewardedVideoReady(AtmosplayTypeRewardedVideoRef rewardedVideo) {
    AtmosplayRewardedVideoBridge *internalRewardedVideo = (__bridge AtmosplayRewardedVideoBridge *)rewardedVideo;
    return [internalRewardedVideo isReady];
}
/// Shows the AtmosplayRewardedVideoBridge.
void AtmosplayShowRewardedVideo(AtmosplayTypeRewardedVideoRef rewardedVideo) {
    AtmosplayRewardedVideoBridge *internalRewardedVideo = (__bridge AtmosplayRewardedVideoBridge *)rewardedVideo;
    [internalRewardedVideo show];
}
/// Sets AtmosplayRewardedVideoBridge autoload next ad.
void AtmosplaySetRewardedVideoAutoload(AtmosplayTypeRewardedVideoRef rewardedVideo, BOOL autoload) {
    AtmosplayRewardedVideoBridge *internalRewardedVideo = (__bridge AtmosplayRewardedVideoBridge *)rewardedVideo;
    [internalRewardedVideo setAutoload:autoload];
}
/// Sets AtmosplayRewardedVideoBridge channel id.
void AtmosplaySetRewardedVideoChannelId(AtmosplayTypeRewardedVideoRef rewardedVideo, const char *channelId) {
    AtmosplayRewardedVideoBridge *internalRewardedVideo = (__bridge AtmosplayRewardedVideoBridge *)rewardedVideo;
    [internalRewardedVideo setChannelId:AtmosplayAdsStringFromUTF8String(channelId)];
}

#pragma mark - Banner method
AtmosplayTypeBannerRef InitAtmosplayBannerAd(AtmosplayTypeBannerClientRef *bannerRef, const char *adAppID, const char *adUnitID) {
    
    AtmosplayBannerBridge *banner = [[AtmosplayBannerBridge alloc] initWithBannerClientReference:bannerRef 
                                                                                         adAppId:AtmosplayAdsStringFromUTF8String(adAppID) 
                                                                                        adUnitId:AtmosplayAdsStringFromUTF8String(adUnitID)];
    
    AtmosplayObjectCache *cache = [AtmosplayObjectCache sharedInstance];
    [cache.references setObject:banner forKey:[banner atmosplayAds_referenceKey]];
    return (__bridge AtmosplayTypeBannerRef)banner;
}

void ShowBannerView(AtmosplayTypeBannerRef bannerView){
    AtmosplayBannerBridge *internalBanner = (__bridge AtmosplayBannerBridge *)bannerView;
    [internalBanner showBannerView];
}
void HideBannerView(AtmosplayTypeBannerRef bannerView){
    AtmosplayBannerBridge *internalBanner = (__bridge AtmosplayBannerBridge *)bannerView;
    [internalBanner hideBannerView];
}
void DestroyBannerView(AtmosplayTypeBannerRef bannerView){
    AtmosplayBannerBridge *internalBanner = (__bridge AtmosplayBannerBridge *)bannerView;
    [internalBanner removeBannerView];
}
void SetBannerAdSize(AtmosplayTypeBannerRef bannerView,AtmosplayBannerSize bannerSize){
    AtmosplayBannerBridge *internalBanner = (__bridge AtmosplayBannerBridge *)bannerView;
    [internalBanner setBannerAdSize:bannerSize];
}

void SetBannerPosition(AtmosplayTypeBannerRef bannerView,int position){
    AtmosplayBannerBridge *internalBanner = (__bridge AtmosplayBannerBridge *)bannerView;
    [internalBanner setBannerPosition:position];
}
void SetBannerChannelID(AtmosplayTypeBannerRef bannerView,const char *channelID){
    AtmosplayBannerBridge *internalBanner = (__bridge AtmosplayBannerBridge *)bannerView;
    [internalBanner setChannelID:AtmosplayAdsStringFromUTF8String(channelID)];
}
/// Sets the banner callback methods to be invoked during banner ad events.
void SetBannerCallbacks(
                        AtmosplayTypeBannerRef bannerView,
                        AtmosplayBannerDidReceiveAdCallback adReceivedCallback,
                        AtmosplayBannerDidFailToReceiveAdWithErrorCallback adFailedCallback,
                        AtmosplayBannerDidClickCallback adClickedCallback){
    AtmosplayBannerBridge *internalBanner = (__bridge AtmosplayBannerBridge *)bannerView;
    // set banner property
    internalBanner.adReceivedCallback = adReceivedCallback;
    internalBanner.adFailedCallback = adFailedCallback;
    internalBanner.adClickedCallback = adClickedCallback;
}
void RequestBannerAd( AtmosplayTypeBannerRef bannerView){
    AtmosplayBannerBridge *internalBanner = (__bridge AtmosplayBannerBridge *)bannerView;
    [internalBanner loadAd];
}

#pragma mark - FloatAd Method
/// Creates a AtmosplayFloatAdBridge and return its reference
AtmosplayTypeFloatAdRef AtmosplayAdsCreateFloatAd(AtmosplayTypeFloatAdClientRef *floatAdClient,
                                                 const char *adAppID,
                                                 const char *adUnitID,
                                                 BOOL autoLoad) {
    AtmosplayFloatAdBridge *floatAd = [[AtmosplayFloatAdBridge alloc] initWithFloatAdClientReference:floatAdClient
                                                                                             adAppId:AtmosplayAdsStringFromUTF8String(adAppID)
                                                                                            adUnitId:AtmosplayAdsStringFromUTF8String(adUnitID)
                                                                                            autoLoad:autoLoad];
    AtmosplayObjectCache *cache = [AtmosplayObjectCache sharedInstance];
    [cache.references setObject:floatAd forKey:[floatAd atmosplayAds_referenceKey]];
    return (__bridge AtmosplayTypeFloatAdRef)floatAd;
}

/// Sets the float ad callback methods to be invoked during ad events.
void AtmosplayAdsSetFloatAdCallbacks(
        AtmosplayTypeFloatAdRef floatAd,
        AtmosplayFloatAdDidReceiveAdCallback adDidReceivedCallback,
        AtmosplayFloatAdDidFailToReceiveAdWithErrorCallback adDidFailedCallback,
        AtmosplayFloatAdDidStartedCallback adDidStartedCallback,
        AtmosplayFloatAdDidClickCallback adDidClickedCallback,
        AtmosplayFloatAdDidFinishedCallback adDidCompletedCallback,
        AtmosplayFloatAdDidClosedCallback adDidClosedCallback,
        AtmosplayFloatAdDidRewardedCallback adDidRewardedCallback) {
    AtmosplayFloatAdBridge *internalFloatAd = (__bridge AtmosplayFloatAdBridge *)floatAd;
    internalFloatAd.adDidReceivedCallback = adDidReceivedCallback;
    internalFloatAd.adDidFailedCallback = adDidFailedCallback;
    internalFloatAd.adDidStartedCallback = adDidStartedCallback;
    internalFloatAd.adDidClickedCallback = adDidClickedCallback;
    internalFloatAd.adDidCompletedCallback = adDidCompletedCallback;
    internalFloatAd.adDidClosedCallback = adDidClosedCallback;
    internalFloatAd.adDidRewardedCallback = adDidRewardedCallback;
}

BOOL floatAdIsReady(AtmosplayTypeFloatAdRef floatAd) {
    AtmosplayFloatAdBridge *internalFloatAd = (__bridge AtmosplayFloatAdBridge *)floatAd;
    return [internalFloatAd isReady];
}

void showFloatAd(AtmosplayTypeFloatAdRef floatAd, int x, int y, int width) {
    AtmosplayFloatAdBridge *internalFloatAd = (__bridge AtmosplayFloatAdBridge *)floatAd;
    [internalFloatAd showFloatAdWith:x y:y width:width];
}

void updateFloatAdPosition(AtmosplayTypeFloatAdRef floatAd, int x, int y, int width) {
    AtmosplayFloatAdBridge *internalFloatAd = (__bridge AtmosplayFloatAdBridge *)floatAd;
    [internalFloatAd resetFloatAdFrameWith:x y:y width:width];
}

void setFloatAdChannelId(AtmosplayTypeFloatAdRef floatAd, const char *channelId) {
    AtmosplayFloatAdBridge *internalFloatAd = (__bridge AtmosplayFloatAdBridge *)floatAd;
    [internalFloatAd setChannelID:AtmosplayAdsStringFromUTF8String(channelId)];
}

void hiddenFloatAd(AtmosplayTypeFloatAdRef floatAd) {
    AtmosplayFloatAdBridge *internalFloatAd = (__bridge AtmosplayFloatAdBridge *)floatAd;
    [internalFloatAd hiddenFloatAd];
}

void showFloatAdAgainAfterHiding(AtmosplayTypeFloatAdRef floatAd) {
    AtmosplayFloatAdBridge *internalFloatAd = (__bridge AtmosplayFloatAdBridge *)floatAd;
    [internalFloatAd showAgainAfterHiding];
}

void destroyFloatAd(AtmosplayTypeFloatAdRef floatAd) {
    AtmosplayFloatAdBridge *internalFloatAd = (__bridge AtmosplayFloatAdBridge *)floatAd;
    [internalFloatAd destroyFloatAd];
}

#pragma mark - Other methods
/// Removes an object from the cache.
void AtmosplayAdsRelease(AtmosplayTypeRef ref) {
    if (ref) {
        AtmosplayObjectCache *cache = [AtmosplayObjectCache sharedInstance];
        [cache.references removeObjectForKey:[(__bridge NSObject *)ref atmosplayAds_referenceKey]];
    }
}
