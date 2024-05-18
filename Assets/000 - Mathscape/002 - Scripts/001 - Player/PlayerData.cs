using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Mathscape/Player/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD
    }

    [field: SerializeField] public Difficulty CurrentDifficulty { get; set; }
}
