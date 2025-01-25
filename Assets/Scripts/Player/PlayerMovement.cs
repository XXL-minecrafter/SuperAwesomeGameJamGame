using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction move;

    private Rigidbody2D rb2D;
    private PlayerStats playerStats;
    private Vector2 moveDirection;
    [SerializeField] private float movespeed;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        rb2D = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInput();
        move = playerInput.Player.move;
    }
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Enable();
        move.Enable();
    }
    void Start()
    {

    }
    private void OnDisable()
    {
        move.Disable();
        playerInput.Player.Disable();
        playerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Vector2.zero;

        //Gets WASD Inputs and adds them to the velocity
        moveDirection.x = move.ReadValue<Vector2>().x;
        moveDirection.y = move.ReadValue<Vector2>().y;
    }
    private void FixedUpdate()
    {
        rb2D.velocity = moveDirection * movespeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Collectable")
        {
            collision.transform.GetComponent<ICollectable>().Collect();
        }
        if (collision.transform.tag == "Interactable")
        {
            collision.transform.GetComponent<IInteractable>().Interact();
        }
    }

}

