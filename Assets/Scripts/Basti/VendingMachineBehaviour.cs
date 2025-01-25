using UnityEngine;
using UnityEngine.InputSystem;

public class VendingMachineBehaviour : MonoBehaviour, IInteractable
{
    private int gumAmount = 2;

    private PlayerInput playerInput;
    private InputAction interact;

    private PlayerStats playerStats;
    public SpawnPoint SpawnPoint;

    public GameObject InteractionBox { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        interact = playerInput.Player.interaction;

        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }


    public void Interact()
    {
        //Play Animation "Lever pulled"

        gumAmount -= 1;

        playerStats.SetChewing(true);

        CheckGumAmount();
    }

    private void VendingMachinePulled()
    {
        gumAmount -= 1;

        playerStats.SetChewing(true);

    }

    private void CheckGumAmount()
    {
        if(gumAmount == 0)
        {
            SpawnPoint.RemoveSpawnPointObject();
            Destroy(gameObject);
            Destroy(this);
        }
    }

    public void ShowInteractionBox()
    {
        throw new System.NotImplementedException();
    }

    public void DisableInteractionBox()
    {
        throw new System.NotImplementedException();
    }
}
