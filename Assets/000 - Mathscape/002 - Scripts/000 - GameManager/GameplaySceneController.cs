using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneController : MonoBehaviour
{
    public bool CanMouseLook
    {
        get => canMouseLook;
        set => canMouseLook = value;
    }

    //  ==========================

    [Header("DEBUGGER")]
    [ReadOnly] [SerializeField] private bool canMouseLook;

    private void Awake()
    {
        CanMouseLook = true;
    }

    public void ActivateMouseLook()
    {
        CanMouseLook = true;

#if UNITY_EDITOR
        Cursor.lockState = CursorLockMode.Locked;
#endif
    }

    public void DisableMouseLook()
    {
        CanMouseLook = false;

#if UNITY_EDITOR
        Cursor.lockState = CursorLockMode.None;
#endif
    }
}
