/*
 * Copyright (C) 2016 Google, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package com.atmosplay.ads;


import android.app.Activity;

import com.atmosplayads.AtmosplayRewardVideo;
import com.atmosplayads.listener.AtmosplayAdListener;
import com.atmosplayads.listener.AtmosplayAdLoadListener;
import com.atmosplayads.listener.SimpleAtmosplayAdListener;


public class RewardVideo {
    private AtmosplayRewardVideo rewardVideo;
    private Activity activity;
    private UnityRewardVideoAdListener adListener;

    public RewardVideo(Activity activity, String appId, UnityRewardVideoAdListener adListener) {
        this.activity = activity;
        this.adListener = adListener;
        rewardVideo = AtmosplayRewardVideo.init(activity, appId);
    }

    public void loadAd(final String unitId) {
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                rewardVideo.loadAd(unitId, newRequestListener());
            }
        });
    }

    public boolean isLoaded(final String unitId) {
        return rewardVideo.isReady(unitId);
    }

    public void show(final String unitId) {
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (rewardVideo.isReady(unitId)) {
                    rewardVideo.show(unitId, newPlayListener());
                }
            }
        });
    }

    public void setChannelId(final String channelId){
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                rewardVideo.setChannelId(channelId);
            }
        });
    }

    public void setAutoloadNext(final boolean autoLoad) {
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                rewardVideo.setAutoLoadAd(autoLoad);
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

    private AtmosplayAdListener newPlayListener() {
        return new SimpleAtmosplayAdListener() {

            @Override
            public void onVideoStart() {
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
            public void onVideoFinished() {
                if (adListener != null) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            if (adListener != null) {
                                adListener.onAdVideoCompleted();
                            }
                        }
                    }).start();
                }
            }

            @Override
            public void onLandingPageInstallBtnClicked() {
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
            public void onAdClosed() {
                if (adListener != null) {
                    new Thread(new Runnable() {
                        @Override
                        public void run() {
                            if (adListener != null) {
                                adListener.onAdCompleted();
                            }
                        }
                    }).start();
                }
            }

            @Override
            public void onAdsError(int code, final String msg) {
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
}