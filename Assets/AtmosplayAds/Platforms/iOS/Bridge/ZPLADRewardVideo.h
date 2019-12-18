#import <Foundation/Foundation.h>
#import <AtmosplayAds/AtmosplayRewardedVideo.h>
#import "ZPLADTypes.h"

@interface ZPLADRewardVideo : NSObject
/// Initializes a AtmosplayRewardedVideo
- (id)initWithRewardVideoClientReference:(ZPLADTypeRewardVideoClientRef *)interstitialClient
                                 adAppId:(NSString *)adAppId
                                adUnitId:(NSString *)adUnitId;

/// The reward video ad.
@property(nonatomic, strong) AtmosplayRewardedVideo *rewardedVideo;
/// A reference to the Unity reward video client.
@property(nonatomic, assign) ZPLADTypeRewardVideoClientRef *rewardedVideoClient;
/// The ad received callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoDidReceivedAdCallback adReceivedCallback;
/// The ad failed callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoDidFailToReceiveAdWithErrorCallback adFailedCallback;
/// The ad started playing callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoVideoDidStartPlayingCallback videoDidStartCallback;
/// The ad "INSTALL" button is clicked callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoDidClickCallback adClickedCallback;
/// The user was rewarded callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoDidRewardCallback adRewardCallback;
/// The ad was closed callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoVideoDidCloseCallback adDidCloseCallback;
/// The ad did complete callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoDidCompleteCallback videoDidCompleteCallback;

- (void)loadAd;

- (BOOL)isReady;

- (void)show;

- (void)setAutoload: (BOOL)autoload;

- (void)setChannelId: (NSString *)channelId;

@end
