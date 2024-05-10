using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PasscodeController : MonoBehaviour
{
    [SerializeField] private ObjectiveCheckerItem checkerItem;
    [SerializeField] private GameplaySceneController gameplaySceneController;

    [Space]
    [SerializeField] private bool isAnimatorComplete;
    [SerializeField] private bool isTimelineComplete;
    [SerializeField] private bool isDialogueComplete;

    [Header("REFERENCES")]
    [ConditionalField("isAnimatorComplete")][SerializeField] private Animator animatorComplete;
    [ConditionalField("isTimelineComplete")][SerializeField] private PlayableDirector timelineComplete;
    //[ConditionalField("isDialogueComplete")][SerializeField]
    [SerializeField] private Button passcodeCloseBtn;

    [Header("PUZZLE OBJECTS")]
    [SerializeField] private string passcodeCode;
    [SerializeField] private GameObject passcodeObj;
    [SerializeField] private TMP_InputField passcodeTMP;
    [SerializeField] private List<Button> buttonList;

    //  ==============================

    Coroutine enterCodeCoroutine;

    //  ==============================

    public void Initialize()
    {
        passcodeTMP.text = "";

        foreach (Button btn in buttonList)
            btn.interactable = true;

        passcodeCloseBtn.onClick.AddListener(() => TurnoffPasscode());
        passcodeObj.SetActive(true);
        gameplaySceneController.DisableMouseLook();
    }

    IEnumerator CheckPasscode()
    {
        if (passcodeTMP.text != passcodeCode)
        {
            passcodeTMP.text = "ERROR";

            foreach (Button btn in buttonList)
            {
                btn.interactable = false;
                yield return null;
            }

            yield return new WaitForSeconds(2f);

            passcodeTMP.text = "";

            foreach (Button btn in buttonList)
            {
                btn.interactable = true;
                yield return null;
            }
        }
        else
        {
            passcodeObj.SetActive(false);
            gameplaySceneController.ActivateMouseLook();

            if (isAnimatorComplete)
                animatorComplete.SetTrigger("unlocked");

            else if (isTimelineComplete)
                timelineComplete.Play();

            checkerItem.IsDone = true;
        }
    }

    public void SetCodeOnPanel(string value) => passcodeTMP.text += value;
    public void DeleteCodeOnPanel() => passcodeTMP.text = "";

    private void TurnoffPasscode()
    {
        if (enterCodeCoroutine != null)
            StopCoroutine(enterCodeCoroutine);

        passcodeObj.SetActive(false);
        passcodeTMP.text = "";
        gameplaySceneController.ActivateMouseLook();
        passcodeCloseBtn.onClick.RemoveAllListeners();
    }

    public void EnterCode()
    {
        enterCodeCoroutine = StartCoroutine(CheckPasscode());
    }
}
