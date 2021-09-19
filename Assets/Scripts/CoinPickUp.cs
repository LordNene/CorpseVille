using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Coin pick up. Not being used in current version.
/// </summary>
public class CoinPickUp : MonoBehaviour
{
    public int value;
    private AtributesManagerScript atributesManagerScript;

    private void Start()
    {
        atributesManagerScript = FindObjectOfType<AtributesManagerScript>();
    }

    private void OnMouseDown()
    {
        atributesManagerScript.Money += value;
        Destroy(gameObject);
    }
}