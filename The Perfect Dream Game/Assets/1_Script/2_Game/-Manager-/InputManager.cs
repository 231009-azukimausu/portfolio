using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private List<TriggerResponder> responders = new();
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.ClickAction.performed += OnClickPerformed;
    }

    private void OnDisable()
    {
        inputActions.Player.ClickAction.performed -= OnClickPerformed;
        inputActions.Disable();
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        foreach (var responder in responders)
        {
            if (responder != null && responder.IsInsideTrigger)
            {
                responder.DoSomething();
                break; // 最初の一つだけ反応
            }
        }
    }
}