using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartButtonInteracte : MonoBehaviour
{
    private List<Button> _buttons = new List<Button>();
    private Button _button;
    private void Start()
    {
        _button = GetComponent<Button>();
    }
    public void InteracteButton(bool OnOff)
    {
        _button.interactable = OnOff;
    }
    public void AddInList(Button button)
    {
        _buttons.Add(button);
    }
    public void RemoveFromList(Button button)
    {
        _buttons.Remove(button);
    }
}
