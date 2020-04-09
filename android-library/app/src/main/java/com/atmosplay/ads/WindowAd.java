package com.atmosplay.ads;

import android.app.Activity;
import android.content.pm.ActivityInfo;
import android.content.pm.PackageManager;
import android.util.Log;

import com.atmosplayads.AtmosplayWindowAd;
import com.atmosplayads.listener.WindowAdListener;

public class WindowAd {
    private static final String TAG = "WindowAd";
    private Activity mActivity;
    private UnityWindowAdListener adListener;
    private AtmosplayWindowAd mWindowAd;

    public WindowAd(Activity activity, String appId, String unitId, final UnityWindowAdListener adListener) {
        Log.d(TAG, "init: " + appId);
        this.mActivity = activity;
        this.adListener = adListener;
        if (mWindowAd != null) {
            mWindowAd.destroy();
        }
        Log.d(TAG, "hardwareAccelerated: " + hasHardwareAcceleration(activity));
        if (!hasHardwareAcceleration(activity)) {
            if (adListener != null) {
                new Thread(new Runnable() {
                    @Override
                    public void run() {
                        if (adListener != null) {
                            adListener.onAdFailed("WiodnwAd need hardwareAccelerated, please at manifets set UnityPlayerActivity hardwareAccelerated is true ");
                        }
                    }
                }).start();
            }
            return;
        }
        mWindowAd = new AtmosplayWindowAd(activity, appId, unitId);
        mWindowAd.setWindowAdListener(newWindowAdListener());
    }

    public void setChannelId(final String channelId) {
        Log.d(TAG, "setChannelId: " + channelId);
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mWindowAd != null) {
                    mWindowAd.setChannelId(channelId);
                }
            }
        });
    }

    public boolean isLoaded() {
        Log.d(TAG, "isLoaded: " + mWindowAd.isReady());
        if (mWindowAd == null) {
            return false;
        }
        return mWindowAd.isReady();
    }


    public void setPointAndWidth(final int mPointX, final int mPointY, final int width) {
        Log.d(TAG, "setPointAndWidth mPointX: " + mPointX + ", mPointY: " + mPointY + ", width: " + width);
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mWindowAd != null) {
                    mWindowAd.setPointAndWidth(mPointX, mPointY, width);
                }
            }
        });
    }


    public void show() {
        Log.d(TAG, "show ");
        mActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mWindowAd != null) {
                    if (mWindowAd.isReady()) {
                        mWindowAd.show(mActivity);
                    }
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

    // https://stackoverflow.com/a/18595681/7785373
    private static boolean hasHardwareAcceleration(Activity activity) {
        // Has HW acceleration been enabled manually in the current window?

        // Window window = activity.getWindow();
//        Log.d(TAG,"----window.getAttributes().flags: " + window.getAttributes().flags);
//        Log.d(TAG,"----WindowManager.LayoutParams.FLAG_HARDWARE_ACCELERATED: " + WindowManager.LayoutParams.FLAG_HARDWARE_ACCELERATED);
//        if (window != null) {
//            if ((window.getAttributes().flags
//                    & WindowManager.LayoutParams.FLAG_HARDWARE_ACCELERATED) != 0) {
//                return true;
//            }
//        }

        // Has HW acceleration been enabled in the manifest?
        try {
            ActivityInfo info = activity.getPackageManager().getActivityInfo(
                    activity.getComponentName(), 0);
            if ((info.flags & ActivityInfo.FLAG_HARDWARE_ACCELERATED) != 0) {
                return true;
            }
        } catch (PackageManager.NameNotFoundException e) {
            Log.e("Chrome", "getActivityInfo(self) should not fail");
        }
        return false;
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
