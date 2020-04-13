#if UNITY_IOS
using System;
using AtmosplayAds.Common;
using AtmosplayAds.Api;
using System.Runtime.InteropServices;
using UnityEngine;

namespace AtmosplayAds.iOS
{
    public class WindowAdClient : IWindowAdClient
    {
        private IntPtr windowAdPtr;

        private IntPtr windowAdClientPtr;

        private GameObject currentGameObject;

        private int x;

        private int y;

        private int width;

        private int angle;

#region Float Ad callback types

        internal delegate void AtmosplayWindowAdDidReceivedAdCallback(IntPtr windowAdClient);

        internal delegate void AtmosplayWindowAdDidFailToLoadAdWithErrorCallback(IntPtr windowAdClient, string error);

        internal delegate void AtmosplayWindowAdDidStartPlayingCallback(IntPtr windowAdClient);

        internal delegate void AtmosplayWindowAdDidClickCallback(IntPtr windowAdClient);

        internal delegate void AtmosplayWindowAdDidCompleteCallback(IntPtr windowAdClient);

        internal delegate void AtmosplayWindowAdDidCloseCallback(IntPtr windowAdClient);

        internal delegate void AtmosplayWindowAdDidFailToShowCallback(IntPtr windowAdClient);

#endregion

        public event EventHandler<EventArgs> OnAdLoaded;
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        public event EventHandler<EventArgs> OnAdStarted;
        public event EventHandler<EventArgs> OnAdClicked;
        public event EventHandler<EventArgs> OnAdFinished;
        public event EventHandler<EventArgs> OnAdClosed;
        public event EventHandler<EventArgs> OnAdFailToShow;

        public WindowAdClient(string adAppId, string adUnitId, GameObject gameObject)
        {
            windowAdClientPtr = (IntPtr)GCHandle.Alloc(this);
            windowAdPtr = Externs.AtmosplayAdsCreateWindowAd(windowAdClientPtr, adAppId, adUnitId);
            Externs.AtmosplayAdsSetWindowAdCallbacks(
                windowAdPtr,
                windowAdDidReceivedAdCallback,
                windowAdDidFailToLoadAdWithErrorCallback,
                windowAdDidStartPlayingCallback,
                windowAdDidClickCallback,
                windowAdDidCompleteCallback,
                windowAdDidCloseCallback,
                windowAdDidFailToShowCallback
            );

            currentGameObject = gameObject;
        }

        private IntPtr WindowAdPtr
        {
            get
            {
                return WindowAdPtr;
            }

            set
            {
                Externs.AtmosplayAdsRelease(WindowAdPtr); // clear cache ,if existed
                WindowAdPtr = value;
            }
        }

#region IWindowAdClient implement 
        public bool IsReady()
        {
            return Externs.windowAdIsReady(windowAdPtr);
        }

        public void Show()
        {
            Externs.showWindowAd(windowAdPtr ,x, y, angle, width);
        }

        public void SetAngle(int windowAdAngle) 
        {
            angle = windowAdAngle;
        }

        public void SetChannelId(string channelId)
        {
            Externs.setWindowAdChannelId(windowAdPtr, channelId);
        }

        public void SetPointAndWidth(Transform windowAdRect)
        {
            if (windowAdRect != null)
            {
                Camera camera = Camera.main;
                Rect windowAdRectTransform = getGameObjectRect(windowAdRect as RectTransform, camera);

                x = (int)windowAdRectTransform.x;
                y = (int)windowAdRectTransform.y;
                width = (int)windowAdRectTransform.width;
            }
        }
 
        public void UpdatePointAndWidth(Transform windowAdRect) 
        {
            if (windowAdRect != null)
            {
                Camera camera = Camera.main;
                Rect windowAdRectTransform = getGameObjectRect(windowAdRect as RectTransform, camera);

                x = (int)windowAdRectTransform.x;
                y = (int)windowAdRectTransform.y;
                width = (int)windowAdRectTransform.width;
            }

            Externs.updateWindowAdPosition(windowAdPtr , x, y, angle, width);
        }

        public void Hidden()
        {
            Externs.hiddenWindowAd(windowAdPtr);
        }

        public void ShowAgainAfterHiding()
        {
            Externs.showWindowAdAgainAfterHiding(windowAdPtr);
        }

        public void Destroy()
        {
            Externs.destroyWindowAd(windowAdPtr);
        }

        public void PauseVideo()
        {
            Externs.pauseVideo(windowAdPtr);
        }

        public void ResumeVideo()
        {
            Externs.resumeVideo(windowAdPtr);
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

#region Window Ad callback methods
        [MonoPInvokeCallback(typeof(AtmosplayWindowAdDidReceivedAdCallback))]
        private static void windowAdDidReceivedAdCallback(IntPtr windowAdClient)
        {
            WindowAdClient client = IntPtrToWindowAdClient(windowAdClient);
            if (client.OnAdLoaded != null)
            {
                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }
        [MonoPInvokeCallback(typeof(AtmosplayWindowAdDidFailToLoadAdWithErrorCallback))]
        private static void windowAdDidFailToLoadAdWithErrorCallback(IntPtr windowAdClient, string error)
        {
            WindowAdClient client = IntPtrToWindowAdClient(windowAdClient);
            if (client.OnAdFailedToLoad != null)
            {
                AdFailedEventArgs args = new AdFailedEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }
        [MonoPInvokeCallback(typeof(AtmosplayWindowAdDidStartPlayingCallback))]
        private static void windowAdDidStartPlayingCallback(IntPtr windowAdClient)
        {
            WindowAdClient client = IntPtrToWindowAdClient(windowAdClient);
            if (client.OnAdStarted != null)
            {
                client.OnAdStarted(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayWindowAdDidClickCallback))]
        private static void windowAdDidClickCallback(IntPtr windowAdClient)
        {
            WindowAdClient client = IntPtrToWindowAdClient(windowAdClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayWindowAdDidCloseCallback))]
        private static void windowAdDidCloseCallback(IntPtr windowAdClient)
        {
            WindowAdClient client = IntPtrToWindowAdClient(windowAdClient);
            if (client.OnAdClosed != null)
            {
                client.OnAdClosed(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayWindowAdDidCompleteCallback))]
        private static void windowAdDidCompleteCallback(IntPtr windowAdClient)
        {
            WindowAdClient client = IntPtrToWindowAdClient(windowAdClient);
            if (client.OnAdFinished != null)
            {
                client.OnAdFinished(client, EventArgs.Empty);
            }
        }

        [MonoPInvokeCallback(typeof(AtmosplayWindowAdDidFailToShowCallback))]
        private static void windowAdDidFailToShowCallback(IntPtr windowAdClient)
        {
            WindowAdClient client = IntPtrToWindowAdClient(windowAdClient);
            if (client.OnAdFailToShow != null)
            {
                client.OnAdFailToShow(client, EventArgs.Empty);
            }
        }

        private static WindowAdClient IntPtrToWindowAdClient(IntPtr windowAdClient)
        {
            GCHandle handle = (GCHandle)windowAdClient;

            return handle.Target as WindowAdClient;
        }

#endregion
        }
    }
#endif