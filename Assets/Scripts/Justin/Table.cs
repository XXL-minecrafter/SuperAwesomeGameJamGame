using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{

    [field: SerializeField] public GameObject InteractionBox { get ; set ; }

    private PlayerStats playerStats;

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
