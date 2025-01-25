using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyScript : MonoBehaviour,IInteractable
{
    private int neededCoins = 2;

    public GameObject InteractionBox { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        if (PlayerStats.Instance.CurrentCoins >= neededCoins)
        {

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
