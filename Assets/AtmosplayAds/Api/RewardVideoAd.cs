using System;
using ZPLAYAds.Common;

namespace ZPLAYAds.Api
{
    public class RewardVideoAd
    {
        static readonly object objLock = new object();

        IRewardVideoClient client;

        // Creates RewardVideoAd instance.
        public RewardVideoAd(string adAppId, string adUnitId, AdOptions adOptions)
        {
            client = ZPLAYAdsClientFactory.BuildRewardVideoClient(adAppId, adUnitId);

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

        // Ad event fired when the rewarded video ad has loaded.
        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the rewarded video ad has failed to load.
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        // Ad event fired when the rewarded video ad is started.
        public event EventHandler<EventArgs> OnAdStarted;
        // Ad event fired when the rewarded video ad has rewarded the user.
        public event EventHandler<EventArgs> OnAdRewarded;
        // Ad event fired when the rewarded video ad is clicked.
        public event EventHandler<EventArgs> OnAdClicked;
        // Ad event fired when the rewarded video ad is closed.
        public event EventHandler<EventArgs> OnAdClosed;

        // Loads a new reward video
        public void LoadAd(string adUnitId)
        {
            client.LoadAd(adUnitId);
        }

        // Determines whether the reward video has loaded
        public bool IsReady(string adUnitId)
        {
            return client.IsReady(adUnitId);
        }

        // Shows the reward video
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

        [Obsolete("OnAdVideoCompleted no more supported.", true)]
        public event EventHandler<EventArgs> OnAdVideoCompleted;
    }
}