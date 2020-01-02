package com.atmosplay.ads;

/**
 * Description:
 * <p>
 * Created by lgd on 2019-10-31.
 */
public interface UnityBannerAdListener {
    void onAdLoaded();

    void onAdFailed(String errorReason);

    void onAdClicked();
}
