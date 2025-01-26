using UnityEngine;

public class GumPlace : MonoBehaviour, IInteractable
{
    public bool GumPlaced = false;

    [field: SerializeField] public GameObject InteractionBox { get; set; }

    public void ShowInteractionBox()
    {
        InteractionBox?.SetActive(true);
    }
    public void DisableInteractionBox()
    {
        InteractionBox?.SetActive(false);
    }

    public void Interact()
    {
        PlayerStats.Instance.PlacedGum();
    }
}

