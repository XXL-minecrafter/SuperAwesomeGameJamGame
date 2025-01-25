using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{

    private PlayerStats playerStats;

    public void Interact()
    {
        PlayerStats.Instance.PlacedGum();
        Debug.Log("Table Interaction Holded");
    }
}
