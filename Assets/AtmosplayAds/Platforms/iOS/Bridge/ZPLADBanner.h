//
//  ZPLADBanner.h
//  Unity-iPhone
//
//  Created by Michael Tang on 2019/10/30.
//

#import <Foundation/Foundation.h>
#import "ZPLADTypes.h"
#import <PlayableAds/AtmosplayAdsBanner.h>


@interface ZPLADBanner : NSObject

- (instancetype)initWithBannerClientReference:(AtmosplayTypeBannerClientRef*)bannerClientRef
                                      adAppId:(NSString *)adAppId
                                     adUnitId:(NSString *)adUnitId;

/// A reference to the Unity banner client.
@property(nonatomic, assign) AtmosplayTypeBannerClientRef *bannerClient;

/// A AtmosplayAdsBanner which contains the ad.
@property(nonatomic, strong) AtmosplayAdsBanner *bannerView;

/// The ad received callback into Unity.
@property(nonatomic, assign) AtmosplayBannerDidReceiveAdCallback adReceivedCallback;

/// The ad failed callback into Unity.
@property(nonatomic, assign) AtmosplayBannerDidFailToReceiveAdWithErrorCallback adFailedCallback;

/// The ad clicked callback into Unity.
@property(nonatomic, assign) AtmosplayBannerDidClickCallback adClickedCallback;

/// Makes an ad request. Additional targeting options can be supplied with a request object.
- (void)loadAd;

/// Makes the YumiBannerView hidden on the screen.
- (void)hideBannerView;

/// Makes the YumiBannerView visible on the screen.
- (void)showBannerView;
/// Removes the GADBannerView from the view hierarchy.
- (void)removeBannerView;

- (void)setBannerAdSize:(AtmosplayAdsBannerSize)bannerSize;
- (void)setChannelID:(NSString *)channelID;
//TOP = 0,BOTTOM = 1,
- (void)setBannerPosition:(int)postion;

@end
