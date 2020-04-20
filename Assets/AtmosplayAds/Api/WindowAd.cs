using System;
using AtmosplayAds.Common;
using UnityEngine;

namespace AtmosplayAds.Api
{
    public class WindowAd
    {
        static readonly object objLock = new object();

        IWindowAdClient client;

        // Creates WindowAd instance.
        public WindowAd(string adAppId, string adUnitId, GameObject gameObject, AdOptions adOptions)
        {
            client = AtmosplayAdsClientFactory.BuildWindowAdClient(adAppId, adUnitId, gameObject);

            if (adOptions == null)
            {
                adOptions = new AdOptionsBuilder().build();
            }
            client.SetChannelId(adOptions.mChannelId);

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

            client.OnAdFinished += (sender, args) =>
            {
                if (OnAdFinished != null)
                {
                    OnAdFinished(this, args);
                }
            };

            client.OnAdClosed += (sender, args) =>
            {
                if (OnAdClosed != null)
                {
                    OnAdClosed(this, args);
                }
            };

            client.OnAdFailToShow += (sender, args) =>
            {
                if(OnAdFailToShow != null)
                {
                    OnAdFailToShow(this, args);
                }
            };

        }

        // Ad event fired when the window ad has loaded.
        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the window ad has failed to load.
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        // Ad event fired when the window ad is started.
        public event EventHandler<EventArgs> OnAdStarted;
        // Ad event fired when the window ad has end playing
        public event EventHandler<EventArgs> OnAdFinished;
        // Ad event fired when the window ad is clicked.
        public event EventHandler<EventArgs> OnAdClicked;
        // Ad event fired when the window ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;
        public event EventHandler<EventArgs> OnAdFailToShow;

        // Determines whether the window ad has loaded
        public bool IsReady()
        {
            return client.IsReady();
        }

        // Shows the window ad
        public void Show()
        {
            client.Show();
        }

        [Obsolete("SetChannelId is deprecated, please use AdOptions instead.", true)]
        public void SetChannelId(string channelId)
        {
            client.SetChannelId(channelId);
        }

        // Please set the position of the coordinate point where window ad is displayed
        public void SetPointAndWidth(Transform floatAdRect)
        {
            client.SetPointAndWidth(floatAdRect);
        }

        // Update the position of the coordinate point where window ad is displayed
        public void UpdatePointAndWidth(Transform floatAdRect)
        {
            client.UpdatePointAndWidth(floatAdRect);
        }

        // Hidden window ad view
        public void Hidden()
        {
            client.Hidden();

        }

        // Show again after hiding window ad view
        public void ShowAgainAfterHiding()
        {
            client.ShowAgainAfterHiding();
        }

        // Destroy window ad
        public void Destroy()
        {
            client.Destroy();
        }

        public void SetAngle(int angle)
        {
            client.SetAngle(angle);
        }

        [Obsolete("OnAdVideoCompleted no more supported.", true)]
        public event EventHandler<EventArgs> OnAdVideoCompleted;
    }
}