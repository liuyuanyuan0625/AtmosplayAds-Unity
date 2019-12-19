#if UNITY_ANDROID

using System;
using UnityEngine;

using AtmosplayAds.Api;
using AtmosplayAds.Common;

namespace AtmosplayAds.Android
{
    public class RewardVideoClient : AndroidJavaProxy, IRewardVideoClient
    {
        private AndroidJavaObject androidRewardVideo;
        public event EventHandler<EventArgs> OnAdLoaded = delegate { };
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad = delegate { };
        public event EventHandler<EventArgs> OnAdStarted = delegate { };
        public event EventHandler<EventArgs> OnAdRewarded = delegate { };
        public event EventHandler<EventArgs> OnAdClicked = delegate { };
        public event EventHandler<EventArgs> OnAdVideoFinished = delegate { };
        public event EventHandler<EventArgs> OnAdClosed = delegate { };

        public RewardVideoClient(string appId) : base(Utils.UnityRewardVideoAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            androidRewardVideo = new AndroidJavaObject(Utils.RewardVideoClassName, activity, appId, this);
        }

        #region IRewardVideoAdClient implementation

        public void LoadAd(string adUnitId)
        {
            androidRewardVideo.Call("loadAd", adUnitId);
        }

        public bool IsReady(string adUnitId)
        {
            return androidRewardVideo.Call<bool>("isLoaded", adUnitId);
        }

        public void Show(string adUnitId)
        {
            androidRewardVideo.Call("show", adUnitId);
        }

        public void SetAutoloadNext(bool autoload)
        {
            androidRewardVideo.Call("setAutoloadNext", autoload);
        }

        public void SetChannelId(string channelId){
            androidRewardVideo.Call("setChannelId", channelId);
        }
        #endregion

        #region Callback from UnityRewardVideoAdListener
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

        void onAdRewarded()
        {
            if (OnAdRewarded != null)
            {
                OnAdRewarded(this, EventArgs.Empty);
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