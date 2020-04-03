package com.atmosplay.ads;

import android.app.Activity;
import android.util.Log;

import com.atmosplayads.AtmosplayWindowAd;
import com.atmosplayads.listener.WindowAdListener;

public class WindowAd {
    private static final String TAG = "WindowAd";
    private Activity mActivity;
    private UnityWindowAdListener adListener;
    private AtmosplayWindowAd mWindowAd;

    public WindowAd(Activity activity, String appId, String unitId, UnityWindowAdListener adListener) {
        Log.d(TAG, "init: " + appId);
        this.mActivity = activity;
        this.adListener = adListener;
        if (mWindowAd != null) {
            mWindowAd.destroy();
        }
        mWindowAd = new AtmosplayWindowAd(activity, appId, unitId);
        mWindowAd.setWindowAdListener(newWindowAdListener());
    }

    public void setChannelId(final String channelId) {
        Log.d(TAG, "setChannelId: " + channelId);
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mWindowAd.setChannelId(channelId);
            }
        });
    }

    public boolean isLoaded() {
        Log.d(TAG, "isLoaded: " + mWindowAd.isReady());
        return mWindowAd.isReady();
    }


    public void setPointAndWidth(final int mPointX, final int mPointY, final int width) {
        Log.d(TAG, "setPointAndWidth mPointX: " + mPointX + ", mPointY: " + mPointY + ", width: " + width);
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mWindowAd.setPointAndWidth(mPointX, mPointY, width);
            }
        });
    }


    public void show() {
        Log.d(TAG, "show ");
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mWindowAd.isReady()) {
                    mWindowAd.show(mActivity);
                }
            }
        });
    }

    private WindowAdListener newWindowAdListener() {
        return new WindowAdListener() {
            @Override
            public void onWindowAdPrepared() {
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
            public void onWindowAdPreparedFailed(int code, final String error) {
                if (adListener != null) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            if (adListener != null) {
                                adListener.onAdFailed(error);
                            }
                        }
                    }).start();
                }
            }

            @Override
            public void onWindowAdStart() {
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
            public void onWindowAdFinished() {
                if (adListener != null) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            if (adListener != null) {
                                adListener.onAdFinished();
                            }
                        }
                    }).start();
                }
            }

            @Override
            public void onWindowAdClose() {
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
            public void onWindowAdClicked() {
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
        };
    }

    public void updatePointAndWidth(final int mPointX, final int mPointY, final int width) {
        Log.d(TAG, "updatePointAndWidth mPointX: " + mPointX + ", mPointY: " + mPointY + ", width: " + width);
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mWindowAd != null) {
                    mWindowAd.updatePointAndWidth(mActivity, mPointX, mPointY, width);
                }
            }
        });
    }

    public void hidden() {
        Log.d(TAG, "hidden");
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mWindowAd != null) {
                    mWindowAd.hiddenWindowAd();
                }
            }
        });
    }

    public void showAgainAfterHiding() {
        Log.d(TAG, "showAgainAfterHiding");
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mWindowAd != null) {
                    mWindowAd.showAgainAfterHiding();
                }
            }
        });
    }

    public void onDestroy() {
        Log.d(TAG, "onDestroy");
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mWindowAd != null) {
                    mWindowAd.destroy();
                }
            }
        });
    }

}
