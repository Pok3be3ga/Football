using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyBindingMenu : MonoBehaviour
{
    private Dictionary<string, KeyCode> _keys = new Dictionary<string, KeyCode>();
    [SerializeField] private TextMeshProUGUI up, down, left, right;
    private void Start()
    {
        _keys.Add("Up", KeyCode.W);
        _keys.Add("Down", KeyCode.S);
        _keys.Add("Left", KeyCode.A);
        _keys.Add("Right", KeyCode.D);
    }

}