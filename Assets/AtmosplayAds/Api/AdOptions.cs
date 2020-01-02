namespace AtmosplayAds
{
    public class AdOptions
    {
        internal string mChannelId;
        internal bool isAutoLoad;

        internal AdOptions(AdOptionsBuilder builder)
        {
            mChannelId = builder.mChannelId;
            isAutoLoad = builder.isAutoLoad;
        }
    }

    public class AdOptionsBuilder
    {
        internal string mChannelId = "";
        internal bool isAutoLoad = true;

        public AdOptionsBuilder()
        {
        }

        // Sets channel according to your settings on the platform, default value is empty string.
        public AdOptionsBuilder SetChannelId(string channelId)
        {
            mChannelId = channelId;
            return this;
        }

        // Autoload next advertising when showing an ad, default value is true.
        public AdOptionsBuilder SetAutoLoadNext(bool autoLoad)
        {
            isAutoLoad = autoLoad;
            return this;
        }

        // Creates AdOptions instance.
        public AdOptions build()
        {
            return new AdOptions(this);
        }
    }
}
