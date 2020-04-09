package com.atmosplay.ads;

import android.app.Activity;
import android.util.Log;

import com.atmosplayads.AtmosplayFloatAd;
import com.atmosplayads.listener.AtmosplayAdLoadListener;
import com.atmosplayads.listener.FloatAdListener;

public class FloatAd {
    private static final String TAG = "FloatAd";
    private Activity mActivity;
    private UnityFloatAdListener adListener;
    private AtmosplayFloatAd mFloatAd;

    public FloatAd(Activity activity, String appId, UnityFloatAdListener adListener) {
        Log.d(TAG, "init: " + appId);
        this.mActivity = activity;
        this.adListener = adListener;
        mFloatAd = AtmosplayFloatAd.init(activity, appId);
    }

    public void loadAd(final String unitId) {
        Log.d(TAG, "loadAd: " + unitId);
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mFloatAd.loadAd(unitId, newRequestListener());
            }
        });
    }

    public boolean isLoaded(final String unitId) {
        Log.d(TAG, "isLoaded: " + unitId);
        return mFloatAd.isReady(unitId);
    }


    public void setPointAndWidth(final int mPointX, final int mPointY, final int width) {
        Log.d(TAG, "setPointAndWidth mPointX: " + mPointX + ", mPointY: " + mPointY + ", width: " + width);
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mFloatAd.setPointAndWidth(mPointX, mPointY, width);
            }
        });
    }


    public void show(final String unitId) {
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mFloatAd.isReady(unitId)) {
                    mFloatAd.show(mActivity, unitId, newFloatAdListener());
                }
            }
        });
    }

    public void setChannelId(final String channelId) {
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mFloatAd.setChannelId(channelId);
            }
        });
    }

    public void setAutoloadNext(final boolean autoLoad) {
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mFloatAd.setAutoLoadAd(autoLoad);
            }
        });
    }


    private AtmosplayAdLoadListener newRequestListener() {

        return new AtmosplayAdLoadListener() {

            @Override
            public void onLoadFinished() {
                if (adListener != null) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            if (adListener != null) {
                                adListener.onAdLoaded();
                            }
                        }
                    }).start();
                }
            }

            @Override
            public void onLoadFailed(int errorCode, final String msg) {
                if (adListener != null) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            if (adListener != null) {
                                adListener.onAdFailed(msg);
                            }
                        }
                    }).start();
                }
            }
        };
    }


    private FloatAdListener newFloatAdListener() {
        return new FloatAdListener() {

            @Override
            public void onFloatAdStartPlaying() {
                if (adListener != null) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            if (adListener != null) {
                                adListener.onAdStarted();
                            }
                        }
                    }).start();
                }
            }

            @Override
            public void onFloatAdEndPlaying() {
                if (adListener != null) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            if (adListener != null) {
                                adListener.OnAdVideoFinished();
                            }
                        }
                    }).start();
                }
            }

            @Override
            public void onUserEarnedReward() {
                if (adListener != null) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            if (adListener != null) {
                                adListener.onAdRewarded();
                            }
                        }
                    }).start();
                }
            }

            @Override
            public void onFloatAdClicked() {
                if (adListener != null) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            if (adListener != null) {
                                adListener.onAdClicked();
                            }
                        }
                    }).start();
                }
            }

            @Override
            public void onFloatAdClosed() {
                if (adListener != null) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            if (adListener != null) {
                                adListener.onAdCloase();
                            }
                        }
                    }).start();
                }
            }

            @Override
            public void onFloatAdError(int code, final String msg) {
                if (adListener != null) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            if (adListener != null) {
                                adListener.onAdFailed(msg);
                            }
                        }
                    }).start();
                }
            }
        };
    }

    public void updatePointAndWidth(final int mPointX, final int mPointY, final int width) {
        Log.d(TAG, "updatePointAndWidth mPointX: " + mPointX + ", mPointY: " + mPointY + ", width: " + width);
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mFloatAd != null) {
                    mFloatAd.updatePointAndWidth(mActivity, mPointX, mPointY, width);
                }
            }
        });
    }

    public void hidden() {
        Log.d(TAG, "hidden");
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mFloatAd != null) {
                    mFloatAd.hiddenFloatAd();
                }
            }
        });
    }

    public void showAgainAfterHiding() {
        Log.d(TAG, "showAgainAfterHiding");
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mFloatAd != null) {
                    mFloatAd.showAgainAfterHiding();
                }
            }
        });
    }

    public void onDestroy() {
        Log.d(TAG, "onDestroy");
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mFloatAd != null) {
                    mFloatAd.destroy();
                }
            }
        });
    }

}
