using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class VendingMachineBehaviour : MonoBehaviour, IInteractable
{
    private int gumAmount = 2;

    private PlayerInput playerInput;
    private InputAction interact;

    private PlayerStats playerStats;
    public SpawnPoint SpawnPoint;

    [field: SerializeField] public GameObject InteractionBox { get ; set ; }

    public static Action OnInteract;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        interact = playerInput.Player.interaction;

        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }


    public void Interact()
    {
        //Play Animation "Lever pulled"

        OnInteract?.Invoke();
        VendingMachinePulled();

        CheckGumAmount();
    }

    private void VendingMachinePulled()
    {
        gumAmount -= 1;

        playerStats.SetChewing(true);

    }

    private void CheckGumAmount()
    {
        if (gumAmount == 0)
        {
            SpawnPoint.RemoveSpawnPointObject();
            Destroy(gameObject);
            Destroy(this);
        }
    }

    public void ShowInteractionBox()
    {
        InteractionBox.SetActive(true);
    }

    public void DisableInteractionBox()
    {
        InteractionBox.SetActive(false);
    }
}
