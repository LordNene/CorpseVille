using System;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Button GraveButton;
    public Button FenceButton;
    public Button RoadButton;
    public Button DecorationButton;
    public Button ClearButton;

    public GameObject GraveTab;
    public GameObject FenceTab;
    public GameObject RoadTab;
    public GameObject DecorationTab;
    public GameObject ClearTab;

    //Tab selection
    private Button[] buttons;
    private GameObject[] tabs;
    private int tabsCount;
    private Color selected;
    private Color unselected;

    //Item selection
    private GameObject selectedItem;
    public Sprite ItemSelected;
    public Sprite ItemUnselected;


    void Start()
    {
        buttons = new Button[] { GraveButton, FenceButton, RoadButton, DecorationButton, ClearButton };
        tabs = new GameObject[] { GraveTab, FenceTab, RoadTab, DecorationTab, ClearTab };
        tabsCount = tabs.Length;

        selected = new Color(0, 0, 0, 1);
        unselected = new Color(0, 0, 0, (float).4);

        GraveButton.onClick.AddListener(delegate { ButtonClicked(0); });
        FenceButton.onClick.AddListener(delegate { ButtonClicked(1); });
        RoadButton.onClick.AddListener(delegate { ButtonClicked(2); });
        DecorationButton.onClick.AddListener(delegate { ButtonClicked(3); });
        ClearButton.onClick.AddListener(delegate { ButtonClicked(4); });

        //Initially, Graves tab is selected
        ButtonClicked(0);
    }

    /// <summary>
    /// Highlihgts the clicked button and selects the corresponding tab
    /// </summary>
    /// <param name="index">Index of the clicked button</param>
    private void ButtonClicked(int index)
    {
        for (int i = 0; i < tabsCount; i++)
        {
            buttons[i].image.color = (i == index) ? selected : unselected;
            tabs[i].SetActive(i == index);
        }
    }

    public void SelectItem(GameObject item)
    {
        if (selectedItem != null)
        {
            selectedItem.transform.Find("Background").GetComponent<Image>().sprite = ItemUnselected;
        }
        selectedItem = item;
        item.transform.Find("Background").GetComponent<Image>().sprite = ItemSelected;
    }
}
