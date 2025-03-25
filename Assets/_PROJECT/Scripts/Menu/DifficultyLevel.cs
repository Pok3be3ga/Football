using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DifficultyLevel : MonoBehaviour
{
    [SerializeField] private Button _mainButton;
    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameSettings _gameSettings;

    private void Start()
    {
        _mainButton.onClick.AddListener(Active);
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            int idx = i;
            _levelButtons[i].onClick.AddListener(() => { DifficultyLevelChoise(idx); });
        }
    }

    private void Active()
    {
        _buttons.gameObject.SetActive(!_buttons.gameObject.activeSelf);
    }
    private void DifficultyLevelChoise(int index)
    {
        _gameSettings.GameSettingOnePlayer = (Settings)index;
        _mainButton.image.sprite = _levelButtons[index].image.sprite;
        _mainButton.GetComponentInChildren<TextMeshProUGUI>().text = _levelButtons[index].GetComponentInChildren<TextMeshProUGUI>().text;
        Active();
    }
    private void OnDisable()
    {
        _gameSettings.GameSettingOnePlayer = Settings.Empty;
    }
    private void OnEnable()
    {
        _gameSettings.GameSettingOnePlayer = Settings.Easy;
    }
}
