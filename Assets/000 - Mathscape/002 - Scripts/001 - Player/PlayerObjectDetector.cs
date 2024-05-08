using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectDetector : MonoBehaviour
{
    [Header("AIM")]
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask aimLayerMask;

    [Header("UI")]
    [SerializeField] private GameObject normalCrosshair;
    [SerializeField] private GameObject dialogueCrosshair;
    [SerializeField] private GameObject pickupCrosshair;

    private void Update()
    {
        CheckObjectCrosshair();
    }

    private void CheckObjectCrosshair()
    {
        if (DetectObject() == null)
        {
            SetCrosshairActive(normalCrosshair, true);
            SetCrosshairActive(dialogueCrosshair, false);
            SetCrosshairActive(pickupCrosshair, false);
        }
        else
        {
            SetCrosshairActive(normalCrosshair, false);
            SetCrosshairActive(dialogueCrosshair, DetectObject().layer == 9);
            SetCrosshairActive(pickupCrosshair, DetectObject().layer == 8);
        }
    }

    private void SetCrosshairActive(GameObject crosshair, bool active)
    {
        crosshair.SetActive(active);
    }

    public GameObject DetectObject()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, aimLayerMask))
            return raycastHit.collider.gameObject;

        return null;
    }
}
