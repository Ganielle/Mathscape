using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public int ItemIndex
    {
        get => itemIndex;
        private set => itemIndex = value;
    }

    //  =======================

    [SerializeField] private int startingItemIndex;

    [Header("DEBUGGER")]
    [ReadOnly] [SerializeField] private int itemIndex;

    private void Awake()
    {
        ItemIndex = startingItemIndex;
    }

    public void Initialize()
    {
        Destroy(gameObject);
    }
}
