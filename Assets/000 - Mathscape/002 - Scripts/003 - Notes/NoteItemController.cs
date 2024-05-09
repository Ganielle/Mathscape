using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteItemController : MonoBehaviour
{
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
        noteObj.SetActive(true);
    }

    private void CloseNote()
    {
        noteObj.SetActive(false);
        titleTMP.text = "";
        descriptionTMP.text = "";
        closeBtn.onClick.RemoveAllListeners();
    }
}
