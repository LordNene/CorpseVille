using System;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour
{
    public Text TitlePlaceholder;
    public Text DescriptionPlaceholder;
    public Image ImagePlaceholder;
    public Button RewardButton;
    public Image ProgressImage;
    public Text ProgressText;
    public Image RewardImage;
    public Text RewardText;

    public string Title;
    public string Description;
    public bool ResetOnNewLevel;
    public Sprite Image;
    public Sprite GoldImage;
    public Sprite XpImage;
    public RewardType RewardType;

    public int Counter { get; set; }
    public int Level { get; set; }
    public bool PendingReward { get; set; }

    private int[] targets;
    private int[] rewards;

    public UIScript UIScript;
    private AtributesManagerScript atributesManager;

    void Start()
    {
        atributesManager = FindObjectOfType<AtributesManagerScript>();

        DescriptionPlaceholder.text = Description;
        ImagePlaceholder.sprite = Image;
        RewardImage.sprite = RewardType == RewardType.Gold ? GoldImage : XpImage;
    }

    public void Init(int[] targets, int[] rewards, int counter = 0, int level = 0, bool pendingReward = false)
    {
        this.targets = targets;
        this.rewards = rewards;

        RewardButton.onClick.AddListener(delegate
        {
            //AdController.Instance.ShowVideoAd(); //todo use rewarded video
            GiveReward();
            RewardButton.gameObject.SetActive(false);
        });

        Counter = counter;
        Level = level;
        PendingReward = pendingReward;

        RewardButton.gameObject.SetActive(pendingReward);

        RewardText.text = $"{rewards[Level]}";
        TitlePlaceholder.text = Title + $"{Environment.NewLine} Lvl {Level + 1}";
        ProgressText.text = $"{Counter}/{targets[Level]}";
        ProgressImage.fillAmount = Counter * 1.0f / targets[Level];
    }

    public void UpdateProgress(int increment = 1)
    {
        if (PendingReward || targets == null || Level == targets.Length) return; //achievement can't move to another level until the reward is collected
        Counter += increment;
        if (Counter < 0) Counter = 0; //can this even happen?

        ProgressText.text = $"{Counter}/{targets[Level]}";
        ProgressImage.fillAmount = Counter * 1.0f / targets[Level];

        if (Counter >= targets[Level])
        {
            Win();
        }
    }

    private void Win()
    {
        Level++;
        PendingReward = true;
        RewardButton.gameObject.SetActive(true);
        UIScript.DisplayToolTip($"Collect your reward for {Title} Lvl {Level + 1}");
    }

    private void GiveReward()
    {
        switch (RewardType)
        {
            case RewardType.Gold:
                atributesManager.increaseMoney(rewards[Level]);
                break;
            case RewardType.Experience:
                atributesManager.EarnXP(rewards[Level]);
                break;
        }
        UIScript.DisplayToolTip($"You have earned {rewards[Level-1]} " + (RewardType == RewardType.Gold ? "Gold!" : "XP!"));
        if (ResetOnNewLevel) Counter = 0;
        RewardText.text = $"{rewards[Level]}";
        TitlePlaceholder.text = Title + $"{Environment.NewLine} Lvl {Level + 1}";
        ProgressText.text = $"{Counter}/{targets[Level]}";
        ProgressImage.fillAmount = Counter * 1.0f / targets[Level];
        PendingReward = false;
    }
}
