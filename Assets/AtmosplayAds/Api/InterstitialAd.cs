using System;
using AtmosplayAds.Common;
namespace AtmosplayAds.Api
{
    public class InterstitialAd
    {
        private IInterstitialClient client;

        // Creates InterstitialAd instance.
        public InterstitialAd(string adAppId, string adUnitId, AdOptions adOptions)
        {
            client = ZPLAYAdsClientFactory.BuildInterstitialClient(adAppId, adUnitId);
            if (adOptions == null)
            {
                adOptions = new AdOptionsBuilder().build();
            }
            client.SetChannelId(adOptions.mChannelId);
            client.SetAutoloadNext(adOptions.isAutoLoad);

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

            client.OnAdClosed += (sender, args) =>
            {
                if (OnAdClosed != null)
                {
                    OnAdClosed(this, args);
                }
            };
        }

        // Ad event fired when the interstitial ad has loaded.
        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the interstitial ad has failed to load.
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        // Ad event fired when the interstitial ad is started.
        public event EventHandler<EventArgs> OnAdStarted;
        // Ad event fired when the interstitial ad is clicked.
        public event EventHandler<EventArgs> OnAdClicked;
        // Ad event fired when the interstitial ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;

        // Loads the InterstitialAd.
        public void LoadAd(string adUnitId)
        {
            client.LoadAd(adUnitId);
        }

        // Determines whether the InterstitialAd has loaded.
        public bool IsReady(string adUnitId)
        {
            return client.IsReady(adUnitId);
        }

        // Displays the InterstitialAd.
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

        [Obsolete("OnAdVideoCompleted is no more supported.", true)]
        public event EventHandler<EventArgs> OnAdVideoCompleted;
    }
}
