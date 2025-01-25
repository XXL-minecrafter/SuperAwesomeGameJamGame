using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class VendingMachineBehaviour : MonoBehaviour, IInteractable
{
    private int gumAmount = 2;

    private PlayerInput playerInput;
    private InputAction interact;

    private PlayerStats playerStats;
    public SpawnPoint SpawnPoint;


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

        playerStats.setChewing(true);

        CheckGumAmount();
    }

    private void VendingMachinePulled()
    {
        gumAmount -= 1;

        playerStats.setChewing(true);

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
}
