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
    [SerializeField] private GameObject gameplayUIObj;

    [Space]
    [SerializeField] private bool isAnimatorComplete;
    [SerializeField] private bool isTimelineComplete;
    [SerializeField] private bool isDialogueComplete;

    [Header("REFERENCES")]
    [ConditionalField("isAnimatorComplete")][SerializeField] private Animator animatorComplete;
    [ConditionalField("isTimelineComplete")][SerializeField] private PlayableDirector timelineComplete;
    //[ConditionalField("isDialogueComplete")][SerializeField]
    [SerializeField] private GameObject objectiveObj;
    [SerializeField] private Button passcodeCloseBtn;
    [SerializeField] private Button passcodeEnterBtn;

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
        passcodeEnterBtn.onClick.AddListener(() => EnterCode());
        gameplayUIObj.SetActive(false);
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
            gameplayUIObj.SetActive(true);
            gameplaySceneController.ActivateMouseLook();

            if (isAnimatorComplete)
                animatorComplete.SetTrigger("open");

            else if (isTimelineComplete)
                timelineComplete.Play();

            objectiveObj.SetActive(true);

            checkerItem.IsDone = true;
        }
    }

    private void TurnoffPasscode()
    {
        if (enterCodeCoroutine != null)
            StopCoroutine(enterCodeCoroutine);

        passcodeObj.SetActive(false);
        gameplayUIObj.SetActive(true);
        passcodeTMP.text = "";
        gameplaySceneController.ActivateMouseLook();
        passcodeCloseBtn.onClick.RemoveAllListeners();
        passcodeEnterBtn.onClick.RemoveAllListeners();
    }

    public void EnterCode()
    {
        enterCodeCoroutine = StartCoroutine(CheckPasscode());
    }
}
