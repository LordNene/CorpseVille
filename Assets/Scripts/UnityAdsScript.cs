using System.Collections;
using UnityEngine;
using UnityEngine.Monetization;

public class UnityAdsScript : MonoBehaviour
{
    public static UnityAdsScript Instance;

    string gameId = "3137832";
    bool testMode = true;

    void Start()
    {
        Monetization.Initialize(gameId, testMode);
    }

    public string placementId = "video";

    public void ShowAd () {
        Debug.Log("Show Ad");
        StartCoroutine (ShowAdWhenReady());
    }

    private IEnumerator ShowAdWhenReady () {
        while (!Monetization.IsReady (placementId)) {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent (placementId) as ShowAdPlacementContent;

        if(ad != null) {
            ad.Show ();
        }
    }
}