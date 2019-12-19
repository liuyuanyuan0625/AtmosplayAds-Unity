- [ZPLAYAds for Unity](#zplayads-for-unity)
  - [Get Started](#get-started)
  - [Download the ZPLAYAds Unity plugin](#download-the-zplayads-unity-plugin)
  - [Import the Mobile Ads Unity plugin](#import-the-mobile-ads-unity-plugin)
  - [Include the ZPLAYAds SDK](#include-the-zplayads-sdk)
    - [Deploy to iOS](#deploy-to-ios)
    - [Deploy to Android](#deploy-to-android)
  - [Select an Ad Format](#select-an-ad-format)
    - [Interstitial](#interstitial)
      - [Initialize and Request Interstitial](#initialize-and-request-interstitial)
      - [Request Interstitial](#request-interstitial)
      - [Determine If Interstitial Has Been Loaded](#determine-if-interstitial-has-been-loaded)
      - [Present Interstitial](#present-interstitial)
    - [Rewarded Video](#rewarded-video)
      - [Initialize and Request Rewarded Video](#initialize-and-request-rewarded-video)
      - [Request Rewarded Video](#request-rewarded-video)
      - [Determine If Rewarded Video Has Been Loaded](#determine-if-rewarded-video-has-been-loaded)
      - [Present Rewarded Video](#present-rewarded-video)
      - [Initialize Banner](#initialize-banner)
      - [Request Banner](#request-banner)
      - [Hide Banner](#hide-banner)
      - [Show Banner](#show-banner)
      - [Destroy Banner](#destroy-banner)
  - [Test](#test)

# ZPLAYAds for Unity

## Get Started

1.Guide Intended for

This guide is intended for publishers who want to integrate ZPLAYAds SDK to theri Unity apps. 

2.Prerequisites

- Use Unity 5.6 or higher
- To deploy to iOS

   Xcode 7.0 or higher

   iOS 8.0 or higher

   [CocoaPods](https://guides.cocoapods.org/using/getting-started.html)

- To deploy Android

  Target Android API level 14 or higher

3.[Access Demo](../../Assets)   

## Download the ZPLAYAds Unity plugin

The ZPLAYAds Unity plugin enables Unity developers to easily serve ZPLAY Ads on Android and iOS apps without having to write Java or Objective-C code. The plugin provides a C# interface for requesting ads that is used by C# scripts in your Unity project. You can download the [ZPLAYAds Unity plugin](https://github.com/zplayads/ZPLAYAds-unity/raw/master/source/ZPLAYAds.unitypackage) or look at its code on GitHub.([codes of Android](../android-library/app/src/main/java/com/zplay/adsunity)、[codes of iOS](../../Assets/Plugins/iOS)、[view source](../../Assets/ZPLAYAds))

## Import the Mobile Ads Unity plugin

Open your project in the Unity editor. Select **Assets> Import Package> Custom Package** and find the ZPLAYAds.unitypackage file you downloaded.

![img](resources/image-import-custom-package.png)

Make sure all of the files are selected and click **Import**.

![img](resources/image-select-package.png)

## Include the ZPLAYAds SDK

The ZPLAYAds Unity plugin is distributed with the [Unity Play Services Resolver library](https://github.com/googlesamples/unity-jar-resolver). This library is intended for use by any Unity plugin that requires access to Android specific libraries (e.g., AARs) or iOS CocoaPods. It provides Unity plugins the ability to declare dependencies, which are then automatically resolved and copied into your Unity project.

Follow the steps listed below to ensure your project includes the ZPLAYAds SDK.

### Deploy to iOS

No additional steps are required to include the ZPLAYAds SDK into the Unity project.

After building, open **xcworkspace** project. 

**Note: iOS dependencies are identified using CocoaPods. CocoaPods is run as a post build process step.**

### Deploy to Android

In the Unity editor, select **Assets> Play Services Resolver> Android Resolver>Force Resolve**. The Unity Play Services Resolver library will copy the declared dependencies into the **Assets/Plugins/Android** directory of your Unity app.

![img](./resources/image-play-services-resolver.png)

Note: The Google Mobile Ads Unity plugin dependencies are listed in **Assets/ZPLAYAds/Editor/ZPLAYAdsDependencies.xml**

## Select an Ad Format

The ZPLAYAds SDK is now included in your Unity app when deploying to either the Android or iOS platform. You're now ready to implement an ad. ZPLAYAds offers a number of different ad formats, so you can choose the one that best fits your user experience needs.

### Interstitial

#### Initialize and Request Interstitial

```C#
using ZPLAYAds.Api;
using ZPLAYAds.Common;
public class AtmosplayAdsDemoScript : MonoBehaviour
{
#if UNITY_ANDROID
  const string ZPLAYADS_APP_ID = "YOUR_ZPLAYAds_APP_ID_ANDROID";
  const string ZPLAYADS_UNIT_ID_INTERSTITIAL = "YOUR_ZPLAYAds_UNIT_ID_INTERSTITIAL_ANDROID";
#elif UNITY_IOS
  const string ZPLAYADS_APP_ID = "YOUR_ZPLAYAds_APP_ID_IOS";
  const string ZPLAYADS_UNIT_ID_INTERSTITIAL = "YOUR_ZPLAYAds_UNIT_ID_INTERSTITIAL_IOS";
#else
  const string ZPLAYADS_APP_ID = "unexpected_platform";
  const string ZPLAYADS_UNIT_ID_INTERSTITIAL = "unexpected_platform";
#endif

  InterstitialAd interstitial;

  void Start() 
  {
    AdOptions adOptions = new AdOptionsBuilder()
      .SetChannelId("")
      .SetAutoLoadNext(true)
      .build();

    interstitial = new InterstitialAd(ZPLAYADS_APP_ID, ZPLAYADS_UNIT_ID_INTERSTITIAL, adOptions);
    interstitial.OnAdLoaded += HandleInterstitialLoaded;
    interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
    interstitial.OnAdStarted += HandleInterstitialStart;
    interstitial.OnAdClicked += HandleInterstitialClicked;
    interstitial.OnAdClosed += HandleInterstitialClosed;
  }
  
  #region Interstitial callback handlers
  public void HandleInterstitialLoaded(object sender, EventArgs args)
  {
    print("===> HandleInterstitialLoaded event received");
  }
  public void HandleInterstitialFailedToLoad(object sender, AdFailedEventArgs args)
  {
    print("===> HandleInterstitialFailedToLoad event received with message: " + args.Message);
  }
  public void HandleInterstitialStart(object sender, EventArgs args)
  {
    print("===> HandleInterstitialStart event received.");
  }
  public void HandleInterstitialClicked(object sender, EventArgs args)
  {
    print("===> HandleInterstitialClicked event received.");
  }
  public void HandleInterstitialClosed(object sender, EventArgs args)
  {
    print("===> HandleInterstitialClosed event received.");
  }
  #endregion
}
```

#### Request Interstitial

If you open autoload mode, after first request, the SDK will request the next ad automatically when an ad has been completed or request failed. 

```C#
interstitial.LoadAd(ZPLAYADS_UNIT_ID_INTERSTITIAL);
```

#### Determine If Interstitial Has Been Loaded

```c#
interstitial.IsReady(ZPLAYADS_UNIT_ID_INTERSTITIAL)
```

#### Present Interstitial

We recommend you call ```interstitial.IsReady(ZPLAYADS_UNIT_ID_INTERSTITIAL)``` before showing an interstitial to determine if the ad is ready.

```C#
if(interstitial.IsReady(ZPLAYADS_UNIT_ID_INTERSTITIAL))
{
  interstitial.Show(ZPLAYADS_UNIT_ID_INTERSTITIAL);
}
```

### Rewarded Video

#### Initialize and Request Rewarded Video

```C#
using ZPLAYAds.Api;
using ZPLAYAds.Common;
public class AtmosplayAdsDemoScript : MonoBehaviour
{
#if UNITY_ANDROID
  const string ZPLAYADS_APP_ID = "YOUR_ZPLAYAds_APP_ID_ANDROID";
  const string ZPLAYADS_UNIT_ID_REWARD_VIDEO = "YOUR_ZPLAYAds_UNIT_ID_REWARD_VIDEO_ANDROID";
#elif UNITY_IOS
  const string ZPLAYADS_APP_ID = "YOUR_ZPLAYAds_APP_ID_IOS";
  const string ZPLAYADS_UNIT_ID_REWARD_VIDEO = "YOUR_ZPLAYAds_UNIT_ID_REWARD_VIDEO_IOS";
#else
  const string ZPLAYADS_APP_ID = "unexpected_platform";
  const string ZPLAYADS_UNIT_ID_REWARD_VIDEO = "unexpected_platform";
#endif

  RewardVideoAd rewardVideo;

  void Start() 
  {
    AdOptions adOptions = new AdOptionsBuilder()
      .SetChannelId("")
      .SetAutoLoadNext(true)
      .build();

    rewardVideo = new RewardVideoAd(ZPLAYADS_APP_ID, ZPLAYADS_UNIT_ID_REWARD_VIDEO, adOptions);
    rewardVideo.OnAdLoaded += HandleRewardVideoLoaded;
    rewardVideo.OnAdFailedToLoad += HandleRewardVideoFailedToLoad;
    rewardVideo.OnAdStarted += HandleRewardVideoStart;
    rewardVideo.OnAdClicked += HandleRewardVideoClicked;
    rewardVideo.OnAdRewarded += HandleRewardVideoRewarded;
    rewardVideo.OnAdClosed += HandleRewardVideoClosed;
  }

  #region RewardVideo callback handlers
  public void HandleRewardVideoLoaded(object sender, EventArgs args)
  {
    print("===> HandleRewardVideoLoaded event received");
  }
  public void HandleRewardVideoFailedToLoad(object sender, AdFailedEventArgs args)
  {
    print("===> HandleRewardVideoFailedToLoad event received with message: " + args.Message);
  }
  public void HandleRewardVideoStart(object sender, EventArgs args)
  {
    print("===> HandleRewardVideoStart event received.");
  }
  public void HandleRewardVideoClicked(object sender, EventArgs args)
  {
    print("===> HandleRewardVideoClicked event received.");
  }
  public void HandleRewardVideoRewarded(object sender, EventArgs args)
  {
    print("===> HandleRewardVideoRewarded event received.");
  }
  public void HandleRewardVideoClosed(object sender, EventArgs args)
  {
    print("===> HandleRewardVideoClosed event received.");
  }
  #endregion
}
```

#### Request Rewarded Video
If you open autoload mode, after first request, the SDK will request the next ad automatically when an ad has been completed or request failed. 

```C#
rewardVideo.LoadAd(ZPLAYADS_UNIT_ID_REWARD_VIDEO);
```

#### Determine If Rewarded Video Has Been Loaded

```c#
rewardVideo.IsReady(ZPLAYADS_UNIT_ID_REWARD_VIDEO)
```

#### Present Rewarded Video

```c#
if(rewardVideo.IsReady(ZPLAYADS_UNIT_ID_REWARD_VIDEO))
{
  rewardVideo.Show(ZPLAYADS_UNIT_ID_REWARD_VIDEO);
} 
```
#### Initialize Banner
```C#
using ZPLAYAds.Api;
using ZPLAYAds.Common;
public class AtmosplayAdsDemoScript : MonoBehaviour
{
#if UNITY_ANDROID
  const string ZPLAYADS_APP_ID = "YOUR_ZPLAYAds_APP_ID_ANDROID";
  const string ZPLAYADS_UNIT_ID_BANNER = "YOUR_ZPLAYAds_UNIT_ID_BANNER_ANDROID";
#elif UNITY_IOS
  const string ZPLAYADS_APP_ID = "YOUR_ZPLAYAds_APP_ID_IOS";
  const string ZPLAYADS_UNIT_ID_BANNER = "YOUR_ZPLAYAds_UNIT_ID_BANNER_IOS";
#else
  const string ZPLAYADS_APP_ID = "unexpected_platform";
  const string ZPLAYADS_UNIT_ID_BANNER = "unexpected_platform";
#endif

BannerView bannerView;

  void Start() 
  {
    BannerViewOptions bannerOptions = new BannerViewOptionsBuilder()
            .setAdPosition(AdPosition.BOTTOM)
            .setChannelID(GlobleSettings.GetChannelId)
            .setBannerSize(BannerAdSize.BANNER_AD_SIZE_320x50)
            .Build();

    bannerView = new BannerView(ZPLAYADS_APP_ID, ZPLAYADS_UNIT_ID_BANNER, bannerOptions);
    bannerView.OnAdLoaded += HandleBannerAdLoaded;
    bannerView.OnAdFailedToLoad += HandleBannerAdFailedToLoad;
    bannerView.OnAdClicked += HandleBannerClicked;
  }
  #region Banner callback handlers

    public void HandleBannerAdLoaded(object sender, EventArgs args)
    {
        print("===> HandleBannerAdLoaded event received");
    }

    public void HandleBannerAdFailedToLoad(object sender, AdFailedEventArgs args)
    {
        print("===> HandleBannerAdFailedToLoad event received with message: " + args.Message);
    }

     public void HandleBannerClicked(object sender, EventArgs args)
    {
        print("===> HandleBannerClicked event.");
    }

    #endregion
```
#### Request Banner
**the ZPLAYAds SDK respects any refresh rate you specified in the ZPLAYAds UI**
```c#
if (bannerView != null)
{
    bannerView.LoadAd();
}
```
#### Hide Banner
```c#
if (bannerView != null)
{
    bannerView.Hide();
}
```
#### Show Banner
```c#
if (bannerView != null)
{
    bannerView.Show();
}
```
#### Destroy Banner
```c#
if (bannerView != null)
{
    bannerView.Destroy();
    bannerView = null;
}
```

## Test
You are available to use the following ID when testing your App (no charge). Please replace them with the ID you applied in ZPLAY Ads when you publish your App.

| OS      | Format         | App_ID                               | Ad_Unit_ID                           |
| ------- | -------------- | ------------------------------------ | ------------------------------------ |
| iOS     | Banner         | A650AB0D-7BFC-2A81-3066-D3170947C3DA | A49521F3-339D-994F-FC80-F9C4170AA0CF |
| iOS     | Rewarded Video | A650AB0D-7BFC-2A81-3066-D3170947C3DA | BAE5DAAC-04A2-2591-D5B0-38FA846E45E7 |
| iOS     | Interstitial   | A650AB0D-7BFC-2A81-3066-D3170947C3DA | 0868EBC0-7768-40CA-4226-F9924221C8EB |
| Android | Banner         | 5C5419C7-A2DE-88BC-A311-C3E7A646F6AF | F22F347B-3D57-0C70-0B13-EFCFDF402EBA |
| Android | Rewarded Video | 5C5419C7-A2DE-88BC-A311-C3E7A646F6AF | 3FBEFA05-3A8B-2122-24C7-A87D0BC9FEEC |
| Android | Interstitial   | 5C5419C7-A2DE-88BC-A311-C3E7A646F6AF | 19393189-C4EB-3886-60B9-13B39407064E |
