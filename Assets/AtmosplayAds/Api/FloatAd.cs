using System;
using AtmosplayAds.Common;
using UnityEngine;

namespace AtmosplayAds.Api
{
    public class FloatAd
    {
        static readonly object objLock = new object();

        IFloatAdClient client;

        // Creates FloatAd instance.
        public FloatAd(string adAppId, string adUnitId,GameObject gameObject, AdOptions adOptions)
        {
            client = AtmosplayAdsClientFactory.BuildFloatAdClient(adAppId, adUnitId, gameObject);

            if (adOptions == null)
            {
                adOptions = new AdOptionsBuilder().build();
            }
            client.SetChannelId(adOptions.mChannelId);
            client.SetAutoloadNext(adOptions.isAutoLoad);
            client.LoadAd(adUnitId);

            client.OnAdLoaded += (sender, args) =>
            {
                if (OnAdLoaded != null)
                {
                    OnAdLoaded(this, args);
                }
            };

            client.OnAdFailedToLoad += (sender, args) =>
            {
                if (OnAdFailedToLoad != null)
                {
                    OnAdFailedToLoad(this, args);
                }
            };

            client.OnAdStarted += (sender, args) =>
            {
                if (OnAdStarted != null)
                {
                    OnAdStarted(this, args);
                }
            };

            client.OnAdClicked += (sender, args) =>
            {
                if (OnAdClicked != null)
                {
                    OnAdClicked(this, args);
                }
            };

            client.OnAdRewarded += (sender, args) =>
            {
                if (OnAdRewarded != null)
                {
                    OnAdRewarded(this, args);
                }
            };

            client.OnAdClosed += (sender, args) =>
            {
                if (OnAdClosed != null)
                {
                    OnAdClosed(this, args);
                }
            };

        }

        // Ad event fired when the float ad has loaded.
        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the float ad has failed to load.
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        // Ad event fired when the float ad is started.
        public event EventHandler<EventArgs> OnAdStarted;
        // Ad event fired when the float ad has rewarded the user.
        public event EventHandler<EventArgs> OnAdRewarded;
        // Ad event fired when the float ad is clicked.
        public event EventHandler<EventArgs> OnAdClicked;
        // Ad event fired when the float ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;


        // Determines whether the float ad has loaded
        public bool IsReady(string adUnitId)
        {
            return client.IsReady(adUnitId);
        }

        // Shows the float ad
        public void Show(string adUnitId)
        {
            client.Show(adUnitId);
        }

        [Obsolete("SetAutoloadNext is deprecated, please use AdOptions instead.", true)]
        public void SetAutoloadNext(bool autoload)
        {
            client.SetAutoloadNext(autoload);
        }

        [Obsolete("SetChannelId is deprecated, please use AdOptions instead.", true)]
        public void SetChannelId(string channelId)
        {
            client.SetChannelId(channelId);
        }

        // Please set the position of the coordinate point where float ad is displayed
        public void SetPointAndWidth(Transform floatAdRect)
        {
            client.SetPointAndWidth(floatAdRect);
        }

        // Update the position of the coordinate point where float ad is displayed
        public void UpdatePointAndWidth(Transform floatAdRect)
        {
            client.UpdatePointAndWidth(floatAdRect);
        }

        // Hidden float ad view
        public void Hidden()
        {
            client.Hidden();

        }

        // Show again after hiding float ad view
        public void ShowAgainAfterHiding()
        {
            client.ShowAgainAfterHiding();
        }

        // Destroy float ad
        public void Destroy()
        {
            client.Destroy();
        }

        [Obsolete("OnAdVideoCompleted no more supported.", true)]
        public event EventHandler<EventArgs> OnAdVideoCompleted;
    }
}