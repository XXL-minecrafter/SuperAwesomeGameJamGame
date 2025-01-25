using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionRadius : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction inputActionInteraction;
    private IInteractable interactableObject;

    private float timeStampPressedInteraction;
    [SerializeField] private float interactionHoldTimer;
    private bool holdingInput = false;
    [SerializeField] private float interactionTime = 5f;

    private bool interactionPressed = false;
    [SerializeField] private bool interactionFound;
    private bool isTable;

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

        if (holdingInput)
        {
            interactionHoldTimer += Time.deltaTime;

            if (interactionHoldTimer >= interactionTime && isTable && PlayerStats.Instance.IsChewing)
            {
                interactableObject.Interact(); holdingInput = false;
            }
            else if (!isTable)
            {
                interactableObject.Interact(); holdingInput = false;
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
