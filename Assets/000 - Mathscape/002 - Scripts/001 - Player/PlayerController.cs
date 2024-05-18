using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameplayController controller;
    [SerializeField] private CharacterController characterController;

    [Header("PLAYER MOVEMENTS")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpTimeout;
    [SerializeField] private float fallTimeout;
    [SerializeField] private float speedChangeRate = 10.0f;

    [Header("ENVIRONMENT")]
    [SerializeField] private float groundedOffset;
    [SerializeField] private float groundedRadius;
    [SerializeField] private LayerMask groundLayers;

    [Header("DEBUGGER PLAYER")]
    [ReadOnly][SerializeField] private Vector3 move;
    [ReadOnly][SerializeField] private float _verticalVelocity;
    [ReadOnly][SerializeField] private float _terminalVelocity = 53.0f;

    [Header("DEBUGGER ENVIRONMENT")]
    [ReadOnly][SerializeField] private bool grounded;
    [ReadOnly][SerializeField] private Vector3 spherePosition;
    [ReadOnly][SerializeField] private float _jumpTimeoutDelta;
    [ReadOnly][SerializeField] private float _fallTimeoutDelta;

    private void Update()
    {
        GroundCheck();
        JumpAndGravity();
        Move();
    }

    private void LateUpdate()
    {
        RotateCharacterBasedOnCameraRotation();
    }

    private void GroundCheck()
    {
        grounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z), groundedRadius, groundLayers);
    }

    private void JumpAndGravity()
    {
        if (grounded)
        {
            _fallTimeoutDelta = fallTimeout;

            if (_verticalVelocity < 0.0f)
                _verticalVelocity = -2f;

            if (_jumpTimeoutDelta >= 0.0f)
                _jumpTimeoutDelta -= Time.deltaTime;
        }
        else
        {
            _jumpTimeoutDelta = jumpTimeout;

            if (_fallTimeoutDelta >= 0.0f)
                _fallTimeoutDelta -= Time.deltaTime;
        }

        if (_verticalVelocity < _terminalVelocity)
            _verticalVelocity += gravity * Time.deltaTime;
    }

    private void RotateCharacterBasedOnCameraRotation()
    {
        float horizontalRotation = Camera.main.transform.eulerAngles.y;

        // Create a new rotation based on the camera's horizontal rotation
        Quaternion targetRotation = Quaternion.Euler(0f, horizontalRotation, 0f);

        // Smoothly rotate the character towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 50f * Time.deltaTime);
    }

    private void Move()
    {
        Vector2 inputDirection = new Vector2(controller.MovementDirection.x, controller.MovementDirection.y);

        // Get the camera's forward and right vectors (ignoring the Y-axis)
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;

        // Calculate the movement direction in the XZ plane based on camera orientation
        Vector3 moveDirection = cameraForward * inputDirection.y + cameraRight * inputDirection.x;

        // Make sure the movement vector is normalized
        moveDirection.Normalize();

        // Apply the movement
        characterController.Move(new Vector3(moveDirection.x, _verticalVelocity, moveDirection.z) * moveSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z), groundedRadius);
    }
}
