using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MyBox;
using TMPro;

public class SceneController : MonoBehaviour
{
    //  ===========================================

    private event EventHandler sceneChange;
    public event EventHandler onSceneChange
    {
        add
        {
            if (sceneChange == null || !sceneChange.GetInvocationList().Contains(value))
                sceneChange += value;
        }
        remove { sceneChange -= value; }
    }
    public string CurrentScene
    {
        get => currentScene;
        set
        {
            if (currentScene != "")
                previousScene = currentScene;

            currentScene = value;
            sceneChange?.Invoke(this, EventArgs.Empty);
        }
    }
    public string LastScene
    {
        get => previousScene;
        private set => previousScene = value;
    }

    public bool ActionPass
    {
        get => actionPass;
        set => actionPass = value;
    }

    public List<IEnumerator> GetActionLoadingList
    {
        get => actionLoading;
    }
    public void AddActionLoadinList(IEnumerator action)
    {
        actionLoading.Add(action);
    }

    //  ===========================================

    [SerializeField] private List<GameObject> loadingObjs;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private CanvasGroup loadingCG;

    [Header("LEANTWEEN ANIMATION")]
    [SerializeField] private LeanTweenType easeType;
    [SerializeField] private float speed;
    [SerializeField] private float loadingBarSpeed;
    [SerializeField] private TextMeshProUGUI loadingText;

    [Header("DEBUGGER")]
    [ReadOnly][SerializeField] private string currentScene;
    [ReadOnly][SerializeField] private string previousScene;
    [ReadOnly][SerializeField] private bool actionPass;
    [ReadOnly][SerializeField] private float totalSceneProgress;
    [ReadOnly][SerializeField] private bool splashOver;

    //  ============================================

    private List<IEnumerator> actionLoading = new List<IEnumerator>();
    AsyncOperation scenesLoading = new AsyncOperation();

    Coroutine typingEffect;

    //  ============================================

    private void Awake()
    {
        onSceneChange += SceneChange;
    }

    private void OnDisable()
    {
        onSceneChange -= SceneChange;
    }

    private void SceneChange(object sender, EventArgs e)
    {
        StartCoroutine(Loading());
    }

    public IEnumerator Loading()
    {
        GameManager.Instance.CanUseButtons = false;

        Time.timeScale = 0f;

        loadingSlider.value = 0f;

        typingEffect = StartCoroutine(GameManager.Instance.TypingEffect(loadingText, "Loading...", 0.5f, true));

        for (int a = 0; a < loadingObjs.Count; a++)
        {
            loadingObjs[a].SetActive(true);

            yield return null;
        }

        LeanTween.alphaCanvas(loadingCG, 1f, speed).setEase(easeType);

        yield return new WaitWhile(() => loadingCG.alpha != 1f);

        scenesLoading = SceneManager.LoadSceneAsync(CurrentScene, LoadSceneMode.Single);

        scenesLoading.allowSceneActivation = false;

        while (!scenesLoading.isDone)
        {
            if (scenesLoading.progress >= 0.9f)
            {
                scenesLoading.allowSceneActivation = true;

                break;
            }

            yield return null;
        }

        while (!actionPass) yield return null;

        actionPass = false; //  THIS IS FOR RESET

        if (GetActionLoadingList.Count > 0)
        {
            for (int a = 0; a < GetActionLoadingList.Count; a++)
            {
                yield return StartCoroutine(GetActionLoadingList[a]);

                int index = a + 1;

                totalSceneProgress = (float)index / (1 + GetActionLoadingList.Count);

                LeanTween.value(loadingSlider.gameObject, a => loadingSlider.value = a, loadingSlider.value, totalSceneProgress, loadingBarSpeed).setEase(easeType);

                yield return new WaitWhile(() => loadingSlider.value != totalSceneProgress);

                yield return null;
            }

            totalSceneProgress = scenesLoading.progress;

            LeanTween.value(loadingSlider.gameObject, a => loadingSlider.value = a, loadingSlider.value, totalSceneProgress, loadingBarSpeed).setEase(easeType);
        }
        else
        {
            totalSceneProgress = 1f;

            LeanTween.value(loadingSlider.gameObject, a => loadingSlider.value = a, loadingSlider.value, totalSceneProgress, loadingBarSpeed).setEase(easeType);

            yield return new WaitWhile(() => loadingSlider.value != totalSceneProgress);
        }

        yield return new WaitForSecondsRealtime(1f);

        LeanTween.alphaCanvas(loadingCG, 0f, speed).setEase(easeType).setOnComplete(() => 
        {
            if (typingEffect != null) StopCoroutine(typingEffect);

            typingEffect = null;
        });

        yield return new WaitWhile(() => loadingCG.alpha != 0f);

        for (int a = 0; a < loadingObjs.Count; a++)
        {
            loadingObjs[a].SetActive(false);

            yield return null;
        }

        actionLoading.Clear();

        loadingSlider.value = 0f;

        totalSceneProgress = 0f;

        Time.timeScale = 1f;

        GameManager.Instance.CanUseButtons = true;
    }
}
