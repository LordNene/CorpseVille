using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public AtributesManagerScript AtributesScript;

    //Stat panel components
    public Text MoneyText;
    public Text StockText;

    //Main building power ups
    public Button KillerButton;
    public Button ZombieSpawnSlowButton;
    public Button PoisonButton;
    private int ZombieSpawnSlowTimer;
    private int PoisonTimer;
    private WaitForSeconds PowerUpsWaitForSeconds;
    public Text ZombieSpawnSlowText;
    public Text PoisonText;
    public Image ZombieSpawnSlowPanel;
    public Image PoisonPanel;

    //Main menu
    public Button MainMenuButton;
    public GameObject MainMenuWindow;

    //Tools
    public Button DigButton;
    public Button CorpseButton;
    public Button BuildButton;

    //Tooltip - used to give a hint to player
    public GameObject ToolTip;

    //Build menu window
    public GameObject BuildMenuWindow;

    //helpers
    private WaitForSeconds WaitForSeconds;
    private Color selected;
    private Color unselected;
    private Button[] toolButtons;
    private string[] toolNames;

    void Start()
    {
        #region helpers set up
        WaitForSeconds = new WaitForSeconds(2);
        PowerUpsWaitForSeconds = new WaitForSeconds(1);
        selected = new Color(0, 0, 0, 1);
        unselected = new Color(0, 0, 0, (float).4);
        toolButtons = new Button[] { DigButton, CorpseButton, BuildButton };
        toolNames = new string[] { "Dig", "Corpse", "Build" };

        #endregion

        #region Buttons setup
        MainMenuButton.onClick.AddListener(ShowMainMenu);

        DigButton.onClick.AddListener(delegate { SelectToolButton(0); });

        CorpseButton.onClick.AddListener(delegate { SelectToolButton(1); });

        BuildButton.onClick.AddListener(delegate 
        {
            var animator = BuildMenuWindow.GetComponent<Animator>();
            var isOpen = animator.GetBool("Open");
            animator.SetBool("Open", !isOpen);

            //when closing build window, select dig tool automatically
            if (isOpen)
            {
                SelectToolButton(0);
            }
            else
            {
                SelectToolButton(2);
            }
        });


        KillerButton.onClick.AddListener(delegate
        {
            AtributesScript.IsKiller = true;
            //AtributesScript.decreaseMoney(300);
            AdController.Instance.ShowNoSkipVideoAd();
        });

        ZombieSpawnSlowButton.onClick.AddListener(delegate
        {
            AtributesScript.IsHalfZombieSpawn = true;
            //AtributesScript.decreaseMoney(300);
            ZombieSpawnSlowTimer = 300;
            ZombieSpawnSlowPanel.gameObject.SetActive(true);
            ZombieSpawnSlowButton.enabled = false;
            StartCoroutine("UpdateZombieTimer");
            AdController.Instance.ShowNoSkipVideoAd();

        });

        PoisonButton.onClick.AddListener(delegate
        {
            AtributesScript.IsVirus = true;
            //AtributesScript.decreaseMoney(300);
            PoisonTimer = 300;
            PoisonPanel.gameObject.SetActive(true);
            PoisonButton.enabled = false;
            StartCoroutine("UpdatePoisonTimer");
            AdController.Instance.ShowNoSkipVideoAd();
        });
        #endregion

        //Select Dig tool Defaultly
        SelectToolButton(0);
    }

    public void ShowMainMenu()
    {
        MainMenuWindow.SetActive(true);
        MainMenuWindow.GetComponent<MainMenuScript>().Pause();
    }

    public void SetTool(string text)
    {
        switch (text)
        {
            case "Dig":
                AtributesScript.SetToolID(AtributesManagerScript.ToolID.dig);
                break;
            case "Corpse":
                AtributesScript.SetToolID(AtributesManagerScript.ToolID.corpse);
                break;
            case "Build":
                AtributesScript.SetToolID(AtributesManagerScript.ToolID.build);
                break;
            case "Clear":
                AtributesScript.SetToolID(AtributesManagerScript.ToolID.clear);
                break;
            default:
                AtributesScript.SetToolID(AtributesManagerScript.ToolID.dig);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = AtributesScript.Money.ToString();
        StockText.text = $"{AtributesScript.CorpsesInStock}/{AtributesScript.StockMax}";
    }

    private IEnumerator UpdateZombieTimer()
    {   
        while(ZombieSpawnSlowTimer > 0)
        {
            ZombieSpawnSlowTimer--;
            ZombieSpawnSlowText.text = TimeSpan.FromSeconds(ZombieSpawnSlowTimer).ToString(@"mm\:ss");
            yield return PowerUpsWaitForSeconds;
        }
        ZombieSpawnSlowText.text = "";
        ZombieSpawnSlowPanel.gameObject.SetActive(false);
        ZombieSpawnSlowButton.enabled = true;
        AtributesScript.IsHalfZombieSpawn = false;
    }

    private IEnumerator UpdatePoisonTimer()
    {
        while (PoisonTimer > 0)
        {
            PoisonTimer--;
            PoisonText.text = TimeSpan.FromSeconds(PoisonTimer).ToString(@"mm\:ss");
            yield return PowerUpsWaitForSeconds;
        }
        PoisonText.text = "";
        PoisonPanel.gameObject.SetActive(false);
        PoisonButton.enabled = true;
        AtributesScript.IsVirus = false;
    }

    public void DisplayToolTip(string text)
    {
        StartCoroutine(DisplayToolTipCoroutine(text));
    }

    public IEnumerator DisplayToolTipCoroutine(string text)
    {
        if (!ToolTip.activeSelf)
        {
            ToolTip.SetActive(true);
            ToolTip.transform.GetChild(0).GetComponent<Text>().text = text;
            yield return WaitForSeconds;
            ToolTip.SetActive(false);
        }

    }

    public void SelectToolButton(int index)
    {
        SetTool(toolNames[index]);

        for (int i = 0; i < toolButtons.Length; i++)
        {
            toolButtons[i].image.color = (i == index) ? selected : unselected;
        }
    }

}
