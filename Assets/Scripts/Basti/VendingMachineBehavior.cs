using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VendingMachineBehavior : MonoBehaviour
{
    private int gumAmount = 2;

    //private PlayerInput playerInput;

    private void OnUse()
    {
        gumAmount -= 1;

        //Open UI for VendingMachine


    }
}
