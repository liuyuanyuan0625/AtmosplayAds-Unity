#import "AtmosplayWindowAdBridge.h"

static CGFloat iPhonePlusHeight = 736.0;

@interface AtmosplayWindowAdBridge ()<AtmosplayWindowAdDelegate>
@property (nonatomic, assign) CGFloat scale;
@end

@implementation AtmosplayWindowAdBridge

- (instancetype)initWithWindowAdClientReference:(AtmosplayTypeWindowAdClientRef*)windowAdClientRef
                                        adAppId:(NSString *)adAppId
                                       adUnitId:(NSString *)adUnitId {
    if (self = [super init]) {
        _windowAdClient = windowAdClientRef;
        _windowAd = [[AtmosplayWindowAd alloc] initAndLoadAdWithAppID:adAppId adUnitID:adUnitId];
        _windowAd.delegate = self;
    }
    [self screenScale];
    return self;
}

- (BOOL)isReady {
    return self.windowAd.isReady;
}

- (void)showWindowAdWith:(CGFloat)x
                       y:(CGFloat)y
          transformAngle:(float)angle
                   width:(CGFloat)width {
  x = x/self.scale;
  y = y/self.scale;
  width = width/self.scale;
  if (self.windowAd.isReady) {
    [self.windowAd showWindowAdWith:CGPointMake(x, y) width:width transformAngle:angle rootViewController:UnityGetGLViewController()];
  }
}

- (void)resetWindowAdFrameWith:(CGFloat)x
                             y:(CGFloat)y
                transformAngle:(float)angle
                         width:(CGFloat)width {
  x = x/self.scale;
  y = y/self.scale;
  width = width/self.scale;
  [self.windowAd resetWindowAdFrameWith:CGPointMake(x, y) width:width transfromAngle:angle rootViewController:UnityGetGLViewController()];
}

- (void)pauseVideo {
  [self.windowAd pauseVideo];
}

- (void)resumeVideo {
  [self.windowAd resumeVideo];
}

- (void)hiddenWindowAd {
  [self.windowAd hiddenWindowAd];
}

- (void)showAgainAfterHiding {
  [self.windowAd showAgainAfterHiding];
}

- (void)destroyWindowAd {
  [self.windowAd destroyWindowAd];
  self.windowAd.delegate = nil;
  self.windowAd = nil;
}

- (void)setChannelID:(NSString *)channelID {
  self.windowAd.channelID = channelID;
}

#pragma mark - window ad delegate
/// Tells the delegate that an ad has been successfully loaded.
- (void)atmosplayWindowAdDidLoad:(AtmosplayWindowAd *)windowAd {
  if (self.adDidReceivedCallback) {
    self.adDidReceivedCallback(self.windowAdClient);
  }
}
/// Tells the delegate that a request failed.
- (void)atmosplayWindowAd:(AtmosplayWindowAd *)windowAd DidFailWithError:(NSError *)error {
  if(self.adDidFailedCallback) {
    NSString *errorMsg = [NSString
          stringWithFormat:@"Failed to load ad with error: %@", [error localizedDescription]];
    self.adDidFailedCallback(self.windowAdClient, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
  }
}
/// Tells the delegate that user starts playing the ad.
- (void)atmosplayWindowAdDidStartPlaying:(AtmosplayWindowAd *)windowAd {
  if(self.adDidStartedCallback) {
    self.adDidStartedCallback(self.windowAdClient);
  }
}
/// Tells the delegate that the ad is being fully played.
- (void)atmosplayWindowAdDidEndPlaying:(AtmosplayWindowAd *)windowAd {
  if(self.adDidCompletedCallback) {
    self.adDidCompletedCallback(self.windowAdClient);
  }
}
- (void)atmosplayWindowAdDidFailToPlay:(AtmosplayWindowAd *)windowAd {
  if(self.adDidFailToShowCallback) {
    self.adDidFailToShowCallback(self.windowAdClient);
  }
}
/// Tells the delegate that the ad did animate off the screen.
- (void)atmosplayWindowAdDidDismissScreen:(AtmosplayWindowAd *)windowAd {
  if(self.adDidClosedCallback) {
    self.adDidClosedCallback(self.windowAdClient);
  }
}
/// Tells the delegate that the ad is clicked
- (void)atmosplayWindowAdDidClick:(AtmosplayWindowAd *)windowAd {
  if(self.adDidClickedCallback) {
    self.adDidClickedCallback(self.windowAdClient);
  }
}

- (void)screenScale {
    dispatch_async(dispatch_get_main_queue(), ^{
        CGFloat scale = [UIScreen mainScreen].scale;
        if ([UIScreen mainScreen].bounds.size.height == iPhonePlusHeight) {
            scale = [UIScreen mainScreen].nativeScale; // 6/7/8 plus的实际像素比是2.6。 屏幕宽高414:736  物理像素1080:1920
        }
        NSLog(@"[UIScreen mainScreen].scale = %f,",scale);
        self.scale = scale;
    });
}

@end
