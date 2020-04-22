using System;
using System.Collections;
using System.Collections.Generic;
using AtmosplayAds;
using AtmosplayAds.Api;
using AtmosplayAds.Common;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AtmosplayWindowAdSceneScript : MonoBehaviour
{
    public InputField pointX;
    public InputField pointY;
    public InputField width;
    public InputField angle;
    public GameObject windowAdView;
    public Text statusText;
    WindowAd windowAd;

    void Start()
    {
        AdOptions adOptions = new AdOptionsBuilder()
            .SetChannelId(GlobleSettings.GetChannelId)
            .build();

        windowAd = new WindowAd(GlobleSettings.GetAppID, GlobleSettings.GetWindowAdUnitID, gameObject, adOptions);
        windowAd.OnAdLoaded += HandleWindowAdLoaded;
        windowAd.OnAdFailedToLoad += HandleWindowAdFailedToLoad;
        windowAd.OnAdStarted += HandleWindowAdStart;
        windowAd.OnAdClicked += HandleWindowAdClicked;
        windowAd.OnAdFinished += HandleWindowAdFinished;
        windowAd.OnAdClosed += HandleWindowAdClosed;
        windowAd.OnAdFailToShow += HandleWindowAdFailToShow;
    }

    public void showWindowAd()
    {
        statusText.text = "showWindowAd";
        if (windowAd != null)
        {
            windowAd.SetPointAndWidth(windowAdView.transform);
            windowAd.Show();
        }
    }

    public void setAngle()
    {
        int a = 0;
        if (angle.text != null)
        {
            a = int.Parse(angle.text);
            windowAd.SetAngle(a);
        }
    }

    public void setPositionAndWidth()
    {
        float x = 0;
        float y = 0;
        float w = 0;
        if (pointX.text != null)
        {
            x = float.Parse(pointX.text);
        }

        if (pointY.text != null)
        {
            y = float.Parse(pointY.text);
        }

        if (width.text != null)
        {
            w = float.Parse(width.text);
        }

        windowAdView.transform.position = new Vector3(x, y, 200);
        windowAdView.GetComponent<RectTransform>().sizeDelta = new Vector2(w, w);
    }

    public void UpdatePointAndWidth()
    {
        if (windowAd != null)
        {
            setPositionAndWidth();
            windowAd.UpdatePointAndWidth(windowAdView.transform);
        }
    }

    public void isReady()
    {
        if (windowAd != null)
        {
            windowAd.IsReady();
            statusText.text = "isReady: " + windowAd.IsReady();
        }
    }

    public void hidden()
    {
        statusText.text = "hidden";
        if (windowAd != null)
        {
            windowAd.Hidden();
        }
    }

    public void ShowAgainAfterHiding()
    {
        statusText.text = "ShowAgainAfterHiding";
        if (windowAd != null)
        {
            windowAd.ShowAgainAfterHiding();
        }
    }

    public void Destroy()
    {
        if (windowAd != null)
        {
            windowAd.Destroy();
        }
        SceneManager.LoadScene("MainScene");
    }


    #region WindowAd callback handlers
    public void HandleWindowAdLoaded(object sender, EventArgs args)
    {
        statusText.text = "HandleWindowAdLoaded";
        print("atmosplay---HandleWindowAdLoaded");
    }

    public void HandleWindowAdFailedToLoad(object sender, AdFailedEventArgs args)
    {
        statusText.text = "HandleWindowAdFailedToLoad: " + args.Message;
        print("atmosplay---HandleWindowAdFailedToLoad:" + args.Message);
    }

    public void HandleWindowAdStart(object sender, EventArgs args)
    {
        statusText.text = "HandleWindowAdStart";
        print("atmosplay---HandleWindowAdStart");
    }

    public void HandleWindowAdClicked(object sender, EventArgs args)
    {
        statusText.text = "HandleWindowAdClicked";
        print("atmosplay---HandleWindowAdClicked");
    }


    public void HandleWindowAdFinished(object sender, EventArgs args)
    {
        statusText.text = "HandleWindowAdFinished";
        print("atmosplay---HandleWindowAdFinished");
    }


    public void HandleWindowAdClosed(object sender, EventArgs args)
    {
        statusText.text = "HandleWindowAdClosed";
        print("atmosplay---HandleWindowAdClosed");
    }

    public void HandleWindowAdFailToShow(object sender, EventArgs args)
    {
        statusText.text = "HandleWindowAdFailToShow";
        print("atmosplay---HandleWindowAdFailToShow");
    }
    #endregion
}
