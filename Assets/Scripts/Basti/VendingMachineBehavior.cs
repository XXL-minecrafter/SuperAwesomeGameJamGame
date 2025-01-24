using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class VendingMachineBehavior : MonoBehaviour, IInteractable
{
    private int gumAmount = 2;

    private PlayerInput playerInput;
    private InputAction interact;

    private bool isUsed;

    private PlayerStats playerStats;

    //private SpawnPoint spawnPoint;


    private void OnEnable()
    {
        playerInput = new PlayerInput();
        interact = playerInput.Player.interaction;

        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (isUsed && interact.WasPressedThisFrame())
        {
            
        }
    }


    public void Interaction()
    {
        isUsed = true;
        //Open UI for VendingMachine 

    }

    private void VendingMachinePulled()
    {
        gumAmount -= 1;

        playerStats.setChewing(true);

        //Close UI for VendingMachine
    }

    private void CheckGumAmount()
    {
        if(gumAmount == 0)
        {
            
        }
    }
}
