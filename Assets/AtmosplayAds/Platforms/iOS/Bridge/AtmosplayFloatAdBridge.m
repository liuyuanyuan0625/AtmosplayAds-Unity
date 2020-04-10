#import "AtmosplayFloatAdBridge.h"

@interface AtmosplayFloatAdBridge ()<AtmosplayFloatAdDelegate>

@end

@implementation AtmosplayFloatAdBridge

- (instancetype)initWithFloatAdClientReference:(AtmosplayTypeFloatAdClientRef*)floatAdClientRef
                                       adAppId:(NSString *)adAppId
                                      adUnitId:(NSString *)adUnitId
                                      autoLoad:(BOOL)autoLoad {
    if (self = [super init]) {
        _floatAdClient = floatAdClientRef;
        _floatAd = [[AtmosplayFloatAd alloc] initAndLoadAdWithAppID:adAppId adUnitID:adUnitId autoLoad:autoLoad];
        _floatAd.delegate = self;
    }
    
    return self;
}

- (BOOL)isReady {
    return self.floatAd.isReady;
}

- (void)showFloatAdWith:(CGFloat)x
                      y:(CGFloat)y
                  width:(CGFloat)width {
  if (self.floatAd.isReady) {
    [self.floatAd showFloatAdWith:CGPointMake(x,y) width:width rootViewController:UnityGetGLViewController()];
  }
}

- (void)resetFloatAdFrameWith:(CGFloat)x
                            y:(CGFloat)y
                        width:(CGFloat)width {
  [self.floatAd resetFloatAdFrameWith:CGPointMake(x, y) width:width rootViewController:UnityGetGLViewController()];
}

- (void)hiddenFloatAd {
  [self.floatAd hiddenFloatAd];
}

- (void)showAgainAfterHiding {
  [self.floatAd showAgainAfterHiding];
}

- (void)destroyFloatAd {
  [self.floatAd destroyFloatAd];
  self.floatAd.delegate = nil;
  self.floatAd = nil;
}

- (void)setChannelID:(NSString *)channelID {
  self.floatAd.channelID = channelID;
}

#pragma mark - AtmosplayFloatAdDelegate
/// Tells the delegate that an ad has been successfully loaded.
- (void)atmosplayFloatAdDidLoad:(AtmosplayFloatAd *)floatAd {
  if (self.adReceivedCallback) {
    self.adReceivedCallback(self.floatAdClient);
  }
}
/// Tells the delegate that a request failed.
- (void)atmosplayFloatAd:(AtmosplayFloatAd *)floatAd DidFailWithError:(NSError *)error {
  if(self.adFailedCallback) {
    NSString *errorMsg = [NSString
          stringWithFormat:@"Failed to load ad with error: %@", [error localizedDescription]];
    self.adFailedCallback(self.floatAdClient, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
  }
}
/// Tells the delegate that the user should be rewarded.
- (void)atmosplayFloatAdDidRewardUser:(AtmosplayFloatAd *)floatAd {
  if(self.adRewardedCallback) {
    self.adRewardedCallback(self.floatAdClient);
  }
}
/// Tells the delegate that user starts playing the ad.
- (void)atmosplayFloatAdDidStartPlaying:(AtmosplayFloatAd *)floatAd {
  if(self.adStartedCallback) {
    self.adStartedCallback(self.floatAdClient);
  }
}
/// Tells the delegate that the ad is being fully played.
- (void)atmosplayFloatAdDidEndPlaying:(AtmosplayFloatAd *)floatAd {
  if(self.adFinishedCallback) {
    self.adFinishedCallback(self.floatAdClient);
  }
}
/// Tells the delegate that the landing page did present on the screen.
- (void)atmosplayFloatAdDidPresentLandingPage:(AtmosplayFloatAd *)floatAd {

}
/// Tells the delegate that the ad did animate off the screen.
- (void)atmosplayFloatAdDidDismissScreen:(AtmosplayFloatAd *)floatAd {
  if(self.adClosedCallback) {
    self.adClosedCallback(self.floatAdClient);
  }
}
/// Tells the delegate that the ad is clicked
- (void)atmosplayFloatAdDidClick:(AtmosplayFloatAd *)floatAd {
  if(self.adClickedCallback) {
    self.adClickedCallback(self.floatAdClient);
  }
}

@end
