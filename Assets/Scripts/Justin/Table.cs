using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
<<<<<<< Updated upstream
    [field: SerializeField] public GameObject InteractionBox { get ; set ; }
=======

    private PlayerStats playerStats;

    [field: SerializeField] public GameObject InteractionBox { get; set; }
>>>>>>> Stashed changes

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
