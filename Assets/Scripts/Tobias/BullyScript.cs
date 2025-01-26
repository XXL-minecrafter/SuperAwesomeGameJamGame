using UnityEngine;

public class BullyScript : MonoBehaviour, IInteractable
{
    private int neededCoins = 2;

    [field:SerializeField] public GameObject InteractionBox { get; set; }

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite holdSprite;
    private Sprite originalSprite;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    public void Interact()
    {
        if (PlayerStats.Instance.CurrentCoins >= neededCoins)
        {
            PlayerStats.Instance.DecreaseCoins(neededCoins);
            Destroy(gameObject);
        }
    }

    public void ShowInteractionBox()
    {
        InteractionBox.SetActive(true);

        spriteRenderer.sprite = holdSprite;
    }

    public void DisableInteractionBox()
    {
        InteractionBox.SetActive( false);

        spriteRenderer.sprite = originalSprite;
    }
}
