<<<<<<< Updated upstream
using System;
=======
>>>>>>> Stashed changes
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction inputActionInteraction;
    private IInteractable interactableObject;

    private float timeStampPressedInteraction;
    [SerializeField] private float interactionHoldTimer;
    private bool holdingInput = false;
    [SerializeField] private float interactionTime = 5f;

    [SerializeField] private bool interactionFound;
    private bool isTable;

    public static Action<PlayerAnimations.PlayerStates> OnPlayerInteraction;

    private void Awake()
    {
        playerInput = new PlayerInput();
        inputActionInteraction = playerInput.Player.interaction;
    }

    private void OnEnable()
    {
        inputActionInteraction.Enable();
    }

    private void OnDisable()
    {
        inputActionInteraction.Disable();
    }


    // Update is called once per frame
    void Update()
    {

        if (playerInput.Player.interaction.WasPressedThisFrame()) holdingInput = true;

        if (playerInput.Player.interaction.WasReleasedThisFrame())
        {
            holdingInput = false;
            interactionHoldTimer = 0f;
        }

        if (holdingInput && interactionFound)
        {
            if (PlayerStats.Instance.IsChewing)
            {
                OnPlayerInteraction?.Invoke(PlayerAnimations.PlayerStates.ChewInteract);
            }
            else
            {
                OnPlayerInteraction?.Invoke(PlayerAnimations.PlayerStates.Idle);
            }
            interactionHoldTimer += Time.deltaTime;

            if (interactionHoldTimer >= interactionTime && isTable && PlayerStats.Instance.IsChewing)
            {
                interactableObject.Interact();
                holdingInput = false;
                OnPlayerInteraction?.Invoke(PlayerAnimations.PlayerStates.ChewInteract);
            }
            else if (!isTable)
            {
                interactableObject.Interact(); holdingInput = false;
                OnPlayerInteraction?.Invoke(PlayerAnimations.PlayerStates.Interact);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactionFound = true;
            interactableObject = interactable;

            interactableObject.ShowInteractionBox();

            isTable = interactable is Table;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactionFound = false;

            interactableObject.DisableInteractionBox();

            isTable = false;
        }
    }
}
