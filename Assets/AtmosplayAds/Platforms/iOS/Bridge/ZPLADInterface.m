#import "ZPLADInterstitial.h"
#import "ZPLADRewardVideo.h"
#import "AtmosplayTypes.h"
#import "ZPLADObjectCache.h"
#import "ZPLADBanner.h"

static NSString *ZPLADStringFromUTF8String(const char *bytes) { return bytes ? @(bytes) : nil; }

/// Creates a ZPLADInterstitial and returns its reference
AtmosplayTypeInterstitialRef ZPLADCreateInterstitial(AtmosplayTypeInterstitialClientRef *interstitialClient,
                                                       const char *adAppID,
                                                       const char *adUnitID) {
    ZPLADInterstitial *interstitial = [[ZPLADInterstitial alloc]
                      initWithInterstitialClientReference:interstitialClient
                                       adAppId: ZPLADStringFromUTF8String(adAppID)
                                       adUnitId: ZPLADStringFromUTF8String(adUnitID)];
    ZPLADObjectCache *cache = [ZPLADObjectCache sharedInstance];
    [cache.references setObject:interstitial forKey:[interstitial atmosplayAds_referenceKey]];
    return (__bridge AtmosplayTypeInterstitialRef)interstitial;
}

/// Sets the interstitial callback methods to be invoked during interstitial ad events.
void ZPLADSetInterstitialAdCallbacks(
        AtmosplayTypeInterstitialClientRef interstitialAd,
        AtmosplayInterstitialDidReceivedAdCallback adReceivedCallback,
        AtmosplayInterstitialDidFailToReceiveAdWithErrorCallback adFailedCallback,
        AtmosplayInterstitialDidStartPlayingCallback videoDidStartCallback,
        AtmosplayInterstitiaDidClickCallback adClickedCallback,
        AtmosplayInterstitialDidCloseCallback videoDidCloseCallback,
        AtmosplayInterstitialDidCompleteCallback adDidCompleteCallback) {
    ZPLADInterstitial *internalInterstitialAd = (__bridge ZPLADInterstitial *)interstitialAd;
    internalInterstitialAd.adReceivedCallback = adReceivedCallback;
    internalInterstitialAd.adFailedCallback = adFailedCallback;
    internalInterstitialAd.videoDidStartCallback = videoDidStartCallback;
    internalInterstitialAd.adClickedCallback = adClickedCallback;
    internalInterstitialAd.adDidCloseCallback = videoDidCloseCallback;
    internalInterstitialAd.videoDidCompleteCallback = adDidCompleteCallback;
}

/// Makes an interstitial ad request.
void ZPLADRequestInterstitial(AtmosplayTypeInterstitialRef interstitial) {    
    ZPLADInterstitial *internalInterstitial = (__bridge ZPLADInterstitial *)interstitial;
    [internalInterstitial loadAd];
}

/// Returns YES if the ZPLADInterstitial is ready to be shown.
BOOL ZPLADInterstitialReady(AtmosplayTypeInterstitialRef interstitial) {
    ZPLADInterstitial *internalInterstitial = (__bridge ZPLADInterstitial *)interstitial;
    return [internalInterstitial isReady];
}

/// Shows the ZPLADInterstitial.
void ZPLADShowInterstitial(AtmosplayTypeInterstitialRef interstitial) {
    ZPLADInterstitial *internalInterstitial = (__bridge ZPLADInterstitial *)interstitial;
    [internalInterstitial show];
}

/// Sets ZPLADInterstitial autoload next ad.
void ZPLADSetInterstitialAutoload(AtmosplayTypeInterstitialRef interstitial, BOOL autoload) {
    ZPLADInterstitial *internalInterstitial = (__bridge ZPLADInterstitial *)interstitial;
    [internalInterstitial setAutoload:autoload];
}

