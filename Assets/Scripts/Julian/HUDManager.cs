using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    private int currentCoins;
    private int currentGumsPlaced;

    [SerializeField] private TextMeshProUGUI currentCoinsText;
    [SerializeField] private TextMeshProUGUI currentGumsPlacedText;


    private void OnEnable()
    {
        PlayerStats.Instance.OnSetCoins += SetCurrentCoins;
        PlayerStats.Instance.OnResetCoins += ResetCurrentCoins;
        PlayerStats.Instance.OnGumPlaced += SetCurrentGumsPlaced;
    }

    private void OnDisable()
    {
        PlayerStats.Instance.OnSetCoins -= SetCurrentCoins;
        PlayerStats.Instance.OnResetCoins -= ResetCurrentCoins;
        PlayerStats.Instance.OnGumPlaced -= SetCurrentGumsPlaced;
    }

    private void ResetCurrentCoins()
    {
        currentCoins = 0;
        currentCoinsText.text = $"{currentCoins}";
    }

    private void SetCurrentCoins(int currentCoins)
    {
        this.currentCoins = currentCoins;
        currentCoinsText.text = $"{this.currentCoins}";
    }

    private void SetCurrentGumsPlaced(int currentGumsPlaced)
    {
        this.currentGumsPlaced = currentGumsPlaced;
        currentGumsPlacedText.text = $"{this.currentGumsPlaced}";
    }
}
