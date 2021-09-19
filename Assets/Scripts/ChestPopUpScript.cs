using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestPopUpScript : MonoBehaviour
{
    public Button buttonOpen;
    public Button buttonClaim;
    public Button buttonClose;
    public Image ImagePlaceholder;
    public Text TextPlaceholder;
    public AtributesManagerScript AtributesManager;
    public AchievementManager AchievementManager;

    public Sprite openChestSprite;

    private int reward;

    // Start is called before the first frame update
    void Start()
    {
        buttonOpen.onClick.AddListener(RewardVideoClick);

        buttonClaim.onClick.AddListener(() =>
        {
            AchievementManager.OnTreasureFound();
            AtributesManager.increaseMoney(reward);
            Close();
        });

        buttonClose.onClick.AddListener(Close);
    }

    private void Close()
    {
        Destroy(gameObject);
    }

    private void RewardVideoClick()
    {
        if (AdController.Instance.IsReadyRewardedVideo())
        {
            AdController.Instance.ShowRewardedVideoAd((bool showed) =>
            {
                if (showed)
                {
                    buttonOpen.gameObject.SetActive(false);
                    buttonClaim.gameObject.SetActive(true);
                    reward = Random.Range(50, 250);
                    ImagePlaceholder.sprite = openChestSprite;
                    TextPlaceholder.text = $"You have found {reward} gold!";
                }
            });
        }
        else
        {
            Debug.Log("ChestPopUp: Ad not ready");
        }
    }
}
