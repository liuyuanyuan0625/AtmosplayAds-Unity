#import "AtmosplayRewardedVideoBridge.h"

@interface AtmosplayRewardedVideoBridge() <AtmosplayRewardedVideoDelegate>
@end

@implementation AtmosplayRewardedVideoBridge
- (id)initWithRewardVideoClientReference:(AtmosplayTypeRewardedVideoClientRef *)interstitialClient
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
- (void)atmosplayRewardedVideoDidLoad:(AtmosplayRewardedVideo *)ads {
    if (self.adReceivedCallback) {
        self.adReceivedCallback(self.rewardedVideoClient);
    }
}
/// Tells the delegate that failed to load ad.
- (void)atmosplayRewardedVideo:(AtmosplayRewardedVideo *)ads didFailToLoadWithError:(NSError *)error {
    if (self.adFailedCallback) {
        NSString *errorMsg = [NSString stringWithFormat:@"Failed to receive ad with error: %@", error];
        self.adFailedCallback(self.rewardedVideoClient, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}
/// Tells the delegate that user starts playing the ad.
- (void)atmosplayRewardedVideoDidStartPlaying:(AtmosplayRewardedVideo *)ads {
    if (self.videoDidStartCallback) {
        self.videoDidStartCallback(self.rewardedVideoClient);
    }
}
/// Tells the delegate that the ad is being fully played.
- (void)atmosplayRewardedVideoDidEndPlaying:(AtmosplayRewardedVideo *)ads {
    if (self.videoDidCompleteCallback) {
        self.videoDidCompleteCallback(self.rewardedVideoClient);
    }
}
/// Tells the delegate that the ad did animate off the screen.
- (void)atmosplayRewardedVideoDidDismissScreen:(AtmosplayRewardedVideo *)ads {
   
    if (self.adDidCloseCallback) {
        self.adDidCloseCallback(self.rewardedVideoClient);
    }
}
/// Tells the delegate that the ad is clicked
- (void)atmosplayRewardedVideoDidClick:(AtmosplayRewardedVideo *)ads {
    if (self.adClickedCallback) {
        self.adClickedCallback(self.rewardedVideoClient);
    }
}
/// Tells the delegate that the user should be rewarded.
- (void)atmosplayRewardedVideoDidReceiveReward:(AtmosplayRewardedVideo *)ads {
    if (self.adRewardCallback) {
        self.adRewardCallback(self.rewardedVideoClient);
    }
}

@end
