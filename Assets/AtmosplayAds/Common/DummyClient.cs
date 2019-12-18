using System;
using System.Reflection;

using UnityEngine;

using AtmosplayAds.Api;

namespace AtmosplayAds.Common
{
    public class DummyClient : IInterstitialClient, IRewardVideoClient
    {
        public DummyClient()
        {
            Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
        }

        // Disable warnings for unused dummy ad events.
#pragma warning disable 67
        public event EventHandler<EventArgs> OnAdLoaded;
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        public event EventHandler<EventArgs> OnAdStarted;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<EventArgs> OnAdRewarded;
        public event EventHandler<EventArgs> OnAdVideoFinished;
        public event EventHandler<EventArgs> OnAdClosed;
#pragma warning restore 67

        public void LoadAd(string adUnitId)
        {
            Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
        }

        public bool IsReady(string adUnitId)
        {
            Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
            return true;
        }

        public void Show(string adUnitId)
        {
            Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
        }

        public void SetAutoloadNext(bool autoload)
        {
            Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
        }

        public void SetChannelId(string channelId)
        {
            Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
        }
    }
}
