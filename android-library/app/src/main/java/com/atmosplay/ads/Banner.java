package com.atmosplay.ads;

import android.app.Activity;
import android.text.TextUtils;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.ViewGroup;
import android.widget.FrameLayout;

import com.atmosplayads.AtmosplayBanner;
import com.atmosplayads.entity.BannerSize;
import com.atmosplayads.listener.BannerListener;
import com.atmosplayads.presenter.widget.AtmosBannerView;

import static android.view.ViewGroup.LayoutParams.WRAP_CONTENT;

/**
 * Description:
 * <p>
 * Created by lgd on 2019-10-31.
 */
public class Banner {
    private static final String TAG = "Banner";

    private static final String POSITION_TOP = "TOP";
    private static final String BANNER_SIZE_320X50 = "BANNER_AD_SIZE_320x50";
    private static final String BANNER_SIZE_728X90 = "BANNER_AD_SIZE_728x90";

    private final Activity activity;
    private final UnityBannerAdListener adListener;
    private AtmosplayBanner mBanner;
    private View mBannerView;
    private int mPosition = Gravity.BOTTOM;

    public Banner(Activity activity, UnityBannerAdListener adListener) {
        this.activity = activity;
        this.adListener = adListener;
    }

    public void createBanner(final String appId, final String unitId,
                             final String channelId,
                             final String bannerSize,
                             final String position) {
        if (activity == null) {
            throw new IllegalStateException("AtmosplayAdsBanner need context to initialize.");
        }
        mPosition = getPosition(position);
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mBanner = new AtmosplayBanner(activity, appId, unitId);
                mBanner.setChannelId(channelId);
                mBanner.setBannerSize(getBannerSize(bannerSize));
                mBanner.setBannerListener(new BannerListener() {
                    @Override
                    public void onBannerPrepared(AtmosBannerView atmosBannerView) {
                        removeView(mBannerView);
                        mBannerView = atmosBannerView;
                        new Thread(new Runnable() {
                            @Override
                            public void run() {
                                adListener.onAdLoaded();
                            }
                        }).start();
                    }

                    @Override
                    public void onBannerPreparedFailed(int i, final String s) {
                        Log.i(TAG, "onBannerPreparedFailed: " + s);
                        new Thread(new Runnable() {
                            @Override
                            public void run() {
                                adListener.onAdFailed(s);
                            }
                        }).start();
                    }

                    @Override
                    public void onBannerClicked() {
                        new Thread(new Runnable() {
                            @Override
                            public void run() {
                                adListener.onAdClicked();
                            }
                        }).start();
                    }
                });
            }
        });
    }

    private int getPosition(String position) {
        if (TextUtils.equals(position, POSITION_TOP)) {
            return Gravity.TOP;
        } else {
            return Gravity.BOTTOM;
        }
    }

    private BannerSize getBannerSize(String size) {
        if (TextUtils.equals(size, BANNER_SIZE_320X50)) {
            return BannerSize.BANNER_320x50;
        } else if (TextUtils.equals(size, BANNER_SIZE_728X90)) {
            return BannerSize.BANNER_728x90;
        } else {
            return BannerSize.SMART_BANNER;
        }
    }

    public void loadAd() {
        if (isNotInitialized()) {
            return;
        }
        Log.d(TAG, "loadAd: ");
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mBanner.loadAd();
            }
        });
    }

    public void showBannerView() {
        if (isNotInitialized()) {
            return;
        }
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mBannerView == null) {
                    return;
                }

                if (mBannerView.getParent() instanceof ViewGroup) {
                    mBannerView.setVisibility(View.VISIBLE);
                    return;
                }

                FrameLayout.LayoutParams params = new FrameLayout.LayoutParams(WRAP_CONTENT, WRAP_CONTENT);
                params.gravity = mPosition | Gravity.CENTER_HORIZONTAL;
                activity.addContentView(mBannerView, params);
            }
        });
    }

    public void hideBannerView() {
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mBannerView == null) {
                    return;
                }
                mBannerView.setVisibility(View.GONE);
            }
        });
    }

    public void destroyBannerView() {
        removeView(mBannerView);
        mBanner.destroy();
    }


    private void removeView(View view) {
        if (view == null) {
            return;
        }

        if (view.getParent() instanceof ViewGroup) {
            ((ViewGroup) view.getParent()).removeView(view);
        }
    }

    private boolean isNotInitialized() {
        return mBanner == null;
    }
}
