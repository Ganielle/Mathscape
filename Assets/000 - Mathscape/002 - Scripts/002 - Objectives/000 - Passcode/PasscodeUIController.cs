using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PasscodeUIController : MonoBehaviour
{
    [SerializeField] private TMP_InputField passcodeTMP;

    public void SetCodeOnPanel(string value) => passcodeTMP.text += value;
    public void DeleteCodeOnPanel() => passcodeTMP.text = "";
}
