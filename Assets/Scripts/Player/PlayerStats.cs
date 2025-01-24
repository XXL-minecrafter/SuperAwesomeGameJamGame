using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    // Action f�r das Erh�hen der Coins
    public event Action<int> OnIncreaseCoins;

    // Action f�r das Verringern der Coins
    public event Action<int> OnDecreaseCoins;

    // Action f�r das Zur�cksetzen der Coins
    public event Action OnResetCoins;

    // Action f�r das Setzen des Kaugummi-Zustands
    public event Action<bool> OnChewingChanged;
    
    [field: SerializeField] public int CurrentCoins {  get; private set; } // gesammelte Anzahl an Coins
    [field: SerializeField] public int MaxCoins { get; private set; } // Maximale Anzahl an Coins
    [field: SerializeField] public int CoinsFallBack { get; private set; } = 0; // Auf welchen Werst sollen die Coins zur�ckgesetzt werden 
    [field: SerializeField] public int PlacedBubbleGum { get; private set; } // Wie viele Gums wurden bereits platziert
    [field: SerializeField] public bool IsChewing { get; private set; } // Kaut der Spieler gerade einen Kaugummi

    /// <summary>
    /// Erh�ht die Anzahl der Coins um die �bergebene Menge
    /// </summary>
    /// <param name="amount">Anzahl der hinzuzuf�genden Coins</param>
    public void IncreaseCoins(int amount)
    {
        if (CurrentCoins <= MaxCoins) return; // Raus wenn wir bereits die maximale Anzahl an M�nzen haben

        CurrentCoins += amount;
        OnIncreaseCoins?.Invoke(amount); // Event ausl�sen, wenn Coins erh�ht werden
    } // End public void IncreaseCoins(int amount)

    /// <summary>
    /// Verringert die Anzahl der Coins um die �bergebene Menge
    /// wird -1 �bergeben werden alle Coins entfernt
    /// </summary>
    /// <param name="amount">Anzahl der Coins die entfernt werden sollen</param>
    public void DecreaseCoins(int amount=0)
    {
        if(amount == -1)
        {
            ResetCoins();
            return;
        }
        CurrentCoins += amount;
        OnDecreaseCoins?.Invoke(amount); // Event ausl�sen, wenn Coins erh�ht werden
    } // End public void IncreaseCoins(int amount)

    /// <summary>
    /// Setzt die Anzahl der Coins wieder auf 0 oder eben den eingestellten Wert der Variablen CoinsFallBack
    /// </summary>
    public void ResetCoins()
    {
        CurrentCoins = CoinsFallBack;
        OnResetCoins?.Invoke(); // Event ausl�sen, wenn Coins erh�ht werden
    } // End public void ResetCoins()

    /// <summary>
    /// Gibt an ob der Spieler kaugummi kaut oder nicht
    /// </summary>
    /// <param name="chewing"></param>
    public void setChewing(bool chewing)
    {
        IsChewing = chewing;
        OnChewingChanged?.Invoke(chewing);
    } // End public void setChewing(bool chewing)

} // End public class PlayerStats : MonoBehaviour
