#import <Foundation/Foundation.h>
#import <AtmosplayAds/AtmosplayFloatAd.h>

@interface AtmosplayFloatAdBridge : NSObject

- (instancetype)initWithFloatAdClientReference:(AtmosplayTypeFloatAdClientRef*)floatAdClientRef
                                       adAppId:(NSString *)adAppId
                                      adUnitId:(NSString *)adUnitId
                                      autoLoad:(BOOL)autoLoad;

/// A reference to the Unity floatAd client.
@property(nonatomic, assign) AtmosplayTypeFloatAdClientRef *floatAdClient;
/// A AtmosplayFloatAd which contains the ad.
@property(nonatomic, strong) AtmosplayFloatAd *floatAd;
/// The ad received callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidReceiveAdCallback adDidReceivedCallback;
/// The ad failed callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidFailToReceiveAdWithErrorCallback adDidFailedCallback;
/// The ad clicked callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidClickCallback adDidClickedCallback;
/// The ad started callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidStartedCallback adDidStartedCallback;
/// The ad finished callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidFinishedCallback adDidCompletedCallback;
/// The ad rewarded callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidRewardedCallback adDidRewardedCallback;
/// The ad closed callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidClosedCallback adDidClosedCallback;

- (void)showFloatAdWith:(CGFloat)x
                      y:(CGFloat)y
                  width:(CGFloat)width;

- (void)resetFloatAdFrameWith:(CGFloat)x
                            y:(CGFloat)y
                        width:(CGFloat)width;

- (void)hiddenFloatAd;

- (void)showAgainAfterHiding;

- (void)destroyFloatAd;

- (void)setChannelID:(NSString *)channelID;

- (BOOL)isReady;

@end
