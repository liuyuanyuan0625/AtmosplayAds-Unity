using System;
using UnityEngine;
using AtmosplayAds.Api;
using AtmosplayAds.Common;
using AtmosplayAds;
using UnityEngine.SceneManagement;

public class AtmosplayAdsDemoScript : MonoBehaviour
{
    RewardVideoAd rewardVideo;
    InterstitialAd interstitial;
    BannerView bannerView;

    void Start()
    {
        AdOptions adOptions = new AdOptionsBuilder()
            .SetChannelId(GlobleSettings.GetChannelId)
            .SetAutoLoadNext(GlobleSettings.IsAutoload)
            .build();

        rewardVideo = new RewardVideoAd(GlobleSettings.GetAppID, GlobleSettings.GetRewardVideoUnitID, adOptions);
        rewardVideo.OnAdLoaded += HandleRewardVideoLoaded;
        rewardVideo.OnAdFailedToLoad += HandleRewardVideoFailedToLoad;
        rewardVideo.OnAdStarted += HandleRewardVideoStart;
        rewardVideo.OnAdClicked += HandleRewardVideoClicked;
        rewardVideo.OnAdRewarded += HandleRewardVideoRewarded;
        rewardVideo.OnAdClosed += HandleRewardVideoClosed;

        interstitial = new InterstitialAd(GlobleSettings.GetAppID, GlobleSettings.GetInterstitialUnitID, adOptions);
        interstitial.OnAdLoaded += HandleInterstitialLoaded;
        interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.OnAdStarted += HandleInterstitialStart;
        interstitial.OnAdClicked += HandleInterstitialClicked;
        interstitial.OnAdClosed += HandleInterstitialClosed;

        BannerViewOptions bannerOptions = new BannerViewOptionsBuilder()
            .setAdPosition(AdPosition.BOTTOM)
            .setChannelID(GlobleSettings.GetChannelId)
            .setBannerSize(BannerAdSize.BANNER_AD_SIZE_320x50)
            .Build();

        bannerView = new BannerView(GlobleSettings.GetAppID, GlobleSettings.GetBannerUnitID, bannerOptions);
        bannerView.OnAdLoaded += HandleBannerAdLoaded;
        bannerView.OnAdFailedToLoad += HandleBannerAdFailedToLoad;
        bannerView.OnAdClicked += HandleBannerClicked;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnGUI()
    {
        GUI.skin.button.fontSize = (int)(0.034f * Screen.width);
        float buttonWidth = 0.35f * Screen.width;
        float buttonHeight = 0.1f * Screen.height;
        float columnOnePosition = 0.1f * Screen.width;
        float columnTwoPosition = 0.55f * Screen.width;

        Rect requestRewardRect = new Rect(columnOnePosition, 0.05f * Screen.height, buttonWidth, buttonHeight);
        if (GUI.Button(requestRewardRect, "Request\nRewardVideo"))
        {
            RequestRewaredVideo(GlobleSettings.GetRewardVideoUnitID);
        }

        Rect showRewardRect = new Rect(columnTwoPosition, 0.05f * Screen.height, buttonWidth, buttonHeight);
        if (GUI.Button(showRewardRect, "Show\nRewarded Video"))
        {
            ShowRewardedVideo(GlobleSettings.GetRewardVideoUnitID);
        }

        Rect requestInterstitialRect = new Rect(columnOnePosition, 0.2f * Screen.height, buttonWidth, buttonHeight);
        if (GUI.Button(requestInterstitialRect, "Request\nInterstitital"))
        {
            RequestInterstitital(GlobleSettings.GetInterstitialUnitID);
        }

        Rect showInterstitialRect = new Rect(columnTwoPosition, 0.2f * Screen.height, buttonWidth, buttonHeight);
        if (GUI.Button(showInterstitialRect, "Show\nInterstitital"))
        {
            ShowInterstitial(GlobleSettings.GetInterstitialUnitID);
        }
        // banner
        Rect requestBannerRect = new Rect(columnOnePosition, 0.35f * Screen.height, buttonWidth, buttonHeight);
        if (GUI.Button(requestBannerRect, "Request Banner"))
        {
            if (bannerView != null)
            {
                bannerView.LoadAd();
            }
        }
        Rect hiddenBannerlRect = new Rect(columnTwoPosition, 0.35f * Screen.height, buttonWidth, buttonHeight);
        if (GUI.Button(hiddenBannerlRect, "Hide Banenr"))
        {
            if (bannerView != null)
            {
                bannerView.Hide();
            }
        }
        Rect showBannerlRect = new Rect(columnOnePosition, 0.5f * Screen.height, buttonWidth, buttonHeight);
        if (GUI.Button(showBannerlRect, "Show Banenr"))
        {
            if (bannerView != null)
            {
                bannerView.Show();
            }
        }

        Rect showFloatAdSceneRect = new Rect(columnTwoPosition, 0.5f * Screen.height, buttonWidth, buttonHeight);
        if (GUI.Button(showFloatAdSceneRect, "Show FloatAd"))
        {
            SceneManager.LoadScene("FloatAdScene");
        }

        Rect showWindowAdSceneRect = new Rect(columnOnePosition, 0.65f * Screen.height, buttonWidth, buttonHeight);
        if (GUI.Button(showWindowAdSceneRect, "Show WindowAd"))
        {
            SceneManager.LoadScene("WindowAdScene");
        }
    }

    void RequestRewaredVideo(string adUnitId)
    {
        rewardVideo.LoadAd(adUnitId);
        print("atmosplay---request rewarded video");
    }

    void ShowRewardedVideo(string adUnitId)
    {
        if (rewardVideo.IsReady(adUnitId))
        {
            rewardVideo.Show(adUnitId);
            print("atmosplay---show rewarded video");
        }
        else
        {
            print("atmosplay---RewardedVideo ad not ready");
        }
    }

    void RequestInterstitital(string adUnitId)
    {
        interstitial.LoadAd(adUnitId);
        print("atmosplay---request interstitial");
    }

    void ShowInterstitial(string adUnitId)
    {
        if (interstitial.IsReady(adUnitId))
        {
            interstitial.Show(adUnitId);
            print("atmosplay---show interstitial");
        }
        else
        {
            print("atmosplay---interstitial not ready");
        }
    }

    #region RewardedVideo callback handlers
    public void HandleRewardVideoLoaded(object sender, EventArgs args)
    {
        print("atmosplay---HandleRewardVideoLoaded");
    }

    public void HandleRewardVideoFailedToLoad(object sender, AdFailedEventArgs args)
    {
        print("atmosplay---HandleRewardVideoFailedToLoadWithError:" + args.Message);
    }

    public void HandleRewardVideoStart(object sender, EventArgs args)
    {
        print("atmosplay---HandleRewardVideoStart");
    }

    public void HandleRewardVideoClicked(object sender, EventArgs args)
    {
        print("atmosplay---HandleRewardVideoClicked");
    }


    public void HandleRewardVideoRewarded(object sender, EventArgs args)
    {
        print("atmosplay---HandleRewardVideoRewarded");
    }


    public void HandleRewardVideoClosed(object sender, EventArgs args)
    {
        print("atmosplay---HandleRewardVideoClosed");
    }

    #endregion


    #region Interstitial callback handlers
    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        print("atmosplay---HandleInterstitialLoaded");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedEventArgs args)
    {
        print("atmosplay---HandleInterstitialFailedToLoadWithError:" + args.Message);
    }

    public void HandleInterstitialStart(object sender, EventArgs args)
    {
        print("atmosplay---HandleInterstitialStart");
    }

    public void HandleInterstitialClicked(object sender, EventArgs args)
    {
        print("atmosplay---HandleInterstitialClicked");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        print("atmosplay---HandleInterstitialClosed");
    }

    #endregion
    #region Banner callback handlers
    public void HandleBannerAdLoaded(object sender, EventArgs args)
    {
        print("atmosplay---HandleBannerAdLoaded");
    }

    public void HandleBannerAdFailedToLoad(object sender, AdFailedEventArgs args)
    {
        print("atmosplay---HandleBannerAdFailedToLoadWithError:" + args.Message);
    }

     public void HandleBannerClicked(object sender, EventArgs args)
    {
        print("atmosplay---HandleBannerClicked");
    }

    #endregion
}
