using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleGumBlast : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction bubbleGumBlast;

    private static Action onBubbleGumBlastActivated;
    public static event Action OnBubbleGumBlastActivated { add => onBubbleGumBlastActivated += value;
        remove => onBubbleGumBlastActivated -= value;
    }

    private void OnEnable() => playerInput.Enable();
    private void OnDisable() => playerInput.Disable();

    private void Update()
    {
        if(playerInput.Player.bubbleGumBlast.WasPressedThisFrame())
        {
            onBubbleGumBlastActivated?.Invoke();
        }
    }
}
