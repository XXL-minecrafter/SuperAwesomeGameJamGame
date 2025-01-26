using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsDisplayScript : MonoBehaviour
{
    private TextMeshProUGUI  coinsAmmount;

    private void Awake()
    {
         coinsAmmount = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        PlayerStats.Instance.OnSetCoins += UpdateText;
    }
    private void OnDisable()
    {
        PlayerStats.Instance.OnSetCoins -= UpdateText;

    }
    public void UpdateText(int value)
    {
         coinsAmmount.text = PlayerStats.Instance.CurrentCoins.ToString();
    }
}
