using System;
using ZPLAYAds.Api;

namespace ZPLAYAds.Common
{
    public interface IBannerClient
    {
        // Ad event fired when the banner ad has been received.
        event EventHandler<EventArgs> OnAdLoaded;
        // Ad event fired when the banner ad has failed to load.
        event EventHandler<AdFailedEventArgs> OnAdFailedToLoad;
        // Ad event fired when the banner ad is clicked
        event EventHandler<EventArgs> OnAdClicked;

        // Creates a banner view and adds it to the view hierarchy.
        void CreateBannerView(string adAppId, string adUnitId, BannerViewOptions options);
        // Requests a new ad for the banner view.
        void LoadAd();

        // Shows the banner view on the screen.
        void ShowBannerView();

        // Hides the banner view from the screen.
        void HideBannerView();

        // Destroys a banner view.
        void DestroyBannerView();
    }
}
