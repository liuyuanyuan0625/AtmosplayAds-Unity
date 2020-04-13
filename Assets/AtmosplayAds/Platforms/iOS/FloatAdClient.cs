#if UNITY_IOS
using System;
using AtmosplayAds.Common;
using AtmosplayAds.Api;
using System.Runtime.InteropServices;
using UnityEngine;

namespace AtmosplayAds.iOS
{
    public class FloatAdClient : IFloatAdClient
    {
        private IntPtr floatAdPtr;

        private IntPtr floatAdClientPtr;

        private GameObject currentGameObject;

        private string channelID;

        private bool isAutoLoad;

        private int x;

        private int y;

        private int width;

        private string appID;

#region Float Ad callback types

        internal delegate void AtmosplayFloatAdDidReceivedAdCallback(IntPtr floatAdClient);

        internal delegate void AtmosplayFloatAdDidFailToLoadAdWithErrorCallback(IntPtr floatAdClient, string error);

        internal delegate void AtmosplayFloatAdDidStartPlayingCallback(IntPtr floatAdClient);

        internal delegate void AtmosplayFloatAdDidClickCallback(IntPtr floatAdClient);

        internal delegate void AtmosplayFloatAdDidCloseCallback(IntPtr floatAdClient);

        internal delegate void AtmosplayFloatAdDidCompleteCallback(IntPtr floatAdClient);

        internal delegate void AtmosplayFloatAdDidRewardedCallback(IntPtr floatAdClient);

#endregion

        // Ad event fired when the float ad has been received.
        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the float ad has failed to load.
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        // Ad event fired when the float ad is clicked
        public event EventHandler<EventArgs> OnAdStarted;
        public event EventHandler<EventArgs> OnAdVideoFinished;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<EventArgs> OnAdClosed;
        public event EventHandler<EventArgs> OnAdRewarded;

        public FloatAdClient(string adAppId, string adUnitId, GameObject gameObject)
        {
            floatAdClientPtr = (IntPtr)GCHandle.Alloc(this);

            currentGameObject = gameObject;

            appID = adAppId;
        }

        // This property should be used when setting the FloatAdPtr.
        private IntPtr FloatAdPtr
        {
            get
            {
                return FloatAdPtr;
            }

            set
            {
                Externs.AtmosplayAdsRelease(FloatAdPtr); // clear cache ,if existed
                FloatAdPtr = value;
            }
        }

#region IYumiFloatAdClient implement 
        public void LoadAd(string adUnitId)
        {
            floatAdPtr = Externs.AtmosplayAdsCreateFloatAd(floatAdClientPtr, appID, adUnitId, isAutoLoad);
            Externs.AtmosplayAdsSetFloatAdCallbacks(
                floatAdPtr,
                floatAdDidReceivedAdCallback,
                floatAdDidFailToLoadAdWithErrorCallback,
                floatAdDidStartPlayingCallback,
                floatAdDidClickCallback,
                floatAdDidCompleteCallback,
                floatAdDidCloseCallback,
                floatAdDidRewardedCallback
            );

            Externs.setFloatAdChannelId(floatAdPtr, channelID);
        }
        
        public bool IsReady(string adUnitId)
        {
            return Externs.floatAdIsReady(floatAdPtr);
        }

        public void Show(string adUnitId)
        {
            Externs.showFloatAd(floatAdPtr ,x, y, width);
        }

        public void SetChannelId(string channelId)
        {
            channelID = channelId;
        }

        public void SetAutoloadNext(bool autoLoad)
        {
            isAutoLoad = autoLoad;
        }

        public void SetPointAndWidth(Transform floatAdRect)
        {
            if (floatAdRect != null)
            {

                Camera camera = Camera.main;
                Rect floatAdRectTransform = getGameObjectRect(floatAdRect as RectTransform, camera);

                x = (int)floatAdRectTransform.x;
                y = (int)floatAdRectTransform.y;
                width = (int)floatAdRectTransform.width;
            }
        }
 
        public void UpdatePointAndWidth(Transform floatAdRect) 
        {
            if (floatAdRect != null)
            {

                Camera camera = Camera.main;
                Rect floatAdRectTransform = getGameObjectRect(floatAdRect as RectTransform, camera);

                x = (int)floatAdRectTransform.x;
                y = (int)floatAdRectTransform.y;
                width = (int)floatAdRectTransform.width;
            }

            Externs.updateFloatAdPosition(floatAdPtr , x, y, width);
        }

