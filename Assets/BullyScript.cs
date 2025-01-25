using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyScript : MonoBehaviour,IInteractable
{
    private int neededCoins = 2;

    public GameObject InteractionBox { get; set; }


    public void Interact()
    {
        if (PlayerStats.Instance.CurrentCoins >= neededCoins)
        {
            PlayerStats.Instance.DecreaseCoins(2);
            Destroy(gameObject);
        }
    }

    public void ShowInteractionBox()
    {
        InteractionBox.SetActive(true);
    }

    public void DisableInteractionBox()
    {
        InteractionBox.SetActive( false);
    }
}
