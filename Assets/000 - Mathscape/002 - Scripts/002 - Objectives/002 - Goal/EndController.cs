using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private GameObject gameOverObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            dialogueController.Initialize(() => gameOverObj.SetActive(true));
        }
    }
}
