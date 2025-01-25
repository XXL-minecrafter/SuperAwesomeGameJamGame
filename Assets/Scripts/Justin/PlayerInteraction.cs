using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionRadius : MonoBehaviour
{
    
    private PlayerInput playerInput;
    private InputAction inputActionInteraction;
    private InputAction bubbleGumBlast;

    private IInteractable interactableObject;



    private float timeStampPressedInteraction;
    private float interactionHoldCounter;
    [SerializeField] private float interactionTime = 5f;

    private bool interactionPressed;
    private bool inInteractionZone;
    private bool isTable;

    public event Action BubbleGumBlast;

    private void Awake()
    {
        playerInput = new PlayerInput();
        inputActionInteraction = playerInput.Player.interaction;
        bubbleGumBlast = playerInput.Player.bubbleGumBlast;
    }

    // Start is called before the first frame update
    void Start()
    {
        interactionPressed = false;
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
        if (bubbleGumBlast.WasPressedThisFrame() && PlayerStats.Instance.IsChewing)
        {
            
        }

        if (isTable)
        {
            if (playerInput.Player.interaction.IsPressed())
            {
                if (interactionHoldCounter >= interactionTime)
                {
                    interactableObject.Interact();
                    timeStampPressedInteraction = 0;
                    interactionHoldCounter = 0;
                }
                interactionHoldCounter += Time.deltaTime;
                timeStampPressedInteraction = Time.time;
            }
            else
            {
                timeStampPressedInteraction = 0;
                interactionHoldCounter = 0;
            }

        }

        if (playerInput.Player.interaction.WasPressedThisFrame())
        {
            interactionPressed = true;
        }
        else
        {
            interactionPressed = false;
        }

        if (interactionPressed && inInteractionZone)
        {
            if (!isTable)
            {
                interactableObject.Interact();
                interactionPressed = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            inInteractionZone = true;
            interactableObject = interactable;
            if (collision.TryGetComponent(out Table table))
            {
                Debug.Log("Is Table");
                isTable = true;
            }
            else
            {
                isTable = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            inInteractionZone = false;
            interactableObject = null;
        }
    }
}
