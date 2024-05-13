using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GoalController : MonoBehaviour
{
    [SerializeField] private List<GameObject> gemObjectives;
    [SerializeField] private PlayableDirector endPlayables;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private ObjectiveCheckerItem objectiveCheckerItem;

    [Header("DEBUGGER")]
    [ReadOnly] [SerializeField] private List<int> gemObjects;

    public void Initialize(List<int> values, Action endAction = null)
    {
        Debug.Log(values.Count);
        if (values.Count <= 0)
        {
            dialogueController.Initialize();
            return;
        }

        for (int a = 0; a < values.Count; a++)
        {
            if (!gemObjects.Contains(values[a])) gemObjects.Add(values[a]);
        }

        for (int a = 0; a < gemObjects.Count; a++)
        {
            if (!gemObjectives[gemObjects[a] - 1].activeInHierarchy)
                gemObjectives[gemObjects[a] - 1].SetActive(true);
        }

        if (gemObjects.Count >= 3)
        {
            objectiveCheckerItem.IsDone = true;

            if (endPlayables != null)
                endPlayables.Play();
        }

        endAction?.Invoke();
    }
}
