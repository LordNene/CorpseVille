using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdController : MonoBehaviour
{
    public static AdController Instance;

    bool testMode = true;
    private string gpStoreId = "3137832";

    private string videoAd = "video";
    private string noSkipVideoAd = "rewardedVideo";

    private Action<bool> _callbackOnRewarded;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        Monetization.Initialize(gpStoreId, testMode);
    }

    public void ShowVideoAd()
    {
        StartCoroutine(ShowVideoAdWhenReady());
    }

    public void ShowRewardedVideoAd(Action<bool> callBack)
    {
        _callbackOnRewarded = callBack;
        StartCoroutine(ShowRewardedVideoAdWhenReady());
    }

    public void ShowNoSkipVideoAd()
    {
        StartCoroutine(ShowNoSkipVideoAdWhenReady());
    }

    private IEnumerator ShowVideoAdWhenReady()
    {
        while (!Monetization.IsReady(videoAd))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(videoAd) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show();
        }
    }

    private IEnumerator ShowNoSkipVideoAdWhenReady()
    {
        while (!Monetization.IsReady(noSkipVideoAd))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(noSkipVideoAd) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show();
        }
    }


    private IEnumerator ShowRewardedVideoAdWhenReady()
    {
        while (!Monetization.IsReady(videoAd))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(videoAd) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(options);
        }
    }


    public bool IsReadyRewardedVideo()
    {
        return Monetization.IsReady(videoAd);
    }


    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            // Reward the player
            _callbackOnRewarded(true);
        }
        else if (result == ShowResult.Skipped)
        {
            _callbackOnRewarded(false);
        }
        else if (result == ShowResult.Failed)
        {
            _callbackOnRewarded(false);
        }
    }
}
