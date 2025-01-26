using UnityEngine;

public class GumPlace : MonoBehaviour, IInteractable
{
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
        if (PlayerStats.Instance.IsChewing)
        PlayerStats.Instance.PlacedGum();
    }
}

