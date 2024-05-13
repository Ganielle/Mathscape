using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectDetector : MonoBehaviour
{
    [SerializeField] private GameplayController gameplayController;
    [SerializeField] private GameplaySceneController gameplaySceneController;
    [SerializeField] private PlayerInventory playerInventory;

    [Header("AIM")]
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask aimLayerMask;

    [Header("UI")]
    [SerializeField] private GameObject normalCrosshair;
    [SerializeField] private GameObject dialogueCrosshair;
    [SerializeField] private GameObject pickupCrosshair;
    [SerializeField] private GameObject questCrosshair;

    private void Update()
    {
        CheckObjectCrosshair();
        InteractWithObject();
    }

    private void CheckObjectCrosshair()
    {
        if (DetectObject() == null)
        {
            SetCrosshairActive(normalCrosshair, true);
            SetCrosshairActive(dialogueCrosshair, false);
            SetCrosshairActive(pickupCrosshair, false);
            SetCrosshairActive(questCrosshair, false);
        }
        else
        {
            SetCrosshairActive(normalCrosshair, false);
            SetCrosshairActive(dialogueCrosshair, DetectObject().layer == 9);
            SetCrosshairActive(pickupCrosshair, DetectObject().layer == 8);
            SetCrosshairActive(questCrosshair, DetectObject().layer == 10);
        }
    }

    private void SetCrosshairActive(GameObject crosshair, bool active)
    {
        crosshair.SetActive(active);
    }

    private void InteractWithObject()
    {
        if (!gameplaySceneController.CanMouseLook) return;

        if (!gameplayController.Interact) return;

        if (DetectObject() == null) return;

        if (DetectObject().tag == "Passcode")
        {
            DetectObject().GetComponent<PasscodeController>().Initialize();   
        }
        else if (DetectObject().tag == "NoteItem")
        {
            DetectObject().GetComponent<NoteItemController>().Initialize();
        }
        else if (DetectObject().tag == "DialogueItem")
        {
            DetectObject().GetComponent<DialogueController>().Initialize();
        }
        else if (DetectObject().tag == "QuestionItem")
        {
            DetectObject().GetComponent<QuestionController>().Initialize();
        }
        else if (DetectObject().tag == "PickupItem")
        {
            playerInventory.AddKeyItem(DetectObject().GetComponent<KeyController>().ItemIndex);
            DetectObject().GetComponent<KeyController>().Initialize();
        }
        else if (DetectObject().tag == "Objective")
        {
            DetectObject().GetComponent<GoalController>().Initialize(playerInventory.KeyItems, playerInventory.RemoveAllItemsOnInventoryKey);
        }

        gameplayController.StopInteract();
    }

    public GameObject DetectObject()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, aimLayerMask))
        {
            if (raycastHit.collider.gameObject.layer == 10)
            {
                if (raycastHit.collider.GetComponent<ObjectiveCheckerItem>().IsDone) return null;

                return raycastHit.collider.gameObject;
            }
            else
            {
                return raycastHit.collider.gameObject;
            }
        }

        return null;
    }
}
