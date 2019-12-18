using System;
namespace ZPLAYAds.Common
{
    public class BannerViewOptions
    {
        /// <summary>
        ///  banner position in supview
        /// </summary>
        /// <value>The ad position.</value>
        public AdPosition adPosition { get; private set; }
        /// <summary>
        /// Gets the size of the banner.
        /// </summary>
        /// <value>The size of the banner.</value>
        public BannerAdSize bannerSize { get; private set; }
        /// <summary>
        /// channelID
        /// </summary>
        public string channelID { get; private set; }

        internal BannerViewOptions(BannerViewOptionsBuilder builder)
        {
            adPosition = builder.AdPosition;
            bannerSize = builder.BannerSize;
            channelID = builder.ChannelID;
        }
    }

    public class BannerViewOptionsBuilder
    {
        public BannerViewOptionsBuilder()
        {
            AdPosition = AdPosition.BOTTOM;
            BannerSize = BannerAdSize.BANNER_AD_SIZE_320x50;
            ChannelID = null;
        }
        internal AdPosition AdPosition { get; private set; }
        internal BannerAdSize BannerSize { get; private set; }
        internal string ChannelID { get; private set; }
        /// <summary>
        /// Build default instance.
        /// </summary>
        /// <returns>Builder instance.</returns>
        public BannerViewOptions Build()
        {
            return new BannerViewOptions(this);
        }
        /// <summary>
        /// Sets the banner ad position.
        /// </summary>
        /// <returns>Builder instance.</returns>
        /// <param name="adPosition">Ad position.</param>
        public BannerViewOptionsBuilder setAdPosition(AdPosition adPosition)
        {
            AdPosition = adPosition;
            return this;
        }
        /// <summary>
        /// Sets the size of the banner.
        /// </summary>
        /// <returns>Builder instance.</returns>
        /// <param name="bannerSize">Banner size.</param>
        public BannerViewOptionsBuilder setBannerSize(BannerAdSize bannerSize)
        {
            BannerSize = bannerSize;
            return this;
        }
       /// <summary>
       /// set channel id 
       /// </summary>
       /// <param name="channelID"></param>
       /// <returns></returns>
        public BannerViewOptionsBuilder setChannelID(string channelID)
        {
            ChannelID = channelID;
            return this;
        }

    }

    public enum BannerAdSize
    {
        /// <summary>
        /// iPhone and iPod Touch ad size. Typically 320x50.
        /// </summary>
        BANNER_AD_SIZE_320x50,
        /// <summary>
        /// Leaderboard size for the iPad. Typically 728x90.
        /// </summary>
        BANNER_AD_SIZE_728x90,
        /// <summary>
        /// An ad size that spans the full width of the application in portrait orientation. The height is
        /// typically 50 pixels on an iPhone/iPod UI, and 90 pixels tall on an iPad UI.
        /// </summary>
        BANNER_AD_SIZE_SMART_PORTRAIT,
        /// <summary>
        ///  An ad size that spans the full width of the application in landscape orientation. The height is
        /// typically 32 pixels on an iPhone/iPod UI, and 90 pixels tall on an iPad UI.
        /// </summary>
        BANNER_AD_SIZE_SMART_LANDSCAPE
    }

    public enum AdPosition
    {
        TOP = 0,
        BOTTOM = 1,
    }
 }
