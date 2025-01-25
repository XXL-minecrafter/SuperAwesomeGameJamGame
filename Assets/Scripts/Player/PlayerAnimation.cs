using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public static PlayerStates CurrentAnimationState { get; private set; }
    public enum PlayerStates
    {
        Walk,
        Run,
        Interact,
        ChewWalk,
        ChewRun,
        ChewInteract
    }
    private void OnEnable()
    {
        PlayerMovement.OnPlayerSprint += ChangeAnimation;
        PlayerMovement.OnPlayerWalk += ChangeAnimation;
    }


    private Animator animator;

    public void ChangeAnimation(PlayerStates state)
    {
        CurrentAnimationState = state;
        switch (state)
        {
            case PlayerStates.Walk:
                animator.Play("PlayerWalk");
                break;
            case PlayerStates.Run:
                animator.Play("PlayerRun");
                break;
            case PlayerStates.Interact:
                animator.Play("PlayerInteract");
                break;
            case PlayerStates.ChewWalk:
                animator.Play("PlayerChewWalk");
                break;
            case PlayerStates.ChewRun:
                animator.Play("PlayerChewRun");
                break;
            case PlayerStates.ChewInteract:
                animator.Play("PlayerChewInteract");
                break;
            default:
                animator.Play("PlayerIdle");
                break;
        }
    }
}
