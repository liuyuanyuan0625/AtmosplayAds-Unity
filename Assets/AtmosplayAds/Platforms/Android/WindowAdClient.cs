#if UNITY_ANDROID

using System;
using UnityEngine;

using AtmosplayAds.Api;
using AtmosplayAds.Common;

namespace AtmosplayAds.Android
{
    public class WindowAdClient : AndroidJavaProxy, IWindowAdClient
    {
        private AndroidJavaObject androidWindowAd;
        private GameObject currentGameObject;

        public event EventHandler<EventArgs> OnAdLoaded = delegate { };
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad = delegate { };
        public event EventHandler<EventArgs> OnAdStarted = delegate { };
        public event EventHandler<EventArgs> OnAdClicked = delegate { };
        public event EventHandler<EventArgs> OnAdClosed = delegate { };
        public event EventHandler<EventArgs> OnAdFinished = delegate { };

        public WindowAdClient(string appId, String adUnitId, GameObject gameObject) : base(Utils.UnityWindowAdListenerClassName)
        {
            currentGameObject = gameObject;
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            androidWindowAd = new AndroidJavaObject(Utils.WindowAdClassName, activity, appId, adUnitId, this);
        }

#region IWindowAdClient implementation


        public bool IsReady()
        {
            return androidWindowAd.Call<bool>("isLoaded");
        }

        public void Show()
        {
            androidWindowAd.Call("show");
        }

        public void SetChannelId(string channelId){
            androidWindowAd.Call("setChannelId", channelId);
        }

       
        public void SetPointAndWidth(Transform windowAdRectTransform)
        {
            if (windowAdRectTransform != null)
            {

                Camera camera = Camera.main;
                Rect windowAdRect = getGameObjectRect(windowAdRectTransform as RectTransform, camera);

                androidWindowAd.Call("setPointAndWidth", (int)windowAdRect.x, (int)windowAdRect.y, (int)windowAdRect.width);
            }
        }

        public void UpdatePointAndWidth(Transform windowAdRectTransform)
        {
            Camera camera = Camera.main;
            Rect windowAdRect = getGameObjectRect(windowAdRectTransform as RectTransform, camera);

            androidWindowAd.Call("updatePointAndWidth", (int)windowAdRect.x, (int)windowAdRect.y, (int)windowAdRect.width);
        }

        public void Hidden()
        {
            androidWindowAd.Call("hidden");
        }

        public void ShowAgainAfterHiding()
        {
            androidWindowAd.Call("showAgainAfterHiding");
        }

        public void Destroy()
        {
            androidWindowAd.Call("destroy");
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

#region Callback from UnityWindowAdListener
        void onAdLoaded()
        {
            if (OnAdLoaded != null)
            {
                OnAdLoaded(this, EventArgs.Empty);
            }
        }

        void onAdFailed(String errorReason)
        {
            if (OnAdFailedToLoad != null)
            {
                AdFailedEventArgs args = new AdFailedEventArgs()
                {
                    Message = errorReason
                };
                OnAdFailedToLoad(this, args);
            }
        }

        void onAdStarted()
        {
            if (OnAdStarted != null)
            {
                OnAdStarted(this, EventArgs.Empty);
            }
        }

        void onAdFinished()
        {
            if (OnAdFinished != null)
            {
                OnAdFinished(this, EventArgs.Empty);
            }
        }

        void onAdClicked()
        {
            if (OnAdClicked != null)
            {
                OnAdClicked(this, EventArgs.Empty);
            }
        }

        void onAdCloase()
        {
            if (OnAdClosed != null)
            {
                OnAdClosed(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}

#endif