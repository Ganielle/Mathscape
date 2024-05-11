using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameplaySceneController gameplaySceneController;
    [SerializeField] private float typeSpeed;

    [Space]
    [SerializeField] private GameObject dialogueObj;
    [SerializeField] private GameObject gameplayUIObj;
    [SerializeField] private TextMeshProUGUI characterNameTMP;
    [SerializeField] private TextMeshProUGUI dialogueTMP;
    [SerializeField] private Button nextBtn;

    [Header("CONTENT")]
    [SerializeField] private List<string> nameSequence;
    [TextArea][SerializeField] private List<string> dialogueSequence;

    [Header("DEBUGGER")]
    [ReadOnly][SerializeField] private bool canPressSkip;
    [ReadOnly][SerializeField] private bool canNextDialogue;
    [ReadOnly][SerializeField] private int currentDialogueIndex;

    //  =========================

    Coroutine showText;

    //  =========================

    public void Initialize()
    {
        nextBtn.onClick.AddListener(() => NextDialogueBtn());
        gameplayUIObj.SetActive(false);
        dialogueObj.SetActive(true);
        gameplaySceneController.DisableMouseLook();
        StartCoroutine(ShowDialogue());
    }

    private IEnumerator ShowDialogue()
    {
        currentDialogueIndex = 0;

        for (int a = 0; a < dialogueSequence.Count; a++)
        {
            currentDialogueIndex = a;

            canNextDialogue = false;

            characterNameTMP.text = nameSequence[a];
            dialogueTMP.text = "";

            showText = StartCoroutine(TypeEffect(dialogueSequence[a]));

            while (!canNextDialogue) yield return null;

            yield return null;
        }
    }

    private IEnumerator TypeEffect(string value)
    {   
        foreach(char c in value)
        {
            dialogueTMP.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        showText = null;
    }

    private void NextDialogueBtn()
    {
        if (currentDialogueIndex >= dialogueSequence.Count - 1)
        {
            dialogueObj.SetActive(false);
            gameplayUIObj.SetActive(true);
            characterNameTMP.text = "";
            dialogueTMP.text = "";
            nextBtn.onClick.RemoveAllListeners();
            gameplaySceneController.ActivateMouseLook();
        }
        else
        {
            if (showText != null)
            {
                StopCoroutine(showText);
                showText = null;
            }

            canNextDialogue = true;
        }
    }
}
