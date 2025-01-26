using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public static PlayerStates CurrentAnimationState { get; private set; }
    public enum PlayerStates
    {
        Idle,
        Walk,
        Run,
        Interact,
        Chewidle,
        ChewWalk,
        ChewRun,
        ChewInteract
    }
    private void OnEnable()
    {
        PlayerMovement.OnPlayerSprint += ChangeAnimation;
        PlayerMovement.OnPlayerWalk += ChangeAnimation;
        PlayerMovement.OnPlayerStanding += ChangeAnimation;
        PlayerInteraction.OnPlayerInteraction += ChangeAnimation;
    }


    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();  
    }

    public void ChangeAnimation(PlayerStates state)
    {
        CurrentAnimationState = state;
        switch (state)
        {
            case PlayerStates.Idle:
                animator.Play("Stand");
                break;
            case PlayerStates.Walk:
                animator.Play("Walk");
                break;
            case PlayerStates.Run:
                animator.Play("Walk");
                break;
            case PlayerStates.Interact:
                animator.Play("Stand");
                break;
            case PlayerStates.ChewWalk:
                animator.Play("GumWalk");
                break;
            case PlayerStates.ChewRun:
                animator.Play("GumWalk");
                break;
            case PlayerStates.ChewInteract:
                animator.Play("GumStand");
                break;
            case PlayerStates.Chewidle:
                animator.Play("GumStand");
                break;
            default:
                animator.Play("Stand");
                break;
        }
    }
}