/// Sets ZPLADInterstitial channel id.
void ZPLADSetInterstitialChannelId(AtmosplayTypeInterstitialRef interstitial, const char *channelId) {
    ZPLADInterstitial *internalInterstitial = (__bridge ZPLADInterstitial *)interstitial;
    [internalInterstitial setChannelId:ZPLADStringFromUTF8String(channelId)];
}


/// Creates a ZPLADRewardVideo and returns its reference
AtmosplayTypeRewardedVideoRef ZPLADCreateRewardVideo(AtmosplayTypeRewardedVideoClientRef *rewardVideoClient,
                                                 const char *adAppID,
                                                 const char *adUnitID) {
    ZPLADRewardVideo *rewardVideo = [[ZPLADRewardVideo alloc]
                                       initWithRewardVideoClientReference:rewardVideoClient
                                       adAppId: ZPLADStringFromUTF8String(adAppID)
                                       adUnitId: ZPLADStringFromUTF8String(adUnitID)];
    ZPLADObjectCache *cache = [ZPLADObjectCache sharedInstance];
    [cache.references setObject:rewardVideo forKey:[rewardVideo atmosplayAds_referenceKey]];
    return (__bridge AtmosplayTypeRewardedVideoRef)rewardVideo;
}

/// Sets the interstitial callback methods to be invoked during interstitial ad events.
void ZPLADSetRewardVideoAdCallbacks(
        AtmosplayTypeRewardedVideoClientRef rewardVideoAd,
        AtmosplayRewardedVideoDidReceivedAdCallback adReceivedCallback,
        AtmosplayRewardedVideoDidFailToReceiveAdWithErrorCallback adFailedCallback,
        AtmosplayRewardedVideoDidStartPlayingCallback videoDidStartCallback,
        AtmosplayRewardedVideoDidClickCallback adClickedCallback,
        AtmosplayRewardedVideoDidCloseCallback videoDidCloseCallback,
        AtmosplayRewardedVideoDidRewardCallback adDidRewardCallback,
        AtmosplayRewardedVideoDidCompleteCallback adDidCompleteCallback) {
    ZPLADRewardVideo *internalRewardVideoAd = (__bridge ZPLADRewardVideo *)rewardVideoAd;
    internalRewardVideoAd.adReceivedCallback = adReceivedCallback;
    internalRewardVideoAd.adFailedCallback = adFailedCallback;
    internalRewardVideoAd.videoDidStartCallback = videoDidStartCallback;
    internalRewardVideoAd.adClickedCallback = adClickedCallback;
    internalRewardVideoAd.adRewardCallback = adDidRewardCallback;
    internalRewardVideoAd.adDidCloseCallback = videoDidCloseCallback;
    internalRewardVideoAd.videoDidCompleteCallback = adDidCompleteCallback;
}

/// Makes an reward video ad request.
void ZPLADRequestRewardVideo(AtmosplayTypeRewardedVideoRef rewardVideo) {
    ZPLADRewardVideo *internalRewardVideo = (__bridge ZPLADRewardVideo *)rewardVideo;
    [internalRewardVideo loadAd];
}

/// Returns YES if the ZPLADRewardVideo is ready to be shown.
BOOL ZPLADRewardVideoReady(AtmosplayTypeRewardedVideoRef rewardVideo) {
    ZPLADRewardVideo *internalRewardVideo = (__bridge ZPLADRewardVideo *)rewardVideo;
    return [internalRewardVideo isReady];
}

/// Shows the ZPLADRewardVideo.
void ZPLADShowRewardVideo(AtmosplayTypeRewardedVideoRef rewardVideo) {
    ZPLADRewardVideo *internalRewardVideo = (__bridge ZPLADRewardVideo *)rewardVideo;
    [internalRewardVideo show];
}

/// Sets ZPLADRewardVideo autoload next ad.
void ZPLADSetRewardVideoAutoload(AtmosplayTypeRewardedVideoRef rewardVideo, BOOL autoload) {
    ZPLADRewardVideo *internalRewardVideo = (__bridge ZPLADRewardVideo *)rewardVideo;
    [internalRewardVideo setAutoload:autoload];
}

