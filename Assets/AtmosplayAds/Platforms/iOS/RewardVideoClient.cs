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
        IntPtr rewardedVideoPtr;
        IntPtr rewardedVideoClientPtr;
        #region RewardedVideo callback types
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
            rewardedVideoClientPtr = (IntPtr)GCHandle.Alloc(this);
            RewardedVideoPtr = Externs.AtmosplayAdsCreateRewardedVideo(rewardedVideoClientPtr, adAppId, adUnitId);
            Externs.AtmosplayAdsSetRewardedVideoAdCallbacks(
                rewardedVideoPtr,
                RewardedVideoDidReceivedAdCallback,
                RewardedVideoDidFailToReceiveAdWithErrorCallback,
                RewardedVideoVideoDidStartPlayingCallback,
                RewardedVideoDidClickCallback,
                RewardedVideoDidRewardCallback,
                RewardedVideoVideoDidCloseCallback,
                RewardedVideoDidCompleteCallback
            );
        }

        IntPtr RewardedVideoPtr
        {
            get
            {
                return rewardedVideoPtr;
            }
            set
            {
                Externs.AtmosplayAdsRelease(rewardedVideoPtr);
                rewardedVideoPtr = value;
            }
        }

        #region IRewardedVideoAdClient implementation

        public void LoadAd(string adUnitId)
        {
            Externs.AtmosplayAdsRequestRewardedVideo(RewardedVideoPtr);
        }

        public bool IsReady(string adUnitId)
        {
            return Externs.AtmosplayRewardedVideoReady(RewardedVideoPtr);
        }

        public void Show(string adUnitId)
        {
            Externs.AtmosplayShowRewardedVideo(RewardedVideoPtr);
        }

        public void SetAutoloadNext(bool autoload)
        {
            Externs.AtmosplaySetRewardedVideoAutoload(RewardedVideoPtr, autoload);
        }

        public void SetChannelId(string channelId)
        {
            Externs.AtmosplaySetRewardedVideoChannelId(RewardedVideoPtr, channelId);
        }
        public void DestroyRewardedVideo()
        {
            RewardedVideoPtr = IntPtr.Zero;
        }

        public void Dispose()
        {
            DestroyRewardedVideo();
            ((GCHandle)rewardedVideoClientPtr).Free();
        }

        ~RewardVideoClient()
        {
            Dispose();
        }
        #endregion

        #region RewardedVideo callback methods

        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidReceivedAdCallback))]
        static void RewardedVideoDidReceivedAdCallback(IntPtr rewardedVideoClient)
        {
            RewardVideoClient client = IntPtrToRewardedVideoClient(rewardedVideoClient);
            if (client.OnAdLoaded != null)
            {
                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidFailToReceiveAdWithErrorCallback))]
        static void RewardedVideoDidFailToReceiveAdWithErrorCallback(IntPtr rewardedVideoClient, string error)
        {
            RewardVideoClient client = IntPtrToRewardedVideoClient(rewardedVideoClient);
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
        static void RewardedVideoVideoDidStartPlayingCallback(IntPtr rewardedVideoClient)
        {
            RewardVideoClient client = IntPtrToRewardedVideoClient(rewardedVideoClient);
            if (client.OnAdStarted != null)
            {
                client.OnAdStarted(client, EventArgs.Empty);
            }
        }


        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidClickCallback))]
        static void RewardedVideoDidClickCallback(IntPtr rewardedVideoClient)
        {
            RewardVideoClient client = IntPtrToRewardedVideoClient(rewardedVideoClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidRewardCallback))]
        static void RewardedVideoDidRewardCallback(IntPtr rewardedVideoClient)
        {
            RewardVideoClient client = IntPtrToRewardedVideoClient(rewardedVideoClient);
            if (client.OnAdRewarded != null)
            {
                client.OnAdRewarded(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidCloseCallback))]
        static void RewardedVideoVideoDidCloseCallback(IntPtr rewardedVideoClient)
        {
            RewardVideoClient client = IntPtrToRewardedVideoClient(rewardedVideoClient);
            if (client.OnAdClosed != null)
            {
                client.OnAdClosed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayRewardedVideoDidCompleteCallback))]
        static void RewardedVideoDidCompleteCallback(IntPtr rewardedVideoClient)
        {
            RewardVideoClient client = IntPtrToRewardedVideoClient(rewardedVideoClient);
            if (client.OnAdVideoFinished != null)
            {
                client.OnAdVideoFinished(client, EventArgs.Empty);
            }
        }

        private static RewardVideoClient IntPtrToRewardedVideoClient(IntPtr rewardedVideoClient)
        {
            GCHandle handle = (GCHandle)rewardedVideoClient;
            return handle.Target as RewardVideoClient;
        }
        #endregion
    }
}
#endif
