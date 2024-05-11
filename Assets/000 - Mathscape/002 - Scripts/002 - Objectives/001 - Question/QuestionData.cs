using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "Mathscape/Quesiton/QuestionData")]
public class QuestionData : ScriptableObject
{
    [field: TextArea][field: SerializeField] public string Question { get; private set; }
    [field: TextArea][field: SerializeField] public string Answer { get; private set; }
    [field: SerializeField] public bool PictureHint { get; private set; }
    [field: SerializeField] public Sprite HintImg { get; private set; }
    [field: ConditionalField("PictureHint", false)][field: TextArea][field: SerializeField] public string Hint { get; private set; }
}
