#if UNITY_ANDROID
using System;
using UnityEngine;

using AtmosplayAds.Api;
using AtmosplayAds.Common;
namespace AtmosplayAds.Android
{
    public class InterstitialClient : AndroidJavaProxy, IInterstitialClient
    {
        AndroidJavaObject androidInterstitial;
        public event EventHandler<EventArgs> OnAdLoaded = delegate { };
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad = delegate { };
        public event EventHandler<EventArgs> OnAdStarted = delegate { };
        public event EventHandler<EventArgs> OnAdClicked = delegate { };
        public event EventHandler<EventArgs> OnAdVideoFinished = delegate { };
        public event EventHandler<EventArgs> OnAdClosed = delegate { };

        public InterstitialClient(string appId) : base(Utils.UnityInterstitialAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            androidInterstitial = new AndroidJavaObject(Utils.InterstitialClassName, activity, appId, this);
        }

        #region InterstitialAdClient implementation

        public void LoadAd(string adUnitId)
        {
            androidInterstitial.Call("loadAd", adUnitId);
        }

        public bool IsReady(string adUnitId)
        {
            return androidInterstitial.Call<bool>("isLoaded", adUnitId);
        }

        public void Show(string adUnitId)
        {
            androidInterstitial.Call("show", adUnitId);
        }

        public void SetAutoloadNext(bool autoload)
        {
            androidInterstitial.Call("setAutoloadNext", autoload);
        }

        public void SetChannelId(string channelId)
        {
            androidInterstitial.Call("setChannelId", channelId);
        }
        #endregion

        #region Callback from UnityInterstitialAdListener
        void onAdLoaded()
        {
            if (OnAdLoaded != null)
            {
                OnAdLoaded(this, EventArgs.Empty);
            }
        }

        void onAdFailed(String errorReason)
        {
            if (OnAdFailedToLoad != null)
            {
                AdFailedEventArgs args = new AdFailedEventArgs()
                {
                    Message = errorReason
                };
                OnAdFailedToLoad(this, args);
            }
        }

        void onAdStarted()
        {
            if (OnAdStarted != null)
            {
                OnAdStarted(this, EventArgs.Empty);
            }
        }

        void onAdClicked()
        {
            if (OnAdClicked != null)
            {
                OnAdClicked(this, EventArgs.Empty);
            }
        }

        void onAdVideoCompleted()
        {
            if (OnAdVideoFinished != null)
            {
                OnAdVideoFinished(this, EventArgs.Empty);
            }
        }

        void onAdCompleted()
        {
            if (OnAdClosed != null)
            {
                OnAdClosed(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}

#endif