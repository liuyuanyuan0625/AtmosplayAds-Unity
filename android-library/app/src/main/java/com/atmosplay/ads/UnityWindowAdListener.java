package com.atmosplay.ads;

public interface UnityWindowAdListener {
    void onAdLoaded();
    void onAdFailed(String errorReason);
    void onAdStarted();
    void onAdFinished();
    void onAdClicked();
    void onAdCloase();
}
