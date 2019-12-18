#import "ZPLADRewardVideo.h"

@interface ZPLADRewardVideo() <AtmosplayRewardedVideoDelegate>
@end

@implementation ZPLADRewardVideo
- (id)initWithRewardVideoClientReference:(ZPLADTypeRewardVideoClientRef *)interstitialClient
                                 adAppId:(NSString *)adAppId
                                adUnitId:(NSString *)adUnitId {
    self = [super init];
    if (self){
        _rewardedVideoClient = interstitialClient;
        _rewardedVideo = [[AtmosplayRewardedVideo alloc] initWithAppID:adAppId adUnitID:adUnitId];
        _rewardedVideo.delegate = self;
    }
    return self;
}

- (void)dealloc {
    _rewardedVideo.delegate = nil;
}

- (void)loadAd {
    [self.rewardedVideo loadAd];
}

- (BOOL)isReady {
    return self.rewardedVideo.isReady;
}

- (void)show {
    if(self.rewardedVideo.isReady){
        [self.rewardedVideo present];
    } else {
        NSLog(@"AtmosplayAdsPlugin: Interstitial is not ready to be shown.");
    }
}

- (void)setAutoload: (BOOL)autoload {
    self.rewardedVideo.autoLoad = autoload;
}

- (void)setChannelId: (NSString *)channelId {
    self.rewardedVideo.channelId = channelId;
}

#pragma mark - AtmosplayRewardedVideoDelegate
/// Tells the delegate that succeeded to load ad.
- (void)playableAdsDidLoad:(PlayableAds *)ads {
    if (self.adReceivedCallback) {
        self.adReceivedCallback(self.rewardedVideoClient);
    }
}
/// Tells the delegate that failed to load ad.
- (void)playableAds:(PlayableAds *)ads didFailToLoadWithError:(NSError *)error {
    if (self.adFailedCallback) {
        NSString *errorMsg = [NSString stringWithFormat:@"Failed to receive ad with error: %@", error];
        self.adFailedCallback(self.rewardedVideoClient, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}
/// Tells the delegate that user starts playing the ad.
- (void)playableAdsDidStartPlaying:(PlayableAds *)ads {
    if (self.videoDidStartCallback) {
        self.videoDidStartCallback(self.rewardedVideoClient);
    }
}
/// Tells the delegate that the ad is being fully played.
- (void)playableAdsDidEndPlaying:(PlayableAds *)ads {
    if (self.videoDidCompleteCallback) {
        self.videoDidCompleteCallback(self.rewardedVideoClient);
    }
}
/// Tells the delegate that the ad did animate off the screen.
- (void)playableAdsDidDismissScreen:(PlayableAds *)ads {
   
    if (self.adDidCloseCallback) {
        self.adDidCloseCallback(self.rewardedVideoClient);
    }
}
/// Tells the delegate that the ad is clicked
- (void)playableAdsDidClick:(PlayableAds *)ads {
    if (self.adClickedCallback) {
        self.adClickedCallback(self.rewardedVideoClient);
    }
}
/// Tells the delegate that the USER should be rewarded.
- (void)playableAdsDidRewardUser:(PlayableAds *)ads {
    if (self.adRewardCallback) {
        self.adRewardCallback(self.rewardedVideoClient);
    }
}

@end
