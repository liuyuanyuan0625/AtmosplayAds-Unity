using System;
using AtmosplayAds.Api;
using UnityEngine;

namespace AtmosplayAds.Common
{
    public interface IFloatAdClient
    {
        event EventHandler<EventArgs> OnAdLoaded;
        event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        event EventHandler<EventArgs> OnAdStarted;
        event EventHandler<EventArgs> OnAdClicked;
        event EventHandler<EventArgs> OnAdRewarded;
        event EventHandler<EventArgs> OnAdVideoFinished;
        event EventHandler<EventArgs> OnAdClosed;

        void LoadAd(string adUnitId);

        bool IsReady(string adUnitId);

        void Show(string adUnitId);

        void SetChannelId(string channelId);

        void SetAutoloadNext(bool autoLoad);

        void SetPointAndWidth(Transform floatAdRect);

        void UpdatePointAndWidth(Transform floatAdRect);

        void Hidden();

        void ShowAgainAfterHiding();

        void Destroy();
    }
}