        public void Hidden()
        {
            Externs.hiddenFloatAd(floatAdPtr);
        }

        public void ShowAgainAfterHiding()
        {
            Externs.showFloatAdAgainAfterHiding(floatAdPtr);
        }

        public void Destroy()
        {
            Externs.destroyFloatAd(floatAdPtr);
        }

        private Rect getGameObjectRect(RectTransform rectTransform, Camera camera)
        {
            if (rectTransform == null)
            {
                return Rect.zero;
            }

            Vector3[] worldCorners = new Vector3[4];
            Canvas canvas = getCanvas(this.currentGameObject);

            rectTransform.GetWorldCorners(worldCorners);
            Vector3 gameObjectBottomLeft = worldCorners[0];
            Vector3 gameObjectTopRight = worldCorners[2];
            Vector3 cameraBottomLeft = camera.pixelRect.min;
            Vector3 cameraTopRight = camera.pixelRect.max;

            if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                gameObjectBottomLeft = camera.WorldToScreenPoint(gameObjectBottomLeft);
                gameObjectTopRight = camera.WorldToScreenPoint(gameObjectTopRight);
            }

            return new Rect(Mathf.Round(gameObjectBottomLeft.x),
                            Mathf.Floor((cameraTopRight.y - gameObjectTopRight.y)),
                            Mathf.Ceil((gameObjectTopRight.x - gameObjectBottomLeft.x)),
                            Mathf.Round((gameObjectTopRight.y - gameObjectBottomLeft.y)));
        }
        private Canvas getCanvas(GameObject gameObject)
        {
            if (gameObject.GetComponent<Canvas>() != null)
            {
                return gameObject.GetComponent<Canvas>();
            }
            else
            {
                if (gameObject.transform.parent != null)
                {
                    return getCanvas(gameObject.transform.parent.gameObject);
                }
            }
            return null;
        }
        
#endregion

#region Float Ad callback methods
        [MonoPInvokeCallback(typeof(AtmosplayFloatAdDidReceivedAdCallback))]
        private static void floatAdDidReceivedAdCallback(IntPtr floatAdClient)
        {
            FloatAdClient client = IntPtrToFloatAdClient(floatAdClient);
            if (client.OnAdLoaded != null)
            {
                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }
        [MonoPInvokeCallback(typeof(AtmosplayFloatAdDidFailToLoadAdWithErrorCallback))]
        private static void floatAdDidFailToLoadAdWithErrorCallback(IntPtr floatAdClient, string error)
        {
            FloatAdClient client = IntPtrToFloatAdClient(floatAdClient);
            if (client.OnAdFailedToLoad != null)
            {
                AdFailedEventArgs args = new AdFailedEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }
        [MonoPInvokeCallback(typeof(AtmosplayFloatAdDidStartPlayingCallback))]
        private static void floatAdDidStartPlayingCallback(IntPtr floatAdClient)
        {
            FloatAdClient client = IntPtrToFloatAdClient(floatAdClient);
            if (client.OnAdStarted != null)
            {
                client.OnAdStarted(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayFloatAdDidClickCallback))]
        private static void floatAdDidClickCallback(IntPtr floatAdClient)
        {
            FloatAdClient client = IntPtrToFloatAdClient(floatAdClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayFloatAdDidCloseCallback))]
        private static void floatAdDidCloseCallback(IntPtr floatAdClient)
        {
            FloatAdClient client = IntPtrToFloatAdClient(floatAdClient);
            if (client.OnAdClosed != null)
            {
                client.OnAdClosed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayFloatAdDidCompleteCallback))]
        private static void floatAdDidCompleteCallback(IntPtr floatAdClient)
        {
            FloatAdClient client = IntPtrToFloatAdClient(floatAdClient);
            if (client.OnAdVideoFinished != null)
            {
                client.OnAdVideoFinished(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayFloatAdDidRewardedCallback))]
        private static void floatAdDidRewardedCallback(IntPtr floatAdClient)
        {
            FloatAdClient client = IntPtrToFloatAdClient(floatAdClient);
            if (client.OnAdRewarded != null)
            {
                client.OnAdRewarded(client, EventArgs.Empty);
            }
        }

        private static FloatAdClient IntPtrToFloatAdClient(IntPtr floatAdClient)
        {
            GCHandle handle = (GCHandle)floatAdClient;

            return handle.Target as FloatAdClient;
        }

#endregion
        }
    }
#endif