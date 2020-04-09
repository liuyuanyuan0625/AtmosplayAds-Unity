using System;
using System.Collections;
using System.Collections.Generic;
using AtmosplayAds;
using AtmosplayAds.Api;
using AtmosplayAds.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AtmosplayFloatAdSceneScript : MonoBehaviour
{
    public InputField pointX;
    public InputField pointY;
    public InputField width;
    public GameObject floatAdView;
    public Text statusText;
    FloatAd floatAd;


    public void init() {
        AdOptions adOptions = new AdOptionsBuilder()
            .SetChannelId(GlobleSettings.GetChannelId)
            .SetAutoLoadNext(GlobleSettings.IsAutoload)
            .build();

        floatAd = new FloatAd(GlobleSettings.GetAppID, GlobleSettings.GetFloatAdUnitID, gameObject, adOptions);
        floatAd.OnAdLoaded += HandleFloatAdLoaded;
        floatAd.OnAdFailedToLoad += HandleFloatAdFailedToLoad;
        floatAd.OnAdStarted += HandleFloatAdStart;
        floatAd.OnAdClicked += HandleFloatAdClicked;
        floatAd.OnAdRewarded += HandleFloatAdRewarded;
        floatAd.OnAdClosed += HandleFloatAdClosed;
    }

    public void showFloatAd() {
        statusText.text = "showFloatAd";
        if (floatAd != null)
        {
            floatAd.SetPointAndWidth(floatAdView.transform);
            floatAd.Show(GlobleSettings.GetFloatAdUnitID);
        }
    }


    public void setPositionAndWidth() {
        float x = 0;
        float y = 0;
        float w = 0;
        if (pointX.text != null) {
            x = float.Parse(pointX.text);
        }

        if (pointY.text != null)
        {
            y = float.Parse(pointY.text);
        }

        if (width.text != null) {
            w = float.Parse(width.text);
        }
        
        floatAdView.transform.position = new Vector3(x, y, 200);
        floatAdView.GetComponent<RectTransform>().sizeDelta = new Vector2(w, w);
    }

    public void UpdatePointAndWidth() {
        if (floatAd != null)
        {
            setPositionAndWidth();
            floatAd.UpdatePointAndWidth(floatAdView.transform);
        }
    }

    public void isReady()
    {
        if (floatAd != null)
        {
            floatAd.IsReady(GlobleSettings.GetFloatAdUnitID);
            statusText.text = "isReady: " + floatAd.IsReady(GlobleSettings.GetFloatAdUnitID);
        }
    }

    public void hidden() {
        statusText.text = "hidden";
        if (floatAd != null)
        {
            floatAd.Hidden();
        }
    }

    public void ShowAgainAfterHiding()
    {
        statusText.text = "ShowAgainAfterHiding";
        if (floatAd != null)
        {
            floatAd.ShowAgainAfterHiding();
        }
    }

    public void Destroy()
    {
        print("atmosplay---Destroy");
        if (floatAd != null)
        {
            floatAd.Destroy();
        }
        SceneManager.LoadScene("MainScene");
    }


    #region FloatAd callback handlers
    public void HandleFloatAdLoaded(object sender, EventArgs args)
    {
        statusText.text = "HandleFloatAdLoaded";
        print("atmosplay---HandleFloatAdLoaded");
    }

    public void HandleFloatAdFailedToLoad(object sender, AdFailedEventArgs args)
    {
        statusText.text = "HandleFloatAdFailedToLoad: " + args.Message;
        print("atmosplay---HandleFloatAdFailedToLoad:" + args.Message);
    }

    public void HandleFloatAdStart(object sender, EventArgs args)
    {
        statusText.text = "HandleFloatAdStart";
        print("atmosplay---HandleFloatAdStart");
    }

    public void HandleFloatAdClicked(object sender, EventArgs args)
    {
        statusText.text = "HandleFloatAdClicked";
        print("atmosplay---HandleFloatAdClicked");
    }


    public void HandleFloatAdRewarded(object sender, EventArgs args)
    {
        statusText.text = "HandleFloatAdRewarded";
        print("atmosplay---HandleFloatAdRewarded");
    }


    public void HandleFloatAdClosed(object sender, EventArgs args)
    {
        statusText.text = "HandleFloatAdClosed";
        print("atmosplay---HandleFloatAdClosed");
    }

    #endregion
}
