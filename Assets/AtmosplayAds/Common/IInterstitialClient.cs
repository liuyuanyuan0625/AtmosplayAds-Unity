using System;
using AtmosplayAds.Api;
namespace AtmosplayAds.Common
{
    public interface IInterstitialClient
    {
        event EventHandler<EventArgs> OnAdLoaded;
        event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        event EventHandler<EventArgs> OnAdStarted;
        event EventHandler<EventArgs> OnAdClicked;
        event EventHandler<EventArgs> OnAdVideoFinished;
        event EventHandler<EventArgs> OnAdClosed;

        void LoadAd(string adUnitId);

        bool IsReady(string adUnitId);

        void Show(string adUnitId);

        void SetChannelId(string channelId);

        void SetAutoloadNext(bool autoLoad);
    }
}
