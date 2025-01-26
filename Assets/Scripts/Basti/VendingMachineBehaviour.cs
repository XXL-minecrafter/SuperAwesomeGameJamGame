using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class VendingMachineBehaviour : MonoBehaviour, IInteractable
{
    private int gumAmount = 2;

    private PlayerInput playerInput;
    private InputAction interact;

    private PlayerStats playerStats;
    public SpawnPoint SpawnPoint;
    private SpriteRenderer spriteRenderer;

    [field: SerializeField] public GameObject InteractionBox { get ; set ; }
    [SerializeField] Sprite leverPulled;
    Sprite oldSprite;

    private Shaking shake;
    public static Action OnInteract;

    private void OnEnable()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerInput = new PlayerInput();
        interact = playerInput.Player.interaction;
        shake =GetComponent<Shaking>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        
    }
    private void Start()
    {
        oldSprite = spriteRenderer.sprite;
    }

    public void Interact()
    {
        //Play Animation "Lever pulled"
        if (!PlayerStats.Instance.IsChewing)
        {
            spriteRenderer.sprite = leverPulled;
            OnInteract?.Invoke();
            gumAmount -= 1;
            playerStats.SetChewing(true);
            PlayerStats.Instance.DecreaseCoins(1);
            StartCoroutine(CheckGumAmount());
        }
    }

    public IEnumerator CheckGumAmount()
    {
        yield return new WaitForSeconds(shake.setShakeDuration);
        spriteRenderer.sprite = oldSprite;
        if (gumAmount == 0)
        {
            SpawnPoint.RemoveSpawnPointObject();
            Destroy(gameObject);
        }
    }

    public void ShowInteractionBox()
    {
        InteractionBox.SetActive(true);
    }

    public void DisableInteractionBox()
    {
        InteractionBox.SetActive(false);
    }
}
