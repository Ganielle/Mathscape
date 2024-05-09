using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private GameplaySceneController gameplaySceneController;
    [SerializeField] private bool cursorLocked = true;

    [field: Header("DEBUGGER GLOBAL")]
    [field: ReadOnly] [field: SerializeField] public Vector2 MovementDirection { get; private set; }
    [field: ReadOnly] [field: SerializeField] public Vector2 LookDirection { get; private set; }
    [field: ReadOnly] [field: SerializeField] public bool Interact { get; private set; }

    //  =========================

    private PlayerControls playerControls;

    //  =========================

    private void Awake()
    {
        playerControls = new PlayerControls();

#if !UNITY_IOS || !UNITY_ANDROID
        SetCursorState(cursorLocked);
#endif
    }

    private void OnEnable()
    {
        playerControls.Enable();

        playerControls.PlayerGround.Movement.performed += _ => MovementStart();
        playerControls.PlayerGround.Movement.canceled += _ => MovementStop();
        playerControls.PlayerGround.Interact.started += _ => InteractStart();
        playerControls.PlayerGround.Interact.canceled += _ => InteractStop();

#if UNITY_EDITOR
        playerControls.PlayerGround.Look.performed += _ => LookStart();
        playerControls.PlayerGround.Look.canceled += _ => LookStop();
#endif
    }

    private void OnDisable()
    {
        playerControls.PlayerGround.Movement.performed -= _ => MovementStart();
        playerControls.PlayerGround.Movement.canceled -= _ => MovementStop();
        playerControls.PlayerGround.Interact.started -= _ => InteractStart();
        playerControls.PlayerGround.Interact.canceled -= _ => InteractStop();

#if UNITY_EDITOR
        playerControls.PlayerGround.Look.performed -= _ => LookStart();
        playerControls.PlayerGround.Look.canceled -= _ => LookStop();
#endif

        playerControls.Disable();
    }

#if !UNITY_IOS || !UNITY_ANDROID

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(gameplaySceneController.CanMouseLook);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

#endif

    private void LookStart() => LookDirection = playerControls.PlayerGround.Look.ReadValue<Vector2>();

    private void LookStop() => LookDirection = Vector2.zero;

    private void MovementStart() => MovementDirection = playerControls.PlayerGround.Movement.ReadValue<Vector2>();

    private void MovementStop() => MovementDirection = Vector2.zero;

    private void InteractStart() => Interact = true;

    private void InteractStop() => Interact = false;

    public void StopInteract() => Interact = false;
}
