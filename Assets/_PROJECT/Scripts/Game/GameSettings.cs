using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public int[] SaveGlovesFirstPlayer = new int[10];
    public int[] SaveGlovesSecondPlayer = new int[10];

    public GameObject[] FirstPlayer = new GameObject[3];
    public GameObject[] SecondPlayer = new GameObject[3];

    public bool TwoPlayer;
}
