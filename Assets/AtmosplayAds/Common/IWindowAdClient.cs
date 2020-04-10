using System;
using AtmosplayAds.Api;
using UnityEngine;

namespace AtmosplayAds.Common
{
    public interface IWindowAdClient
    {
        event EventHandler<EventArgs> OnAdLoaded;
        event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        event EventHandler<EventArgs> OnAdStarted;
        event EventHandler<EventArgs> OnAdClicked;
        event EventHandler<EventArgs> OnAdFinished;
        event EventHandler<EventArgs> OnAdClosed;


        bool IsReady();

        void Show();

        void SetChannelId(string channelId);

        void SetPointAndWidth(Transform windowAdRect);

        void UpdatePointAndWidth(Transform windowAdRect);

        void Hidden();

        void ShowAgainAfterHiding();

        void Destroy();
    }
}