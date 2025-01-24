using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    // Action für das Erhöhen der Coins
    public event Action<int> OnIncreaseCoins;

    // Action für das Verringern der Coins
    public event Action<int> OnDecreaseCoins;

    // Action für das Setzen des Kaugummi-Zustands
    public event Action<bool> OnChewingChanged;

    public int CurrentCoins {  get; private set; } // gesammelte Anzahl an Coins
    public int MaxCoins { get; private set; } // Maximale Anzahl an Coins

    public int PlacedBubbleGum { get; private set; } // Wie viele Gums wurden bereits platziert

    public bool IsChewing { get; private set; } // Kaut der Spieler gerade einen Kaugummi


    /// <summary>
    /// Erhöht die Anzahl der Coins um die übergebene Menge
    /// </summary>
    /// <param name="amount">Anzahl der hinzuzufügenden Coins</param>
    public void IncreaseCoins(int amount)
    {
        CurrentCoins += amount;
        OnIncreaseCoins?.Invoke(amount); // Event auslösen, wenn Coins erhöht werden
    } // End public void IncreaseCoins(int amount)

    /// <summary>
    /// Verringert die Anzahl der Coins um die übergebene Menge
    /// wird -1 übergeben werden alle Coins entfernt
    /// </summary>
    /// <param name="amount">Anzahl der Coins die entfernt werden sollen</param>
    public void DecreaseCoins(int amount=0)
    {
        CurrentCoins += amount;
        OnDecreaseCoins?.Invoke(amount); // Event auslösen, wenn Coins erhöht werden
    } // End public void IncreaseCoins(int amount)

    /// <summary>
    /// Gibt an ob der Spieler kaugummi kaut oder nicht
    /// </summary>
    /// <param name="chewing"></param>
    public void setChewing(bool chewing)
    {
        IsChewing = chewing;
        OnChewingChanged?.Invoke(chewing);
    }


}
