using UnityEngine;

public class VendingMachineTestScript : MonoBehaviour, IInteractable
{
    public GameObject InteractionBox { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void DisableInteractionBox()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        Debug.Log("Interaction startetd");
    }

    public void ShowInteractionBox()
    {
        throw new System.NotImplementedException();
    }
}
