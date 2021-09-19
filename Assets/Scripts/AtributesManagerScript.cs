using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class AtributesManagerScript : MonoBehaviour
{
    public long Money;
    public int Burried;
    public int CorpsesInQueue;
    public int CorpsesInStock;

    public int StockMax;

    public bool IsHalfZombieSpawn;
    public bool IsKiller;
    public bool IsVirus;

    public int Lvl;
    public long XP;
    public Image XPBarImage;
    public Text LvlText;

    public GameObject[] LevelUpListeners;

    public long ItemSelectedCost;
    public Tile ItemSelected;
    public bool ItemSelectedIsGrave;

    private AchievementManager achievementManager;

    //Id of the SelectedTool 
    // 0 - Dig
    // 1 - Corpse
    // 2 - Build
    // 3 - Clear
    public ToolID SelectedTool;

    public enum ToolID{
        dig,
        corpse,
        build,
        clear
    }

    public void SetToolID(ToolID tool)
    {
        SelectedTool = tool;
    }

	public Tile GetItemSelected()
	{
		return ItemSelected;
	}

	public bool GetItemSelectedIsGrave()
	{
		return ItemSelectedIsGrave;
	}

    public void SetAtributesToDefault()
    {
        Money = 300;
        Burried = 0;
        CorpsesInQueue = 0;
        CorpsesInStock = 0;

        StockMax = 10;

        IsHalfZombieSpawn = false;
        IsKiller = false;
        IsVirus = false;

        Lvl = 1;
        XP = 0;

        XPBarImage.fillAmount = 0;
        LvlText.text = $"{Lvl}\nLVL";
        ReloadLvlDependentComponents();
    }
    
    public void increaseMoney(long m) { Money += m; }
	public bool decreaseMoney(long m)
	{
		if (Money - m < 0)
		{
			return false;
		}
		Money -= m;
		return true;
	}
	public long getMoney() { return Money; }

	public int getBurried() { return Burried; }
	public void increaseBurried() { Burried++; achievementManager.OnCorpseBurried(); }
	public void decreaseBurried() { Burried--; }

	public int getCorpsesInQueue() { return CorpsesInQueue; }
	public void increaseCorpsesInQueue() { CorpsesInQueue++; }
	public void decreaseCorpsesInQueue() { CorpsesInQueue--; }

	public int getCorpsesInStock() { return CorpsesInStock; }
	public void increaseCorpsesInStock() { CorpsesInStock++; }
	public void decreaseCorpsesInStock() { CorpsesInStock--; }

	public long getItemSelectedCost() { return ItemSelectedCost; }


	void Start()
    {
        achievementManager = FindObjectOfType<AchievementManager>();
		StockMax = 10;
        LevelUP();
    }


    public void EarnXP(int xp = 10)
    {
        XP += xp;
        var xpNeededForNextLvl = GetXPNeededForNextLvl(Lvl);
        var xpNeededForThisLvl = GetXPNeededForNextLvl(Lvl - 1);

        if (XP >= xpNeededForNextLvl)
        {
            LevelUP();
            xpNeededForNextLvl = GetXPNeededForNextLvl(Lvl);
            xpNeededForThisLvl = GetXPNeededForNextLvl(Lvl - 1);
        }
        XPBarImage.fillAmount = ((XP - xpNeededForThisLvl) * 1.0f) / (xpNeededForNextLvl - xpNeededForThisLvl);
    }

    private void LevelUP()
    {
        Lvl++;
        LvlText.text = $"{Lvl}\nLVL";
        ReloadLvlDependentComponents();
        achievementManager.OnLevelUp();
    }

    private int GetXPNeededForNextLvl(int lvl)
    {
        return lvl*1000;
    }

    private void ReloadLvlDependentComponents()
    {
        foreach (var listener in LevelUpListeners)
        {
            listener.GetComponent<ILevelUpAction>().OnLevelUp(Lvl);
        }
    }

    public void LoadData(long money, int burried, int corpsesInQueue, int corpsesInStock, int stockMax, bool isHalfZombieSpawn,
        bool isKiller, bool isVirus, int lvl, long xp)
    {   
        Money = money;
        Burried = burried;
        CorpsesInQueue = corpsesInQueue;
        CorpsesInStock = corpsesInStock;
        StockMax = stockMax;
        IsHalfZombieSpawn = isHalfZombieSpawn;
        IsKiller = isKiller;
        IsVirus = isVirus;
        Lvl = lvl;
        XP = xp;

        //sorry for this copy&paste
        var xpNeededForNextLvl = GetXPNeededForNextLvl(Lvl);
        var xpNeededForThisLvl = GetXPNeededForNextLvl(Lvl - 1);
        XPBarImage.fillAmount = ((XP - xpNeededForThisLvl) * 1.0f) / (xpNeededForNextLvl - xpNeededForThisLvl);
        LvlText.text = $"{Lvl}\nLVL";
        ReloadLvlDependentComponents();
    }
}
