using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionDataController : MonoBehaviour
{
    public List<QuestionData> TempQuestions { get => tempQuestions; }

    //  ===========================

    [SerializeField] private List<QuestionData> easyQuestions;
    [SerializeField] private List<QuestionData> mediumQuestions;
    [SerializeField] private List<QuestionData> hardQuesitons;

    [Header("DEBUGGER")]
    [ReadOnly][SerializeField] private List<QuestionData> tempQuestions;

    private void Awake()
    {
        StartCoroutine(GenerateQuestions());
    }

    public IEnumerator GenerateQuestions(Action action = null)
    {
        tempQuestions = easyQuestions;

        yield return StartCoroutine(Shuffler.Shuffle(tempQuestions));

        action?.Invoke();
    }
}

public static class Shuffler
{
    private static System.Random rng = new System.Random();

    public static IEnumerator Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;

            yield return null;
        }
    }
}