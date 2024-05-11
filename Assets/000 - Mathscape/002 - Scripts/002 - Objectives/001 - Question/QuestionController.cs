using MyBox;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class QuestionController : MonoBehaviour
{
    [SerializeField] private QuestionDataController questionDataController;
    [SerializeField] private ObjectiveCheckerItem objectiveChecker;
    [SerializeField] private GameplaySceneController gameplaySceneController;
    [SerializeField] private GameObject gameplayUIObj;

    [Header("OBJECTIVE")]
    [SerializeField] private bool isAnimatorEnd;
    [ConditionalField("isAnimatorEnd")][SerializeField] private Animator animator;
    [SerializeField] private bool isTimelineEnd;
    [ConditionalField("isTimelineEnd")][SerializeField] private PlayableDirector playables;
    [SerializeField] private bool isDialogueEnd;
    [ConditionalField("isDialogueEnd")][SerializeField] private DialogueController dialogue;

    [Header("MAIN")]
    [SerializeField] private GameObject mainObj;
    [SerializeField] private TextMeshProUGUI questionTMP;
    [SerializeField] private TMP_InputField answerTMP;
    [SerializeField] private Button checkAnswerBtn;
    [SerializeField] private Button hintBtn;
    [SerializeField] private Button closeBtn;

    [Header("HINT")]
    [SerializeField] private GameObject hintObj;
    [SerializeField] private Image hintImg;
    [SerializeField] private TextMeshProUGUI hintTMP;

    [Header("DEBUGGER")]
    [ReadOnly][SerializeField] private int questionIndex;
    [ReadOnly][SerializeField] private QuestionData tempQuesiton;

    public void Initialize()
    {
        gameplaySceneController.DisableMouseLook();
        ReInitializeQuestionData();

        closeBtn.onClick.AddListener(() => CloseQuestion());

        gameplayUIObj.SetActive(false);
        mainObj.SetActive(true);
    }

    private void ReInitializeQuestionData()
    {
        questionIndex = Random.Range(0, questionDataController.TempQuestions.Count);
        tempQuesiton = questionDataController.TempQuestions[questionIndex];

        //  MAIN
        questionTMP.text = tempQuesiton.Question;
        answerTMP.text = "";

        //  HINT
        hintImg.sprite = tempQuesiton.PictureHint ? tempQuesiton.HintImg : null;
        hintImg.gameObject.SetActive(tempQuesiton.PictureHint ? true : false);
        hintTMP.text = tempQuesiton.PictureHint ? "" : tempQuesiton.Hint;
        hintTMP.gameObject.SetActive(tempQuesiton.PictureHint ? false : true);
    }

    private void CloseQuestion()
    {
        tempQuesiton = null;
        questionIndex = 0;

        //  MAIN
        questionTMP.text = "";
        answerTMP.text = "";

        //  HINT
        hintImg.gameObject.SetActive(false);
        hintImg.sprite = null;
        hintTMP.text = "";
        hintTMP.gameObject.SetActive(false);

        mainObj.SetActive(false);
        gameplayUIObj.SetActive(true);
        gameplaySceneController.ActivateMouseLook();

        closeBtn.onClick.RemoveAllListeners();
    }
}
