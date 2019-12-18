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

#import <Foundation/Foundation.h>
#import <PlayableAds/PlayableAds.h>
#import "ZPLADTypes.h"

/// A wrapper around ZPLADInterstitial. Includes the ability to create ZPLADInterstitial objects,
/// load them with ads, show them, and listen for ad events.
@interface ZPLADInterstitial : NSObject

/// Initializes a ZPLADInterstitial
- (id)initWithInterstitialClientReference:(ZPLADTypeInterstitialClientRef *)interstitialClient
                                  adAppId:(NSString *)adAppId
                                 adUnitId:(NSString *)adUnitId;

/// The interstitial ad.
@property(nonatomic, strong) PlayableAds *interstitial;

/// A reference to the Unity interstitial client.
@property(nonatomic, assign) ZPLADTypeInterstitialClientRef *interstitialClient;

/// The ad received callback into Unity.
@property(nonatomic, assign) ZPLADInterstitialDidReceivedAdCallback adReceivedCallback;

/// The ad failed callback into Unity
@property(nonatomic, assign) ZPLADInterstitialDidFailToReceiveAdWithErrorCallback adFailedCallback;

/// The ad started playing callback into Unity.
@property(nonatomic, assign) ZPLADInterstitialVideoDidStartPlayingCallback videoDidStartCallback;

/// The ad "INSTALL" button is clicked callback into Unity.
@property(nonatomic, assign) ZPLADInterstitiaDidClickCallback adClickedCallback;

/// The ad was closed callback into Unity
@property(nonatomic, assign) ZPLADInterstitialVideoDidCloseCallback adDidCloseCallback;

/// The ad did complete callback into Unity.
@property(nonatomic, assign) ZPLADInterstitialDidCompleteCallback videoDidCompleteCallback;

- (void)loadAd;

- (BOOL)isReady;

- (void)show;

- (void)setAutoload: (BOOL)autoload;

- (void)setChannelId: (NSString *)channelId;

@end

