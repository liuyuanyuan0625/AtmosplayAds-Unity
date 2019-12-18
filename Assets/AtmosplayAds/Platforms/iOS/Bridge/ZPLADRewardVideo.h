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

@interface ZPLADRewardVideo : NSObject
/// Initializes a ZPLADRewardVideo
- (id)initWithRewardVideoClientReference:(ZPLADTypeRewardVideoClientRef *)interstitialClient
                                 adAppId:(NSString *)adAppId
                                adUnitId:(NSString *)adUnitId;

/// The reward video ad.
@property(nonatomic, strong) PlayableAds *rewardVideo;

/// A reference to the Unity reward video client.
@property(nonatomic, assign) ZPLADTypeRewardVideoClientRef *rewardVideoClient;

/// The ad received callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoDidReceivedAdCallback adReceivedCallback;

/// The ad failed callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoDidFailToReceiveAdWithErrorCallback adFailedCallback;

/// The ad started playing callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoVideoDidStartPlayingCallback videoDidStartCallback;

/// The ad "INSTALL" button is clicked callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoDidClickCallback adClickedCallback;

/// The user was rewarded callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoDidRewardCallback adRewardCallback;

/// The ad was closed callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoVideoDidCloseCallback adDidCloseCallback;

/// The ad did complete callback into Unity.
@property(nonatomic, assign) ZPLADRewardVideoDidCompleteCallback videoDidCompleteCallback;

- (void)loadAd;

- (BOOL)isReady;

- (void)show;

- (void)setAutoload: (BOOL)autoload;

- (void)setChannelId: (NSString *)channelId;

@end
