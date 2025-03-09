using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public int Money;
    public int[] SaveGlovesFirstPlayer = new int[9];
    public int[] SaveGlovesSecondPlayer = new int[9];
    public int BallIndex;
    public bool TwoPlayer;
    public Settings GameSettingOnePlayer;

    public List<bool> Hair = new List<bool>();
    public List<bool> Face = new List<bool>();
    public List<bool> Headgear = new List<bool>();
    public List<bool> Top = new List<bool>();
    public List<bool> Bottom = new List<bool>();
    public List<bool> Bag = new List<bool>();
    public List<bool> Shoes = new List<bool>();
    public List<bool> Glove = new List<bool>();
    public List<bool> Eyewear = new List<bool>();
}
public enum Settings
{
    Easy,
    Normal,
    Hard,
    Empty
}
