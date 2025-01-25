using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{

    private PlayerStats playerStats;

    [field: SerializeField] public GameObject InteractionBox { get ; set ; }

    public void DisableInteractionBox()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        PlayerStats.Instance.PlacedGum();
        Debug.Log("Table Interaction Holded");
    }

    public void ShowInteractionBox()
    {
        throw new System.NotImplementedException();
    }
}
