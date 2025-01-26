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

    [field: SerializeField] public GameObject InteractionBox { get ; set ; }
    private Shaking shake;
    public static Action OnInteract;

    private void OnEnable()
    {
        playerInput = new PlayerInput();
        interact = playerInput.Player.interaction;
        shake =GetComponent<Shaking>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }


    public void Interact()
    {
        //Play Animation "Lever pulled"
        if (!PlayerStats.Instance.IsChewing)
        {
            OnInteract?.Invoke();
            gumAmount -= 1;
            playerStats.SetChewing(true);

            StartCoroutine(CheckGumAmount());
        }
    }

    public IEnumerator CheckGumAmount()
    {
        yield return new WaitForSeconds(shake.setShakeDuration);
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
