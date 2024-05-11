using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteItemController : MonoBehaviour
{
    [SerializeField] private GameplaySceneController gameplaySceneController;
    [SerializeField] private GameObject gameplayUIObj;

    [Space]
    [SerializeField] private GameObject noteObj;
    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private TextMeshProUGUI descriptionTMP;
    [SerializeField] private Button closeBtn;

    [Header("CONTENT")]
    [SerializeField] private string titleContent;
    [TextArea][SerializeField] private string descriptionContent;

    public void Initialize()
    {
        titleTMP.text = titleContent;
        descriptionTMP.text = descriptionContent;
        closeBtn.onClick.AddListener(() => CloseNote());
        gameplayUIObj.SetActive(false);
        noteObj.SetActive(true);
        gameplaySceneController.DisableMouseLook();
    }

    private void CloseNote()
    {
        noteObj.SetActive(false);
        gameplayUIObj.SetActive(true);
        titleTMP.text = "";
        descriptionTMP.text = "";
        closeBtn.onClick.RemoveAllListeners();
        gameplaySceneController.ActivateMouseLook();
    }
}
