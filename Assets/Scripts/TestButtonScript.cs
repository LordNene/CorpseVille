using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonScript : MonoBehaviour
{
    public Button VideoButton;
    public Button NoSkipVideoButton;
    public Button RewardVideoButton;

    // Start is called before the first frame update
    void Start()
    {
        VideoButton.onClick.AddListener(VideoClick);
        NoSkipVideoButton.onClick.AddListener(NoSkipVideoClick);
        RewardVideoButton.onClick.AddListener(RewardVideoClick);
    }

    public void VideoClick()
    {
        AdController.Instance.ShowVideoAd();
    }

    public void NoSkipVideoClick()
    {
        AdController.Instance.ShowNoSkipVideoAd();
    }

    public void RewardVideoClick()
    {
        if (AdController.Instance.IsReadyRewardedVideo())
        {
            AdController.Instance.ShowRewardedVideoAd((bool showed) =>
            {
                if (showed)
                {
                    Debug.Log("Ad OK");
                }
                else
                {
                    Debug.Log("Ad Fail");
                }
            });
        }
        else
        {
            Debug.Log("Not ready");
        }
    }
        


}
