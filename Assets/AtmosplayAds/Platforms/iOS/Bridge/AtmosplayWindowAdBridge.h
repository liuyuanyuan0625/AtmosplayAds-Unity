#import <Foundation/Foundation.h>
#import <AtmosplayAds/AtmosplayWindowAd.h>
#import "AtmosplayTypes.h"

@interface AtmosplayWindowAdBridge : NSObject

- (instancetype)initWithWindowAdClientReference:(AtmosplayTypeFloatAdClientRef*)floatAdClientRef
                                        adAppId:(NSString *)adAppId
                                       adUnitId:(NSString *)adUnitId;

/// A reference to the Unity floatAd client.
@property(nonatomic, assign) AtmosplayTypeWindowAdClientRef *windowAdClient;
/// A AtmosplayFloatAd which contains the ad.
@property(nonatomic, strong) AtmosplayWindowAd *windowAd;
/// The ad received callback into Unity.
@property(nonatomic, assign) AtmosplayWindowAdDidReceiveAdCallback adDidReceivedCallback;
/// The ad failed callback into Unity.
@property(nonatomic, assign) AtmosplayWindowAdDidFailToReceiveAdWithErrorCallback adDidFailedCallback;
/// The ad clicked callback into Unity.
@property(nonatomic, assign) AtmosplayWindowAdDidClickCallback adDidClickedCallback;
/// The ad started callback into Unity.
@property(nonatomic, assign) AtmosplayWindowAdDidStartedCallback adDidStartedCallback;
/// The ad finished callback into Unity.
@property(nonatomic, assign) AtmosplayWindowAdDidFinishedCallback adDidCompletedCallback;
/// The ad rewarded callback into Unity.
@property(nonatomic, assign) AtmosplayWindowAdDidFailToShowCallback adDidFailToShowCallback;
/// The ad closed callback into Unity.
@property(nonatomic, assign) AtmosplayWindowAdDidClosedCallback adDidClosedCallback;

- (void)showWindowAdWith:(CGFloat)x
                       y:(CGFloat)y
          transformAngle:(float)angle
                   width:(CGFloat)width;

- (void)resetWindowAdFrameWith:(CGFloat)x
                             y:(CGFloat)y
                transformAngle:(float)angle
                         width:(CGFloat)width;

- (void)hiddenWindowAd;

- (void)showAgainAfterHiding;

- (void)pauseVideo;

- (void)resumeVideo;

- (void)destroyWindowAd;

- (void)setChannelID:(NSString *)channelID;

- (BOOL)isReady;

@end
