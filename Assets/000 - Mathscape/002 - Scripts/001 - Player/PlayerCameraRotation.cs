using Cinemachine;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerCameraRotation : CinemachineExtension
{
    [SerializeField] private GameplayController gameplayController;
    [SerializeField] private GameplaySceneController gameplaySceneController;

    [Header("VCAM")]
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    [SerializeField] private float clampAngle = 80f;

    [Header("DEBUGGER")]
    [ReadOnly] [SerializeField] private Vector3 startingRotation;
    [ReadOnly] [SerializeField] private Vector2 deltaInput;
    [ReadOnly] [SerializeField] private Quaternion tempRotation;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (!Application.isPlaying) return;

        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;

                if (!gameplaySceneController.CanMouseLook)
                {
                    state.RawOrientation = tempRotation;
                    return;
                }

#if UNITY_EDITOR
                deltaInput = gameplayController.LookDirection;
#else
        foreach (var touch in Touchscreen.current.touches)
            {
                int touchId = touch.touchId.ReadValue();
                if (touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
                    touchOverUI[touchId] = IsTouchOverUI(touch.position);

                if (touchOverUI.ContainsKey(touchId) && touchOverUI[touchId])
                    continue;

                deltaInput = touch.delta.ReadValue();
            }
#endif
                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                tempRotation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }

    private bool IsTouchOverUI(Vector2Control touchPosition)
    {
        Vector2 touchPos = touchPosition.ReadValue();

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = touchPos;

        return EventSystem.current.IsPointerOverGameObject(eventData.pointerId);
    }
}
