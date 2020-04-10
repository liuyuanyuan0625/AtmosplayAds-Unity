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
@property(nonatomic, assign) AtmosplayFloatAdDidReceiveAdCallback adReceivedCallback;
/// The ad failed callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidFailToReceiveAdWithErrorCallback adFailedCallback;
/// The ad clicked callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidClickCallback adClickedCallback;
/// The ad started callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidStartedCallback adStartedCallback;
/// The ad finished callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidFinishedCallback adFinishedCallback;
/// The ad rewarded callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidRewardedCallback adRewardedCallback;
/// The ad closed callback into Unity.
@property(nonatomic, assign) AtmosplayFloatAdDidClosedCallback adClosedCallback;

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
