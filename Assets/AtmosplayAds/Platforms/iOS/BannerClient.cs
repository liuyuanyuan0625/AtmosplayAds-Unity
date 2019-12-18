#if UNITY_IOS
using System;
using ZPLAYAds.Common;
using ZPLAYAds.Api;
using System.Runtime.InteropServices;

namespace ZPLAYAds.iOS
{
    public class BannerClient : IBannerClient
    {
        private IntPtr bannerViewPtr;

        private IntPtr bannerClientPtr;
#region Banner callback types

        internal delegate void AtmosplayBannerDidReceiveAdCallback(IntPtr bannerClient);

        internal delegate void AtmosplayBannerDidFailToReceiveAdWithErrorCallback(
                IntPtr bannerClient, string error);

        internal delegate void AtmosplayBannerDidClickCallback(IntPtr bannerClient);

#endregion

        // Ad event fired when the banner ad has been received.
        public event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the banner ad has failed to load.
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        // Ad event fired when the banner ad is clicked
        public event EventHandler<EventArgs> OnAdClicked;

        // This property should be used when setting the bannerViewPtr.
        private IntPtr BannerViewPtr
        {
            get
            {
                return bannerViewPtr;
            }

            set
            {
                Externs.ZPLADRelease(bannerViewPtr); // clear cache ,if existed
                bannerViewPtr = value;
            }
        }

#region IYumiBannerClient implement 
        // Creates a banner view and adds it to the view hierarchy.
        public void CreateBannerView(string adAppId, string adUnitId, BannerViewOptions options)
        {
            // A new GCHandle that protects the object from garbage collection. This GCHandle must be released with Free() when it is no longer needed.
            bannerClientPtr = (IntPtr)GCHandle.Alloc(this);

            BannerViewPtr = Externs.InitAtmosplayBannerAd(bannerClientPtr,adAppId, adUnitId);
            AtmosplayBannerAdSize bannerSize = AtmosplayBannerAdSize.kAtmosplayAdsBanner320x50;
            switch (options.bannerSize)
            {
                case BannerAdSize.BANNER_AD_SIZE_320x50:
                    bannerSize = AtmosplayBannerAdSize.kAtmosplayAdsBanner320x50;
                    break;
                case BannerAdSize.BANNER_AD_SIZE_728x90:
                    bannerSize = AtmosplayBannerAdSize.kAtmosplayAdsBanner728x90;
                    break;
                
                case BannerAdSize.BANNER_AD_SIZE_SMART_PORTRAIT:
                    bannerSize = AtmosplayBannerAdSize.kAtmosplayAdsSmartBannerPortrait;
                    break;
                case BannerAdSize.BANNER_AD_SIZE_SMART_LANDSCAPE:
                    bannerSize = AtmosplayBannerAdSize.kAtmosplayAdsSmartBannerLandscape;
                    break;
            }

            Externs.SetBannerAdSize(BannerViewPtr,bannerSize);
            Externs.SetBannerPosition(BannerViewPtr,(int)options.adPosition);
            if (options.channelID != null)
            {
                Externs.SetBannerChannelID(BannerViewPtr, options.channelID);
            }
            
            Externs.SetBannerCallbacks(
                BannerViewPtr,
                BannerDidReceiveAdCallback,
                BannerDidFailToReceiveAdWithErrorCallback,
                BannerDidClickCallback
                );
        }
        // Requests a new ad for the banner view.
        public void LoadAd()
        {
            Externs.RequestBannerAd(BannerViewPtr);
        }

        // Shows the banner view on the screen.
        public void ShowBannerView()
        {
            Externs.ShowBannerView(BannerViewPtr);
        }

        // Hides the banner view from the screen.
        public void HideBannerView()
        {
            Externs.HideBannerView(BannerViewPtr);
        }

        // Destroys a banner view.
        public void DestroyBannerView()
        {
            Externs.DestroyBannerView(BannerViewPtr);
            BannerViewPtr = IntPtr.Zero;
        }
        //dealloc
        public void Dispose()
        {
            DestroyBannerView();
            ((GCHandle)this.bannerClientPtr).Free();
        }

        ~BannerClient()
        {
            Dispose();
        }
#endregion

#region Banner callback methods
        [MonoPInvokeCallback(typeof(AtmosplayBannerDidReceiveAdCallback))]
        private static void BannerDidReceiveAdCallback(IntPtr bannerClient)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdLoaded != null)
            {
                client.OnAdLoaded(client, EventArgs.Empty);
            }
        }
        [MonoPInvokeCallback(typeof(AtmosplayBannerDidFailToReceiveAdWithErrorCallback))]
        private static void BannerDidFailToReceiveAdWithErrorCallback(IntPtr bannerClient, string error)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdFailedToLoad != null)
            {
                AdFailedEventArgs args = new AdFailedEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }
        [MonoPInvokeCallback(typeof(AtmosplayBannerDidClickCallback))]
        private static void BannerDidClickCallback(IntPtr bannerClient)
        {
            BannerClient client = IntPtrToBannerClient(bannerClient);
            if (client.OnAdClicked != null)
            {
                client.OnAdClicked(client, EventArgs.Empty);
            }
        }


        private static BannerClient IntPtrToBannerClient(IntPtr bannerClient)
        {
            GCHandle handle = (GCHandle)bannerClient;

            return handle.Target as BannerClient;
        }

#endregion
        }
    }
#endif