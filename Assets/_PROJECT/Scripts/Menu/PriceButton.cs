using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PriceButton : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    //[SerializeField] private StartButtonInteracte _startButton;
    private Button _button;
    [SerializeField] private Button _startButton;
    private int _price;
    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _price = Int32.Parse(_button.GetComponentInChildren<TextMeshProUGUI>().text);
    }
    private void OnDisable()
    {
        _startButton.interactable = true;
    }
    void Update()
    {
        if (_price <= _gameSettings.Money)
        {
            _button.interactable = true;
            _startButton.interactable = false;
        }
        else if (_price > _gameSettings.Money)
        {
            _button.interactable = false;
            _startButton.interactable = false;
        }
    }
}
