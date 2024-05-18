using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private SettingsController settingsController;
    [SerializeField] private AudioClip bgMusic;

    [Header("DEBUGGER")]
    [ReadOnly] [SerializeField] private string stageName;

    private void Awake()
    {
        GameManager.Instance.sceneController.AddActionLoadinList(GameManager.Instance.SoundMnger.CheckVolumeSaveData());
        GameManager.Instance.sceneController.AddActionLoadinList(settingsController.CheckVolumeOnStart());
        GameManager.Instance.sceneController.ActionPass = true;
        GameManager.Instance.SoundMnger.SetBGMusic(bgMusic);
    }

    public void SetStageName(string value) => stageName = value;

    public void SetDifficultyProceedToGame(int difficulty)
    {
        playerData.CurrentDifficulty = (PlayerData.Difficulty)difficulty;
        GameManager.Instance.sceneController.CurrentScene = stageName;
    }

    public void AppQuit() => Application.Quit();
}
