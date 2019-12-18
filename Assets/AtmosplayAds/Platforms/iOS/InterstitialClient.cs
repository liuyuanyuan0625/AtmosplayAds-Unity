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

        internal delegate void ZPLADInterstitialDidReceivedAdCallback(IntPtr interstitialClient);

        internal delegate void ZPLADInterstitialDidFailToReceiveAdWithErrorCallback(IntPtr interstitialClient, string error);

        internal delegate void ZPLADInterstitialVideoDidStartPlayingCallback(IntPtr interstitialClient);

        internal delegate void ZPLADInterstitialDidClickCallback(IntPtr interstitialClient);

        internal delegate void ZPLADInterstitialVideoDidCloseCallback(IntPtr interstitialClient);

        internal delegate void ZPLADInterstitialDidCompleteCallback(IntPtr interstitialClient);

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
            InterstitialPtr = Externs.ZPLADCreateInterstitial(interstitialClientPtr, adAppId, adUnitId);
            Externs.ZPLADSetInterstitialAdCallbacks(
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
                Externs.ZPLADRelease(interstitialPtr);
                interstitialPtr = value;
            }
        }



        #region IInterstitialAdClient implementation

        public void LoadAd(string adUnitId)
        {
            Externs.ZPLADRequestInterstitial(InterstitialPtr);
        }

        public bool IsReady(string adUnitId)
        {
            return Externs.ZPLADInterstitialReady(InterstitialPtr);
        }

        public void Show(string adUnitId)
        {
            Externs.ZPLADShowInterstitial(InterstitialPtr);
        }

        public void SetAutoloadNext(bool autoload)
        {
            Externs.ZPLADSetInterstitialAutoload(InterstitialPtr, autoload);
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

        [MonoPInvokeCallback(typeof(ZPLADInterstitialDidReceivedAdCallback))]
        static void InterstitialDidReceivedAdCallback(IntPtr interstitialClient)
        {
            InterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdLoaded != null)
            {
                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(ZPLADInterstitialDidFailToReceiveAdWithErrorCallback))]
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

        [MonoPInvokeCallback(typeof(ZPLADInterstitialVideoDidStartPlayingCallback))]
        static void InterstitialVideoDidStartPlayingCallback(IntPtr interstitialClient)
        {
            InterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdStarted != null)
            {
                client.OnAdStarted(client, EventArgs.Empty);
            }
        }


        [MonoPInvokeCallback(typeof(ZPLADInterstitialDidClickCallback))]
        static void InterstitiaDidClickCallback(IntPtr interstitialClient)
        {
            InterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(ZPLADInterstitialVideoDidCloseCallback))]
        static void InterstitialVideoDidCloseCallback(IntPtr interstitialClient)
        {
            InterstitialClient client = IntPtrToInterstitialClient(interstitialClient);
            if (client.OnAdClosed != null)
            {
                client.OnAdClosed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(ZPLADInterstitialDidCompleteCallback))]
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