using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                    _instance = new GameObject().AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    //  ===========================

    [SerializeField] private List<GameObject> dontDestroyOnLoad;
    [field: SerializeField] public SceneController sceneController { get; set; }
    [field: SerializeField] public SoundManager SoundMnger { get; set; }
    [field: SerializeField] public Camera MainCamera { get; set; }
    [field: SerializeField] public Camera UICamera { get; set; }

    [Space]
    [SerializeField] private string sceneName;

    [field: Header("DEBUGGER")]
    [field: ReadOnly] [field: SerializeField] public bool CanUseButtons { get; set; }

    private void Awake()
    {
        for (int a = 0; a < dontDestroyOnLoad.Count; a++)
            DontDestroyOnLoad(dontDestroyOnLoad[a]);

    }

    private void Start()
    {
        sceneController.CurrentScene = sceneName;
    }

    public IEnumerator TypingEffect(TextMeshProUGUI text, string value, float speed, bool isLoop, Action action = null)
    {
        text.text = "";
        int index = 0;

        if (isLoop)
        {
            while (true)
            {
                foreach (char c in value)
                {
                    if (index >= value.Length)
                    {
                        text.text = "";
                        index = 0;
                    }

                    text.text += c;
                    index++;
                    yield return new WaitForSecondsRealtime(speed);
                }

                yield return null;
            }
        }
        else
        {
            foreach (char c in value)
            {
                text.text += c;
                yield return new WaitForSecondsRealtime(speed);
            }
        }

        action?.Invoke();
    }
}
