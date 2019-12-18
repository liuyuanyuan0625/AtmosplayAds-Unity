#if UNITY_ANDROID
using System;
using UnityEngine;

using ZPLAYAds.Api;
using ZPLAYAds.Common;

namespace ZPLAYAds.Android
{
    public class BannerClient : AndroidJavaProxy, IBannerClient
    {
        AndroidJavaObject androidBanner;
        // Ad event fired when the banner ad has been received.
        public event EventHandler<EventArgs> OnAdLoaded = delegate { };
        // Ad event fired when the banner ad has failed to load.
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad = delegate { };
        // Ad event fired when the banner ad is clicked
        public event EventHandler<EventArgs> OnAdClicked = delegate { };

        public BannerClient() : base(Utils.UnityBannerAdListenerClassName)
        {
            Debug.Log("===> BannerClickent");

            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            androidBanner = new AndroidJavaObject(Utils.BannerClassName, activity, this);
        }

        #region InterstitialAdClient implementation

        // Creates a banner view and adds it to the view hierarchy.
        public void CreateBannerView(string adAppId, string adUnitId, BannerViewOptions options)
        {
            Debug.Log("===> CreateBannerView: " + adAppId + ", " + adUnitId);
            androidBanner.Call("createBanner", adAppId, adUnitId, options.channelID, options.bannerSize.ToString(), options.adPosition.ToString());
        }
        // Requests a new ad for the banner view.
        public void LoadAd()
        {
            Debug.Log("LoadAd");
            androidBanner.Call("loadAd");
        }

        // Shows the banner view on the screen.
        public void ShowBannerView()
        {
            Debug.Log("ShowBannerView");
            androidBanner.Call("showBannerView");
        }

        // Hides the banner view from the screen.
        public void HideBannerView()
        {
            Debug.Log("HideBannerView");
            androidBanner.Call("hideBannerView");
        }

        // Destroys a banner view.
        public void DestroyBannerView()
        {
            androidBanner.Call("destroyBannerView");
        }
        #endregion

        #region Callback from UnityInterstitialAdListener
        void onAdLoaded()
        {
            Debug.Log("unity: onAdLoaded");
            if (OnAdLoaded != null)
            {
                OnAdLoaded(this, EventArgs.Empty);
            }
        }

        void onAdFailed(String errorReason)
        {
            Debug.Log("unity: onAdFailed: " + errorReason);
            if (OnAdFailedToLoad != null)
            {
                AdFailedEventArgs args = new AdFailedEventArgs()
                {
                    Message = errorReason
                };
                OnAdFailedToLoad(this, args);
            }
        }

        void onAdClicked()
        {
            Debug.Log("unity: onAdClicked: ");
            if (OnAdClicked != null)
            {
                OnAdClicked(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}

#endif