<<<<<<< Updated upstream
using System;
=======
>>>>>>> Stashed changes
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionRadius : MonoBehaviour
{
<<<<<<< Updated upstream
    
=======
>>>>>>> Stashed changes
    private PlayerInput playerInput;
    private InputAction inputActionInteraction;
    private InputAction bubbleGumBlast;

    private IInteractable interactableObject;



    private float timeStampPressedInteraction;
    private float interactionHoldTimer;
    private bool holdingInput = false;
    [SerializeField] private float interactionTime = 5f;

    private bool interactionPressed;
    private bool interactionFound;
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
<<<<<<< Updated upstream
        if (bubbleGumBlast.WasPressedThisFrame() && PlayerStats.Instance.IsChewing)
        {
            
        }

        if (isTable)
=======
        //TODO: Gum can also be placed

        if (playerInput.Player.interaction.WasPressedThisFrame()) holdingInput = true;
 
        if (playerInput.Player.interaction.WasReleasedThisFrame()) 
>>>>>>> Stashed changes
        {
            holdingInput = false;
            interactionHoldTimer = 0f;
        }

        if(holdingInput)
        {
            interactionHoldTimer += Time.deltaTime;

            if(interactionHoldTimer >= interactionTime && isTable)
            {
<<<<<<< Updated upstream
                if (interactionHoldCounter >= interactionTime)
                {
                    interactableObject.Interact();
                    timeStampPressedInteraction = 0;
                    interactionHoldCounter = 0;
                }
                interactionHoldCounter += Time.deltaTime;
                timeStampPressedInteraction = Time.time;
=======
                interactableObject.Interact(); holdingInput = false;
>>>>>>> Stashed changes
            }
            else if(!isTable)
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
<<<<<<< Updated upstream
            if (collision.TryGetComponent(out Table table))
            {
                Debug.Log("Is Table");
                isTable = true;
            }
            else
            {
                isTable = false;
            }
=======
            
            isTable = interactable is Table;         
>>>>>>> Stashed changes
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            interactionFound = false;
            isTable = false;
        }
    }
}
