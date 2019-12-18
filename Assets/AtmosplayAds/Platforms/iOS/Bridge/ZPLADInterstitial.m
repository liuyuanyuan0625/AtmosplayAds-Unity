//
// Copyright (C) 2016 Google, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#import "ZPLADInterstitial.h"
#import <UIKit/UIKit.h>

@interface ZPLADInterstitial() <PlayableAdsDelegate>
@end

@implementation ZPLADInterstitial

- (id)initWithInterstitialClientReference:(ZPLADTypeInterstitialClientRef *)interstitialClient
                                  adAppId:(NSString *)adAppId
                                 adUnitId:(NSString *)adUnitId {
    self = [super init];
    if (self){
        _interstitialClient = interstitialClient;
        _interstitial = [[PlayableAds alloc] initWithAdUnitID:adUnitId appID:adAppId];
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
        [self.interstitial present];
    } else {
        NSLog(@"ZPLAYAdsPlugin: Interstitial is not ready to be shown.");
    }
}

- (void)setAutoload: (BOOL)autoload {
    self.interstitial.autoLoad = autoload;
}

- (void)setChannelId: (NSString *)channelId {
    self.interstitial.channelId = channelId;
}

#pragma mark ZPLYAds PlayableAdsDelegate implementation

/// Tells the delegate that succeeded to load ad.
- (void)playableAdsDidLoad:(PlayableAds *)ads {
    if (self.adReceivedCallback) {
        self.adReceivedCallback(self.interstitialClient);
    }
}

/// Tells the delegate that failed to load ad.
- (void)playableAds:(PlayableAds *)ads didFailToLoadWithError:(NSError *)error {
    if (self.adFailedCallback) {
        NSString *errorMsg = [NSString stringWithFormat:@"Failed to receive ad with error: %@", error];
        self.adFailedCallback(self.interstitialClient, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

/// Tells the delegate that user starts playing the ad.
- (void)playableAdsDidStartPlaying:(PlayableAds *)ads {
    if (self.videoDidStartCallback) {
        self.videoDidStartCallback(self.interstitialClient);
    }
}

/// Tells the delegate that the ad is being fully played.
- (void)playableAdsDidEndPlaying:(PlayableAds *)ads {
    if (self.videoDidCompleteCallback) {
        self.videoDidCompleteCallback(self.interstitialClient);
    }
}

/// Tells the delegate that the ad did animate off the screen.
- (void)playableAdsDidDismissScreen:(PlayableAds *)ads {
    
    if (self.adDidCloseCallback) {
        self.adDidCloseCallback(self.interstitialClient);
    }
}

/// Tells the delegate that the ad is clicked
- (void)playableAdsDidClick:(PlayableAds *)ads {
    if (self.adClickedCallback) {
        self.adClickedCallback(self.interstitialClient);
    }
}

@end
