using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveCheckerItem : MonoBehaviour
{
    [field: Header("DEBUGGER")]
    [field: ReadOnly][field: SerializeField] public bool IsDone { get; set; }

    public void SetIsDone(bool value) => IsDone = value;
}
