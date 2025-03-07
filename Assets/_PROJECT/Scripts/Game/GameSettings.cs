using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public int[] SaveGlovesFirstPlayer = new int[9];
    public int[] SaveGlovesSecondPlayer = new int[9];

    [HideInInspector] public Dictionary<int, bool> HairPayed = new Dictionary<int, bool>();
    public bool TwoPlayer;
    public Settings GameSettingOnePlayer;
}
public enum Settings
{
    Easy,
    Normal,
    Hard,
    Empty
}
