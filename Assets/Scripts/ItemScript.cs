using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    //For communication with Attributes Manager and UIScript
    public int Price;
    public bool IsGrave;
    public bool IsClear;
    public Tile Tile;
    public int LevelToUnlock;

    //Item children
    public Image Image;
    public Text NameText;
    public Text PriceText;
    public Button Button;

    //Script properties used to set up Item children
    public Sprite Sprite;
    public string Name;

    //helpers
    private TabController tabController;
    private UIScript UIScript;
    private AtributesManagerScript AttributesManagerScript;

    private void Awake()
    {
        AttributesManagerScript = FindObjectOfType<AtributesManagerScript>();
        UIScript = FindObjectOfType<UIScript>();
        tabController = FindObjectOfType<TabController>();
    }

    void Start()
    {
        Button.onClick.AddListener(ButtonClick);

        Image.sprite = Sprite;
        NameText.text = Name;
        PriceText.text = Price.ToString();
    }

    private void ButtonClick()
    {
        UIScript.SelectToolButton(2);
        tabController.SelectItem(gameObject);
        AttributesManagerScript.ItemSelected = Tile;
        AttributesManagerScript.ItemSelectedCost = Price;
        AttributesManagerScript.ItemSelectedIsGrave = IsGrave;

        if (IsClear)
        {
            UIScript.SetTool("Clear"); //I am sory for this. The idea owner made me do it.
        }
    }
}
