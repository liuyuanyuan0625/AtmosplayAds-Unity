#if UNITY_ANDROID

using System;
using UnityEngine;

using AtmosplayAds.Api;
using AtmosplayAds.Common;

namespace AtmosplayAds.Android
{
    public class FloatAdClient : AndroidJavaProxy, IFloatAdClient
    {
        private AndroidJavaObject androidFloatAd;
        private GameObject currentGameObject;

        public event EventHandler<EventArgs> OnAdLoaded = delegate { };
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad = delegate { };
        public event EventHandler<EventArgs> OnAdStarted = delegate { };
        public event EventHandler<EventArgs> OnAdRewarded = delegate { };
        public event EventHandler<EventArgs> OnAdClicked = delegate { };
        public event EventHandler<EventArgs> OnAdVideoFinished = delegate { };
        public event EventHandler<EventArgs> OnAdClosed = delegate { };

        public FloatAdClient(string appId, GameObject gameObject) : base(Utils.UnityFloatAdListenerClassName)
        {
            currentGameObject = gameObject;
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            androidFloatAd = new AndroidJavaObject(Utils.FloatAdClassName, activity, appId, this);
        }

        #region IFloatAdClient implementation

        public void LoadAd(string adUnitId)
        {
            androidFloatAd.Call("loadAd", adUnitId);
        }

        public bool IsReady(string adUnitId)
        {
            return androidFloatAd.Call<bool>("isLoaded", adUnitId);
        }

        public void Show(string adUnitId)
        {
            androidFloatAd.Call("show", adUnitId);
        }

        public void SetAutoloadNext(bool autoload)
        {
            androidFloatAd.Call("setAutoloadNext", autoload);
        }

        public void SetChannelId(string channelId){
            androidFloatAd.Call("setChannelId", channelId);
        }

       
        public void SetPointAndWidth(Transform floatAdRectTransform)
        {
            if (floatAdRectTransform != null)
            {

                Camera camera = Camera.main;
                Rect floatAdRect = getGameObjectRect(floatAdRectTransform as RectTransform, camera);

                androidFloatAd.Call("setPointAndWidth", (int)floatAdRect.x, (int)floatAdRect.y, (int)floatAdRect.width);
            }
        }

        public void UpdatePointAndWidth(Transform floatAdRectTransform)
        {
            Camera camera = Camera.main;
            Rect floatAdRect = getGameObjectRect(floatAdRectTransform as RectTransform, camera);
            androidFloatAd.Call("updatePointAndWidth", (int)floatAdRect.x, (int)floatAdRect.y, (int)floatAdRect.width);
        }

        public void Hidden()
        {
            androidFloatAd.Call("hidden");
        }

        public void ShowAgainAfterHiding()
        {
            androidFloatAd.Call("showAgainAfterHiding");
        }

        public void Destroy()
        {
            androidFloatAd.Call("onDestroy");
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

#region Callback from UnityFloatAdListener
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

        void onAdRewarded()
        {
            if (OnAdRewarded != null)
            {
                OnAdRewarded(this, EventArgs.Empty);
            }
        }

        void onAdClicked()
        {
            if (OnAdClicked != null)
            {
                OnAdClicked(this, EventArgs.Empty);
            }
        }

        void onAdVideoCompleted()
        {
            if (OnAdVideoFinished != null)
            {
                OnAdVideoFinished(this, EventArgs.Empty);
            }
        }

        void onAdCompleted()
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