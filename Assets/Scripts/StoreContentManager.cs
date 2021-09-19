using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreContentManager : MonoBehaviour, ILevelUpAction
{
    public void OnLevelUp(int lvl)
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<ItemScript>()?.LevelToUnlock <= lvl)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false); //used to reset UI on new game
            }
        }
    }
}
