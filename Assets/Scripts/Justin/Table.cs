using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{

    private PlayerStats playerStats;

    public GameObject InteractionBox { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Interact()
    {
        PlayerStats.Instance.PlacedGum();
        Debug.Log("Table Interaction Holded");
    }

    public void LoadInteractionBox()
    {
        throw new System.NotImplementedException();
    }

    public void ShowInteractionBox()
    {
        throw new System.NotImplementedException();
    }
}
