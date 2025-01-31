using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class VendingMachineBehaviour : MonoBehaviour, IInteractable
{
    private int gumAmount = 2;

    private PlayerStats playerStats;
    public SpawnPoint SpawnPoint;
    private SpriteRenderer spriteRenderer;
    private AudioTransition audioPlayer;

    [field: SerializeField] public GameObject InteractionBox { get ; set ; }
    [SerializeField] Sprite leverPulled;
    Sprite oldSprite;

    private Shaking shake;
    public static Action OnInteract;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        audioPlayer = GameObject.Find("AudioPlayer").GetComponent<AudioTransition>();
        shake = GetComponent<Shaking>();
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
            audioPlayer.ToggleIntensityLevel();
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
