#if UNITY_IOS
using System;
using System.Runtime.InteropServices;

using AtmosplayAds.Common;
using AtmosplayAds.Api;

namespace AtmosplayAds.iOS
{
    public class InterstitialClient : IInterstitialClient
    {
        readonly IntPtr interstitialClientPtr;
        IntPtr interstitialPtr;

        #region Interstitial callback types

        internal delegate void AtmosplayInterstitialDidReceivedAdCallback(IntPtr interstitialClient);

        internal delegate void AtmosplayInterstitialDidFailToReceiveAdWithErrorCallback(IntPtr interstitialClient, string error);

        internal delegate void AtmosplayInterstitialDidStartPlayingCallback(IntPtr interstitialClient);

        internal delegate void AtmosplayInterstitiaDidClickCallback(IntPtr interstitialClient);

        internal delegate void AtmosplayInterstitialDidCloseCallback(IntPtr interstitialClient);

        internal delegate void AtmosplayInterstitialDidCompleteCallback(IntPtr interstitialClient);

        #endregion

        public event EventHandler<EventArgs> OnAdLoaded;
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        public event EventHandler<EventArgs> OnAdStarted;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<EventArgs> OnAdVideoFinished;
        public event EventHandler<EventArgs> OnAdClosed;


        public InterstitialClient(string adAppId, string adUnitId)
        {
            interstitialClientPtr = (IntPtr)GCHandle.Alloc(this);
            InterstitialPtr = Externs.AtmosplayAdsCreateInterstitial(interstitialClientPtr, adAppId, adUnitId);
            Externs.AtmosplayAdsSetInterstitialAdCallbacks(
                InterstitialPtr,
                InterstitialDidReceivedAdCallback,
                InterstitialDidFailToReceiveAdWithErrorCallback,
                InterstitialVideoDidStartPlayingCallback,
                InterstitiaDidClickCallback,
                InterstitialVideoDidCloseCallback,
                InterstitialDidCompleteCallback
            );
        }

        IntPtr InterstitialPtr
        {
            get
            {
                return interstitialPtr;
            }
            set
            {
                Externs.AtmosplayAdsRelease(interstitialPtr);
                interstitialPtr = value;
            }
        }



        #region IInterstitialAdClient implementation

        public void LoadAd(string adUnitId)
        {
            Externs.AtmosplayAdsRequestInterstitial(InterstitialPtr);
        }

        public bool IsReady(string adUnitId)
        {
            return Externs.AtmosplayAdsInterstitialReady(InterstitialPtr);
        }

        public void Show(string adUnitId)
        {
            Externs.AtmosplayAdsShowInterstitial(InterstitialPtr);
        }

        public void SetAutoloadNext(bool autoload)
        {
            Externs.AtmosplayAdsSetInterstitialAutoload(InterstitialPtr, autoload);
        }

        public void SetChannelId(string channelId)
        {
            Externs.ZPLADSetInterstitialChannelId(InterstitialPtr, channelId);
        }

        public void DestroyInterstitial()
        {
            InterstitialPtr = IntPtr.Zero;
        }

        public void Dispose()
        {
            DestroyInterstitial();
            ((GCHandle)interstitialClientPtr).Free();
        }

        ~InterstitialClient()
        {
            Dispose();
        }
        #endregion

        #region Interstitial callback methods

        [MonoPInvokeCallback(typeof(AtmosplayInterstitialDidReceivedAdCallback))]
        static void InterstitialDidReceivedAdCallback(IntPtr interstitialClient)
        {
            InterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdLoaded != null)
            {
                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayInterstitialDidFailToReceiveAdWithErrorCallback))]
        static void InterstitialDidFailToReceiveAdWithErrorCallback(IntPtr interstitialClient, string error)
        {
            InterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdFailedToLoad != null)
            {
                AdFailedEventArgs args = new AdFailedEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayInterstitialDidStartPlayingCallback))]
        static void InterstitialVideoDidStartPlayingCallback(IntPtr interstitialClient)
        {
            InterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdStarted != null)
            {
                client.OnAdStarted(client, EventArgs.Empty);
            }
        }


        [MonoPInvokeCallback(typeof(AtmosplayInterstitiaDidClickCallback))]
        static void InterstitiaDidClickCallback(IntPtr interstitialClient)
        {
            InterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayInterstitialDidCloseCallback))]
        static void InterstitialVideoDidCloseCallback(IntPtr interstitialClient)
        {
            InterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdClosed != null)
            {
                client.OnAdClosed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayInterstitialDidCompleteCallback))]
        static void InterstitialDidCompleteCallback(IntPtr interstitialClient)
        {
            InterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdVideoFinished != null)
            {
                client.OnAdVideoFinished(client, EventArgs.Empty);
            }
        }

        private static InterstitialClient IntPtrToInterstitialClient(IntPtr interstitialClient)
        {
            GCHandle handle = (GCHandle)interstitialClient;
            return handle.Target as InterstitialClient;
        }
        #endregion
    }
}
#endif