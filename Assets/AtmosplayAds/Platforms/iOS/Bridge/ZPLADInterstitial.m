#import "ZPLADInterstitial.h"
#import <UIKit/UIKit.h>

@interface ZPLADInterstitial() <AtmosplayInterstitialDelegate>
@end

@implementation ZPLADInterstitial
- (id)initWithInterstitialClientReference:(ZPLADTypeInterstitialClientRef *)interstitialClient
                                  adAppId:(NSString *)adAppId
                                 adUnitId:(NSString *)adUnitId {
    self = [super init];
    if (self){
        _interstitialClient = interstitialClient;
        _interstitial = [[AtmosplayInterstitial alloc] initWithAppID:adAppId adUnitID:adUnitId ];
        _interstitial.delegate = self;
    }
    return self;
}

- (void)dealloc {
    _interstitial.delegate = nil;
}

- (void)loadAd {
    [self.interstitial loadAd];
}

- (BOOL)isReady {
    return self.interstitial.isReady;
}

- (void)show {
    if(self.interstitial.isReady){
        [self.interstitial showInterstitialWithViewController:nil];
    } else {
        NSLog(@"AtmosplayAdsPlugin: Interstitial is not ready to be show.");
    }
}

- (void)setAutoload: (BOOL)autoload {
    self.interstitial.autoLoad = autoload;
}

- (void)setChannelId: (NSString *)channelId {
    self.interstitial.channelId = channelId;
}

#pragma mark - AtmosplayInterstitialDelegate
/// Tells the delegate that succeeded to load ad.
- (void)atmosplayInterstitialDidLoad:(AtmosplayInterstitial *)ads {
    if (self.adReceivedCallback) {
        self.adReceivedCallback(self.interstitialClient);
    }
}
/// Tells the delegate that failed to load ad.
- (void)atmosplayInterstitial:(AtmosplayInterstitial *)ads didFailToLoadWithError:(NSError *)error {
    if (self.adFailedCallback) {
        NSString *errorMsg = [NSString stringWithFormat:@"Failed to receive ad with error: %@", error];
        self.adFailedCallback(self.interstitialClient, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}
/// Tells the delegate that user starts playing the ad.
- (void)atmosplayInterstitialDidStartPlaying:(AtmosplayInterstitial *)ads {
    if (self.videoDidStartCallback) {
        self.videoDidStartCallback(self.interstitialClient);
    }
}
/// Tells the delegate that the ad is being fully played.
- (void)atmosplayInterstitialDidEndPlaying:(AtmosplayInterstitial *)ads {
    if (self.videoDidCompleteCallback) {
        self.videoDidCompleteCallback(self.interstitialClient);
    }
}
/// Tells the delegate that the ad did animate off the screen.
- (void)atmosplayInterstitialDidDismissScreen:(AtmosplayInterstitial *)ads {   
    if (self.adDidCloseCallback) {
        self.adDidCloseCallback(self.interstitialClient);
    }
}
/// Tells the delegate that the ad is clicked
- (void)atmosplayInterstitialDidClick:(AtmosplayInterstitial *)ads {
    if (self.adClickedCallback) {
        self.adClickedCallback(self.interstitialClient);
    }
}
@end
