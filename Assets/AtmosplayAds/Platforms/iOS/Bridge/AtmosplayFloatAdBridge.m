#import "AtmosplayFloatAdBridge.h"

static CGFloat iPhonePlusHeight = 736.0;

@interface AtmosplayFloatAdBridge ()<AtmosplayFloatAdDelegate>
@property (nonatomic, assign) int scale;
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
    [self screenScale];
    return self;
}

- (BOOL)isReady {
    return self.floatAd.isReady;
}

- (void)showFloatAdWith:(CGFloat)x
                      y:(CGFloat)y
                  width:(CGFloat)width {
  x = x/self.scale;
  y = y/self.scale;
  width = width/self.scale;
  if (self.floatAd.isReady) {
    [self.floatAd showFloatAdWith:CGPointMake(x,y) width:width rootViewController:UnityGetGLViewController()];
  }
}

- (void)resetFloatAdFrameWith:(CGFloat)x
                            y:(CGFloat)y
                        width:(CGFloat)width {
  x = x/self.scale;
  y = y/self.scale;
  width = width/self.scale;
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
  if (self.adDidReceivedCallback) {
    self.adDidReceivedCallback(self.floatAdClient);
  }
}
/// Tells the delegate that a request failed.
- (void)atmosplayFloatAd:(AtmosplayFloatAd *)floatAd DidFailWithError:(NSError *)error {
  if(self.adDidFailedCallback) {
    NSString *errorMsg = [NSString
          stringWithFormat:@"Failed to load ad with error: %@", [error localizedDescription]];
    self.adDidFailedCallback(self.floatAdClient, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
  }
}
/// Tells the delegate that the user should be rewarded.
- (void)atmosplayFloatAdDidRewardUser:(AtmosplayFloatAd *)floatAd {
  if(self.adDidRewardedCallback) {
    self.adDidRewardedCallback(self.floatAdClient);
  }
}
/// Tells the delegate that user starts playing the ad.
- (void)atmosplayFloatAdDidStartPlaying:(AtmosplayFloatAd *)floatAd {
  if(self.adDidStartedCallback) {
    self.adDidStartedCallback(self.floatAdClient);
  }
}
/// Tells the delegate that the ad is being fully played.
- (void)atmosplayFloatAdDidEndPlaying:(AtmosplayFloatAd *)floatAd {
  if(self.adDidCompletedCallback) {
    self.adDidCompletedCallback(self.floatAdClient);
  }
}
/// Tells the delegate that the landing page did present on the screen.
- (void)atmosplayFloatAdDidPresentLandingPage:(AtmosplayFloatAd *)floatAd {
}
/// Tells the delegate that the ad did animate off the screen.
- (void)atmosplayFloatAdDidDismissScreen:(AtmosplayFloatAd *)floatAd {
  if(self.adDidClosedCallback) {
    self.adDidClosedCallback(self.floatAdClient);
  }
}
/// Tells the delegate that the ad is clicked
- (void)atmosplayFloatAdDidClick:(AtmosplayFloatAd *)floatAd {
  if(self.adDidClickedCallback) {
    self.adDidClickedCallback(self.floatAdClient);
  }
}

- (void)screenScale {
    dispatch_async(dispatch_get_main_queue(), ^{
        CGFloat scale = [UIScreen mainScreen].scale;
        NSLog(@"[UIScreen mainScreen].scale = %f,",scale);
        if ([UIScreen mainScreen].bounds.size.height == iPhonePlusHeight) {
            scale = 2.6; // 6/7/8 plus的实际像素比是2.6。 屏幕宽高414:736  物理像素1080:1920
        }
        self.scale = scale;
    });
}

@end
