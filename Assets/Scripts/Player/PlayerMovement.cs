<<<<<<< Updated upstream
<<<<<<< Updated upstream
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
=======
using UnityEngine;
>>>>>>> Stashed changes
=======
using UnityEngine;
>>>>>>> Stashed changes
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction move;
    private InputAction sprint;

    private Rigidbody2D rb2D;
    private PlayerStats playerStats;
    private Vector2 moveDirection;

    [SerializeField] private float walkspeed;
    [SerializeField] private float sprintSpeed;
    private float movespeed;

    public static Action<PlayerAnimations.PlayerStates> OnPlayerSprint;
    public static Action<PlayerAnimations.PlayerStates> OnPlayerWalk;
    public static Action<PlayerAnimations.PlayerStates> OnPlayerStanding;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        rb2D = GetComponent<Rigidbody2D>();
        playerInput = new PlayerInput();
        move = playerInput.Player.move;
        sprint = playerInput.Player.sprint;
    }
    private void OnEnable()
    {
        sprint.Enable();
        move.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
        sprint.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = Vector2.zero;

        //Gets WASD Inputs and adds them to the velocity
        moveDirection.x = move.ReadValue<Vector2>().x;
        moveDirection.y = move.ReadValue<Vector2>().y;
        if (moveDirection == Vector2.zero)
        {
            if (PlayerStats.Instance.IsChewing)
            {
                OnPlayerStanding?.Invoke(PlayerAnimations.PlayerStates.ChewInteract);
            }
            else
            {
                OnPlayerStanding?.Invoke(PlayerAnimations.PlayerStates.Idle);
            }
            return;
        }


        if (sprint.IsPressed())
        {
            SetMoveSpeed(sprintSpeed, PlayerAnimations.PlayerStates.ChewRun, PlayerAnimations.PlayerStates.Run);
        }
        else
        {
            SetMoveSpeed(walkspeed, PlayerAnimations.PlayerStates.ChewWalk, PlayerAnimations.PlayerStates.Walk);
        }
    }

    private void SetMoveSpeed(float speed, PlayerAnimations.PlayerStates chewAnimation, PlayerAnimations.PlayerStates normalAnimation)
    {
        movespeed = speed;
        if (PlayerStats.Instance.IsChewing)
            OnPlayerSprint?.Invoke(chewAnimation);
        else
            OnPlayerSprint?.Invoke(normalAnimation);
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
    }
}

