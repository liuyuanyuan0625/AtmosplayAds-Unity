//
//  ZPLADBanner.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2019/10/30.
//

#import "ZPLADBanner.h"

static BOOL IsOperatingSystemAtLeastVersion(NSInteger majorVersion) {
  NSProcessInfo *processInfo = NSProcessInfo.processInfo;
  if ([processInfo respondsToSelector:@selector(isOperatingSystemAtLeastVersion:)]) {
    // iOS 8+.
    NSOperatingSystemVersion version = {majorVersion};
    return [processInfo isOperatingSystemAtLeastVersion:version];
  } else {
    // pre-iOS 8. App supports iOS 7+, so this process must be running on iOS 7.
    return majorVersion >= 7;
  }
}

@interface ZPLADBanner ()<AtmosplayAdsBannerDelegate>

@property (nonatomic, assign) int bannerPosition;

@end

@implementation ZPLADBanner

- (instancetype)initWithBannerClientReference:(AtmosplayTypeBannerClientRef*)bannerClientRef
 adAppId:(NSString *)adAppId
                                     adUnitId:(NSString *)adUnitId {
    if (self = [super init]) {
        _bannerClient = bannerClientRef;
        _bannerView = [[AtmosplayAdsBanner alloc] initWithAdUnitID:adUnitId appID:adAppId rootViewController:UnityGetGLViewController()];
        _bannerView.delegate = self;
    }
    
    return self;
}

- (void)loadAd {
    [self.bannerView loadAd];
}

/// Makes the YumiBannerView hidden on the screen.
- (void)hideBannerView {
    self.bannerView.hidden = YES;
}

- (void)showBannerView {
    self.bannerView.hidden = NO;
}

- (void)removeBannerView {
    [self.bannerView removeFromSuperview];
    self.bannerView.delegate = nil;
    self.bannerView = nil;
}

- (void)setBannerAdSize:(AtmosplayAdsBannerSize)bannerSize {
    self.bannerView.bannerSize = bannerSize;
}

- (void)setChannelID:(NSString *)channelID {
    self.bannerView.channelID = channelID;
}
//TOP = 0,BOTTOM = 1
- (void)setBannerPosition:(int)postion {
    _bannerPosition = postion;
}

#pragma mark: private method
- (void)positionView:(UIView *)view
        inParentView:(UIView *)parentView {
  CGRect parentBounds = parentView.bounds;
  if (IsOperatingSystemAtLeastVersion(11)) {
    CGRect safeAreaFrame = parentView.safeAreaLayoutGuide.layoutFrame;
    if (!CGSizeEqualToSize(CGSizeZero, safeAreaFrame.size)) {
      parentBounds = safeAreaFrame;
    }
  }
  CGFloat top = CGRectGetMinY(parentBounds) + CGRectGetMidY(view.bounds);
  CGFloat left = CGRectGetMinX(parentBounds) + CGRectGetMidX(view.bounds);

  CGFloat bottom = CGRectGetMaxY(parentBounds) - CGRectGetMidY(view.bounds);
//  CGFloat right = CGRectGetMaxX(parentBounds) - CGRectGetMidX(view.bounds);
  CGFloat centerX = CGRectGetMidX(parentBounds);
//  CGFloat centerY = CGRectGetMidY(parentBounds);

  // If this view is of greater or equal width to the parent view, do not offset
  // to edge of safe area. Eg for smart banners that are still full screen
  // width.
  if (CGRectGetWidth(view.bounds) >= CGRectGetWidth(parentView.bounds)) {
    left = CGRectGetMidX(parentView.bounds);
  }

  // Similarly for height, if view is of custom size which is full screen
  // height, do not offset.
  if (CGRectGetHeight(view.bounds) >= CGRectGetHeight(parentView.bounds)) {
    top = CGRectGetMidY(parentView.bounds);
  }

  CGPoint center = CGPointMake(centerX, top);
   //TOP = 0,BOTTOM = 1
  switch (self.bannerPosition) {
    case 0:
      center = CGPointMake(centerX, top);
      break;
    case 1:
      center = CGPointMake(centerX, bottom);
      break;
    default:
      break;
  }
  view.center = center;
}

#pragma mark: AtmosplayAdsBannerDelegate
/// Tells the delegate that an ad has been successfully loaded.
- (void)atmosplayAdsBannerViewDidLoad:(AtmosplayAdsBanner *)bannerView {
    if (self.bannerView) {
        [self.bannerView removeFromSuperview];
    }
    
    self.bannerView = bannerView;
    /// Align the bannerView in the Unity view bounds.
    UIView *unityView = UnityGetGLViewController().view;
    
    [self positionView:self.bannerView inParentView:unityView];
    
    [unityView addSubview:self.bannerView];
    if (self.adReceivedCallback) {
        self.adReceivedCallback(self.bannerClient);
    }
}

/// Tells the delegate that a request failed.
- (void)atmosplayAdsBannerView:(AtmosplayAdsBanner *)bannerView didFailWithError:(NSError *)error {
    if (self.adFailedCallback) {
      NSString *errorMsg = [NSString
          stringWithFormat:@"Failed to load ad with error: %@", [error localizedDescription]];
      self.adFailedCallback(self.bannerClient, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

/// Tells the delegate that the banner view has been clicked.
- (void)atmosplayAdsBannerViewDidClick:(AtmosplayAdsBanner *)bannerView {
    if (self.adClickedCallback) {
        self.adClickedCallback(self.bannerClient);
    }
}


@end
