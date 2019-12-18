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

        internal delegate void AtmosplayRewardedVideoDidReceivedAdCallback(IntPtr rewardedVideoClient);

        internal delegate void AtmosplayRewardedVideoDidFailToReceiveAdWithErrorCallback(IntPtr rewardedVideoClient, string error);

        internal delegate void AtmosplayRewardedVideoDidStartPlayingCallback(IntPtr rewardedVideoClient);

        internal delegate void AtmosplayRewardedVideoDidClickCallback(IntPtr rewardedVideoClient);

        internal delegate void AtmosplayRewardedVideoDidRewardCallback(IntPtr rewardedVideoClient);

        internal delegate void AtmosplayRewardedVideoDidCloseCallback(IntPtr rewardedVideoClient);

        internal delegate void AtmosplayRewardedVideoDidCompleteCallback(IntPtr rewardedVideoClient);

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

        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidReceivedAdCallback))]
        static void RewardVideoDidReceivedAdCallback(IntPtr rewardedVideoClient)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(rewardedVideoClient);
            if (client.OnAdLoaded != null)
            {
                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidFailToReceiveAdWithErrorCallback))]
        static void RewardVideoDidFailToReceiveAdWithErrorCallback(IntPtr rewardedVideoClient, string error)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(rewardedVideoClient);
            if (client.OnAdFailedToLoad != null)
            {
                AdFailedEventArgs args = new AdFailedEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidStartPlayingCallback))]
        static void RewardVideoVideoDidStartPlayingCallback(IntPtr rewardedVideoClient)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(rewardedVideoClient);
            if (client.OnAdStarted != null)
            {
                client.OnAdStarted(client, EventArgs.Empty);
            }
        }


        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidClickCallback))]
        static void RewardVideoDidClickCallback(IntPtr rewardedVideoClient)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(rewardedVideoClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidRewardCallback))]
        static void RewardVideoDidRewardCallback(IntPtr rewardedVideoClient)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(rewardedVideoClient);
            if (client.OnAdRewarded != null)
            {
                client.OnAdRewarded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidCloseCallback))]
        static void RewardVideoVideoDidCloseCallback(IntPtr rewardedVideoClient)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(rewardedVideoClient);
            if (client.OnAdClosed != null)
            {
                client.OnAdClosed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidCompleteCallback))]
        static void RewardVideoDidCompleteCallback(IntPtr rewardedVideoClient)
        {
            RewardVideoClient client = IntPtrToRewardVideoClient(rewardedVideoClient);
            if (client.OnAdVideoFinished != null)
            {
                client.OnAdVideoFinished(client, EventArgs.Empty);
            }
        }

        private static RewardVideoClient IntPtrToRewardVideoClient(IntPtr rewardedVideoClient)
        {
            GCHandle handle = (GCHandle)rewardedVideoClient;
            return handle.Target as RewardVideoClient;
        }
        #endregion
    }
}
#endif
