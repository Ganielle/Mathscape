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

    [SerializeField] private QuestionDataController questionDataController;
    [SerializeField] private SettingsController settingsController;
    [SerializeField] private AudioClip bgMusic;

    [Header("DEBUGGER")]
    [ReadOnly] [SerializeField] private bool canMouseLook;

    private void Awake()
    {
        GameManager.Instance.sceneController.AddActionLoadinList(settingsController.CheckVolumeOnStart());
        GameManager.Instance.sceneController.AddActionLoadinList(questionDataController.GenerateQuestions());
        GameManager.Instance.sceneController.ActionPass = true;
        CanMouseLook = true;
        GameManager.Instance.SoundMnger.SetBGMusic(bgMusic);
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

    public void ChangeScene(string sceneName) => GameManager.Instance.sceneController.CurrentScene = sceneName;
}
