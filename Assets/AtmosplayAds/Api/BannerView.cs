using System;
using ZPLAYAds.Common;
namespace ZPLAYAds.Api
{
    public class BannerView
    {
        private IBannerClient bannerClient;
        public BannerView(string adAppId, string adUnitId, BannerViewOptions options)
        {
            bannerClient = ZPLAYAdsClientFactory.BuildBannerClient();
            bannerClient.CreateBannerView(adAppId,adUnitId,options);
            ConfigureBannerEvents();
        }
        /// <summary>
        /// Occurs when the banner ad has loaded.
        /// </summary>
        public event EventHandler<EventArgs> OnAdLoaded;
        /// <summary>
        /// Occurs  when the banner ad has failed to load.
        /// </summary>
        public event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        /// <summary>
        /// Occurs when the banner ad is click.
        /// </summary>
        public event EventHandler<EventArgs> OnAdClicked;

        /// <summary>
        /// Loads an ad into the BannerView.
        /// </summary>
        public void LoadAd()
        {
            bannerClient.LoadAd();
        }
        /// <summary>
        /// Hides the BannerView from the screen.
        /// </summary>
        public void Hide()
        {
            bannerClient.HideBannerView();
        }

        /// <summary>
        /// Shows the BannerView on the screen.
        /// </summary>
        public void Show()
        {
            bannerClient.ShowBannerView();
        }

        /// <summary>
        /// Destroys the BannerView.
        /// </summary>
        public void Destroy()
        {
            bannerClient.DestroyBannerView();
        }

        private void ConfigureBannerEvents()
        {
            this.bannerClient.OnAdLoaded += (sender, args) =>
            {
                if (this.OnAdLoaded != null)
                {
                    this.OnAdLoaded(this, args);
                }
            };

            this.bannerClient.OnAdFailedToLoad += (sender, args) =>
            {
                if (this.OnAdFailedToLoad != null)
                {
                    this.OnAdFailedToLoad(this, args);
                }
            };

            this.bannerClient.OnAdClicked += (sender, args) =>
            {
                if (this.OnAdClicked != null)
                {
                    this.OnAdClicked(this, args);
                }
            };


        }
    }
}