/// Sets ZPLADRewardVideo channel id.
void ZPLADSetRewardVideoChannelId(AtmosplayTypeRewardedVideoRef rewardVideo, const char *channelId) {
    ZPLADRewardVideo *internalRewardVideo = (__bridge ZPLADRewardVideo *)rewardVideo;
    [internalRewardVideo setChannelId:ZPLADStringFromUTF8String(channelId)];
}

#pragma mark: Banner method
AtmosplayTypeBannerRef InitAtmosplayBannerAd(AtmosplayTypeBannerClientRef *bannerRef, const char *adAppID, const char *adUnitID) {
    
    ZPLADBanner *banner = [[ZPLADBanner alloc] initWithBannerClientReference:bannerRef adAppId:ZPLADStringFromUTF8String(adAppID) adUnitId:ZPLADStringFromUTF8String(adUnitID)];
    
    ZPLADObjectCache *cache = [ZPLADObjectCache sharedInstance];
    [cache.references setObject:banner forKey:[banner atmosplayAds_referenceKey]];
    return (__bridge AtmosplayTypeBannerRef)banner;
}

void ShowBannerView(AtmosplayTypeBannerRef bannerView){
    ZPLADBanner *internalBanner = (__bridge ZPLADBanner *)bannerView;
    [internalBanner showBannerView];
}
void HideBannerView(AtmosplayTypeBannerRef bannerView){
    ZPLADBanner *internalBanner = (__bridge ZPLADBanner *)bannerView;
    [internalBanner hideBannerView];
}
void DestroyBannerView(AtmosplayTypeBannerRef bannerView){
    ZPLADBanner *internalBanner = (__bridge ZPLADBanner *)bannerView;
    [internalBanner removeBannerView];
}
void SetBannerAdSize(AtmosplayTypeBannerRef bannerView,AtmosplayAdsBannerSize bannerSize){
    ZPLADBanner *internalBanner = (__bridge ZPLADBanner *)bannerView;
    [internalBanner setBannerAdSize:bannerSize];
}

void SetBannerPosition(AtmosplayTypeBannerRef bannerView,int position){
    ZPLADBanner *internalBanner = (__bridge ZPLADBanner *)bannerView;
    [internalBanner setBannerPosition:position];
}
void SetBannerChannelID(AtmosplayTypeBannerRef bannerView,const char *channelID){
    ZPLADBanner *internalBanner = (__bridge ZPLADBanner *)bannerView;
    [internalBanner setChannelID:ZPLADStringFromUTF8String(channelID)];
}

/// Sets the banner callback methods to be invoked during banner ad events.
void SetBannerCallbacks(
                        AtmosplayTypeBannerRef bannerView,
                        AtmosplayBannerDidReceiveAdCallback adReceivedCallback,
                        AtmosplayBannerDidFailToReceiveAdWithErrorCallback adFailedCallback,
                        AtmosplayBannerDidClickCallback adClickedCallback){
    ZPLADBanner *internalBanner = (__bridge ZPLADBanner *)bannerView;
    // set banner property
    internalBanner.adReceivedCallback = adReceivedCallback;
    internalBanner.adFailedCallback = adFailedCallback;
    internalBanner.adClickedCallback = adClickedCallback;
}

void RequestBannerAd( AtmosplayTypeBannerRef bannerView){
    ZPLADBanner *internalBanner = (__bridge ZPLADBanner *)bannerView;
    [internalBanner loadAd];
}

#pragma mark - Other methods
/// Removes an object from the cache.
void AtmosplayAdsRelease(AtmosplayTypeRef ref) {
    if (ref) {
        ZPLADObjectCache *cache = [ZPLADObjectCache sharedInstance];
        [cache.references removeObjectForKey:[(__bridge NSObject *)ref atmosplayAds_referenceKey]];
    }
}
