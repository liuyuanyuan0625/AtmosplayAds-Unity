package com.atmosplay.ads;

public interface UnityFloatAdListener {
    void onAdLoaded();
    void onAdFailed(String errorReason);
    void onAdStarted();
    void onAdRewarded();
    void onAdClicked();
    void OnAdVideoFinished();
    void onAdCloase();
}
