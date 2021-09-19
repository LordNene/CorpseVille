using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public int value;
    private AtributesManagerScript atributesManagerScript;

    private void Start()
    {
        atributesManagerScript = FindObjectOfType<AtributesManagerScript>();
        InvokeRepeating("IncreaseMoney", 10, 10);
    }

    private void IncreaseMoney()
    {
        Debug.Log("Money earned: "+value * atributesManagerScript.Burried);
        atributesManagerScript.increaseMoney(value* atributesManagerScript.Burried);
    }
}
