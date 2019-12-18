#if UNITY_IOS
using System;
using AtmosplayAds.Common;
using AtmosplayAds.Api;
using System.Runtime.InteropServices;
using UnityEngine;
namespace AtmosplayAds.iOS
{
    public class RewardVideoClient : IRewardVideoClient
    {
        IntPtr rewardVideoPtr;
        IntPtr rewardVideoClientPtr;

        #region RewardVideo callback types

        internal delegate void ZPLADRewardVideoDidReceivedAdCallback(IntPtr rewardVideoClient);

        internal delegate void ZPLADRewardVideoDidFailToReceiveAdWithErrorCallback(IntPtr rewardVideoClient, string error);

        internal delegate void ZPLADRewardVideoVideoDidStartPlayingCallback(IntPtr rewardVideoClient);

        internal delegate void ZPLADRewardVideoDidClickCallback(IntPtr rewardVideoClient);

        internal delegate void ZPLADRewardVideoDidRewardCallback(IntPtr rewardVideoClient);

        internal delegate void ZPLADRewardVideoVideoDidCloseCallback(IntPtr rewardVideoClient);

        internal delegate void ZPLADRewardVideoDidCompleteCallback(IntPtr rewardVideoClient);

        #endregion
        public event EventHandler<EventArgs> OnAdLoaded;
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        public event EventHandler<EventArgs> OnAdStarted;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<EventArgs> OnAdRewarded;
        public event EventHandler<EventArgs> OnAdVideoFinished;
        public event EventHandler<EventArgs> OnAdClosed;

        public RewardVideoClient(string adAppId, string adUnitId)
        {
            rewardVideoClientPtr = (IntPtr)GCHandle.Alloc(this);
            RewardVideoPtr = Externs.ZPLADCreateRewardVideo(rewardVideoClientPtr, adAppId, adUnitId);
            Externs.ZPLADSetRewardVideoAdCallbacks(
                rewardVideoPtr,
                RewardVideoDidReceivedAdCallback,
                RewardVideoDidFailToReceiveAdWithErrorCallback,
                RewardVideoVideoDidStartPlayingCallback,
                RewardVideoDidClickCallback,
                RewardVideoDidRewardCallback,
                RewardVideoVideoDidCloseCallback,
                RewardVideoDidCompleteCallback
            );
        }

        IntPtr RewardVideoPtr
        {
            get
            {
                return rewardVideoPtr;
            }
            set
            {
                Externs.ZPLADRelease(rewardVideoPtr);
                rewardVideoPtr = value;
            }
        }

        #region IRewardVideoAdClient implementation

        public void LoadAd(string adUnitId)
        {
            Externs.ZPLADRequestRewardVideo(RewardVideoPtr);
        }

        public bool IsReady(string adUnitId)
        {
            return Externs.ZPLADRewardVideoReady(RewardVideoPtr);
        }

        public void Show(string adUnitId)
        {
            Externs.ZPLADShowRewardVideo(RewardVideoPtr);
        }

        public void SetAutoloadNext(bool autoload)
        {
            Externs.ZPLADSetRewardVideoAutoload(RewardVideoPtr, autoload);
        }

        public void SetChannelId(string channelId)
        {
            Externs.ZPLADSetRewardVideoChannelId(RewardVideoPtr, channelId);
        }
        public void DestroyRewardVideo()
        {
            RewardVideoPtr = IntPtr.Zero;
        }

        public void Dispose()
        {
            DestroyRewardVideo();
            ((GCHandle)rewardVideoClientPtr).Free();
        }

        ~RewardVideoClient()
        {
            Dispose();
        }
        #endregion

        #region RewardVideo callback methods

        [MonoPInvokeCallback(typeof(ZPLADRewardVideoDidReceivedAdCallback))]
        static void RewardVideoDidReceivedAdCallback(IntPtr interstitialClient)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(interstitialClient);
            if (client.OnAdLoaded != null)
            {
                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(ZPLADRewardVideoDidFailToReceiveAdWithErrorCallback))]
        static void RewardVideoDidFailToReceiveAdWithErrorCallback(IntPtr interstitialClient, string error)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(interstitialClient);
            if (client.OnAdFailedToLoad != null)
            {
                AdFailedEventArgs args = new AdFailedEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(ZPLADRewardVideoVideoDidStartPlayingCallback))]
        static void RewardVideoVideoDidStartPlayingCallback(IntPtr interstitialClient)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(interstitialClient);
            if (client.OnAdStarted != null)
            {
                client.OnAdStarted(client, EventArgs.Empty);
            }
        }


        [MonoPInvokeCallback(typeof(ZPLADRewardVideoDidClickCallback))]
        static void RewardVideoDidClickCallback(IntPtr rewardVideo)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(rewardVideo);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(ZPLADRewardVideoDidRewardCallback))]
        static void RewardVideoDidRewardCallback(IntPtr rewardVideo)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(rewardVideo);
            if (client.OnAdRewarded != null)
            {
                client.OnAdRewarded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(ZPLADRewardVideoVideoDidCloseCallback))]
        static void RewardVideoVideoDidCloseCallback(IntPtr interstitialClient)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(interstitialClient);
            if (client.OnAdClosed != null)
            {
                client.OnAdClosed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(ZPLADRewardVideoDidCompleteCallback))]
        static void RewardVideoDidCompleteCallback(IntPtr interstitialClient)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(interstitialClient);
            if (client.OnAdVideoFinished != null)
            {
                client.OnAdVideoFinished(client, EventArgs.Empty);
            }
        }

        private static RewardVideoClient IntPtrToRewardVideoClient(IntPtr rewardVideoClient)
        {
            GCHandle handle = (GCHandle)rewardVideoClient;
            return handle.Target as RewardVideoClient;
        }
        #endregion
    }
}
#endif
