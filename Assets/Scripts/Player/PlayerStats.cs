using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }


    // Action für das Setzen der Coins
    public event Action<int> OnSetCoins;

    // Action für das Zurücksetzen der Coins
    public event Action OnResetCoins;

    // Action für das Setzen des Kaugummi-Zustands
    public event Action<bool> OnChewingChanged;

    // Action für das Beenden des Spiels wenn alle Gums platziert worden sind
    public event Action OnAllGumsPlaced;

    // Action für das Platzieren eines Gum
    public event Action<int> OnGumPlaced;

    [field: SerializeField] public int CurrentCoins { get; private set; } // gesammelte Anzahl an Coins
    [field: SerializeField] public int MaxCoins { get; private set; } // Maximale Anzahl an Coins
    [field: SerializeField] public int CoinsFallBack { get; private set; } = 0; // Auf welchen Werst sollen die Coins zurückgesetzt werden 
    [field: SerializeField] public int PlacedBubbleGum { get; private set; } // Wie viele Gums wurden bereits platziert
    [field: SerializeField] public int PlacedBubbleGumNeeded { get; private set; } = 10; // Wie viele Gums platziert werden müssen
    [field: SerializeField] public bool IsChewing { get; private set; } // Kaut der Spieler gerade einen Kaugummi

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject); return;
        }
    }


    /// <summary>
    /// Erhöht die Anzahl der Coins um die übergebene Menge
    /// </summary>
    /// <param name="amount">Anzahl der hinzuzufügenden Coins</param>
    public void IncreaseCoins(int amount)
    {
        if (CurrentCoins <= MaxCoins) return; // Raus wenn wir bereits die maximale Anzahl an Münzen haben

        CurrentCoins += amount;
        OnSetCoins?.Invoke(CurrentCoins); // Event auslösen, wenn Coins erhöht werden
    } // End public void IncreaseCoins(int amount)

    /// <summary>
    /// Verringert die Anzahl der Coins um die übergebene Menge
    /// wird -1 übergeben werden alle Coins entfernt
    /// </summary>
    /// <param name="amount">Anzahl der Coins die entfernt werden sollen</param>
    public void DecreaseCoins(int amount = 0)
    {
        if (amount == -1)
        {
            ResetCoins();
            return;
        }
        CurrentCoins -= amount;
        OnSetCoins?.Invoke(CurrentCoins); // Event auslösen, wenn Coins erhöht werden
    } // End public void IncreaseCoins(int amount)

    /// <summary>
    /// Setzt die Anzahl der Coins wieder auf 0 oder eben den eingestellten Wert der Variablen CoinsFallBack
    /// </summary>
    public void ResetCoins()
    {
        CurrentCoins = CoinsFallBack;
        OnResetCoins?.Invoke(); // Event auslösen, wenn Coins erhöht werden
    } // End public void ResetCoins()

    /// <summary>
    /// Gibt an ob der Spieler kaugummi kaut oder nicht
    /// </summary>
    /// <param name="chewing"></param>
    public void SetChewing(bool chewing)
    {
        IsChewing = chewing;
        OnChewingChanged?.Invoke(chewing);
    } // End public void setChewing(bool chewing)

    /// <summary>
    /// Erhöht den PlacedGum Count um 1 und checkt ob alle Gums platziert worden sind
    /// </summary>
    public void PlacedGum()
    {
        PlacedBubbleGum++;
        OnGumPlaced?.Invoke(PlacedBubbleGum);
        SetChewing(false);
        if (PlacedBubbleGum == PlacedBubbleGumNeeded)
        {
            OnAllGumsPlaced?.Invoke();
        }
    }
} // End public class PlayerStats : MonoBehaviour
