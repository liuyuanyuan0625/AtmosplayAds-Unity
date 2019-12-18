#import <Foundation/Foundation.h>
#import <AtmosplayAds/AtmosplayRewardedVideo.h>
#import "AtmosplayTypes.h"

@interface AtmosplayRewardedVideoBridge : NSObject
/// Initializes a AtmosplayRewardedVideo
- (id)initWithRewardVideoClientReference:(AtmosplayTypeRewardedVideoClientRef *)interstitialClient
                                 adAppId:(NSString *)adAppId
                                adUnitId:(NSString *)adUnitId;

/// The reward video ad.
@property(nonatomic, strong) AtmosplayRewardedVideo *rewardedVideo;
/// A reference to the Unity reward video client.
@property(nonatomic, assign) AtmosplayTypeRewardedVideoClientRef *rewardedVideoClient;
/// The ad received callback into Unity.
@property(nonatomic, assign) AtmosplayRewardedVideoDidReceivedAdCallback adReceivedCallback;
/// The ad failed callback into Unity.
@property(nonatomic, assign) AtmosplayRewardedVideoDidFailToReceiveAdWithErrorCallback adFailedCallback;
/// The ad started playing callback into Unity.
@property(nonatomic, assign) AtmosplayRewardedVideoDidStartPlayingCallback videoDidStartCallback;
/// The ad "INSTALL" button is clicked callback into Unity.
@property(nonatomic, assign) AtmosplayRewardedVideoDidClickCallback adClickedCallback;
/// The user was rewarded callback into Unity.
@property(nonatomic, assign) AtmosplayRewardedVideoDidRewardCallback adRewardCallback;
/// The ad was closed callback into Unity.
@property(nonatomic, assign) AtmosplayRewardedVideoDidCloseCallback adDidCloseCallback;
/// The ad did complete callback into Unity.
@property(nonatomic, assign) AtmosplayRewardedVideoDidCompleteCallback videoDidCompleteCallback;

- (void)loadAd;

- (BOOL)isReady;

- (void)show;

- (void)setAutoload: (BOOL)autoload;

- (void)setChannelId: (NSString *)channelId;

@end
