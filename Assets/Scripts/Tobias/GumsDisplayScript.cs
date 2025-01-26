using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GumsDisplayeScript : MonoBehaviour
{
    private TextMeshProUGUI gumAmmount;

    private void Awake()
    {
        gumAmmount = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    { 
    
        PlayerStats.Instance.OnGumPlaced += UpdateText;
    }
    private void OnDisable()
    {
        PlayerStats.Instance.OnGumPlaced -= UpdateText;

    }
    public void UpdateText(int value)
    {
        gumAmmount.text = PlayerStats.Instance.PlacedBubbleGum.ToString();
    }
}
