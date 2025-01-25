using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineTestScript : MonoBehaviour, IInteractable
{
    public GameObject InteractionBox { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void Interact()
    {
        Debug.Log("Interaction startetd");
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
