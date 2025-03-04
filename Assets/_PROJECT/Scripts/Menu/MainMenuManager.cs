using Layer_lab._3D_Casual_Character;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private CharacterControl _characterControlFirstPlayer;
    [SerializeField] private CharacterControl _characterControlSecondPlayer;
    [SerializeField] private PartsControl _partsControlFirstPlayer;
    [SerializeField] private PartsControl _partsControlSecondPlayer;
    [SerializeField] private GameSettings _gameSettings;
    [Space]
    [Space]
    [Space]
    [Space]
    [SerializeField] private Button _startButton;
    [SerializeField] private Toggle _secondPlayerToggle;
    [SerializeField] private GameObject[] _levelEnviroments;
    [SerializeField] private Button[] _levelChoseButtons;
    [SerializeField] private GameObject[] _secondPlayerObjectsActive;

    private int _currentLevel;
    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }
    public void GetLoad()
    {
        _moneyText.text = YandexGame.savesData.Money.ToString();
        //LoadBuysFromJSON();
    }
    void Start()
    {
        AddMethodsOnButtons();
        //GetLoad();
        for (int i = 0; i < _secondPlayerObjectsActive.Length; i++)
        {
            _secondPlayerObjectsActive[i].SetActive(false);
        }
        for (int i = 0; i < _levelEnviroments.Length; i++)
        {
            if (_levelEnviroments[i].activeSelf == true) _currentLevel = i;
        }

        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
    }
    private void AddMethodsOnButtons()
    {
        _startButton.onClick.AddListener(StartLevelClick);
        _levelChoseButtons[1].onClick.AddListener(NextLevelClick);
        _levelChoseButtons[0].onClick.AddListener(LastLevelClick);
        _secondPlayerToggle.onValueChanged.AddListener(AddSecondPlayerClick);
    }
    private void StartLevelClick()
    {
        SaveInGameSettings();
        SceneManager.LoadScene(_levelEnviroments[_currentLevel].name);
    }
    private void NextLevelClick()
    {
        _levelEnviroments[_currentLevel].SetActive(false);
        if (_currentLevel < _levelEnviroments.Length - 1) _currentLevel++;
        else _currentLevel = 0;
        _levelEnviroments[_currentLevel].SetActive(true);
    }
    private void LastLevelClick()
    {
        _levelEnviroments[_currentLevel].SetActive(false);
        if (_currentLevel > 0) _currentLevel--;
        else _currentLevel = _levelEnviroments.Length - 1;
        _levelEnviroments[_currentLevel].SetActive(true);
    }
    private void AddSecondPlayerClick(bool toggleValue)
    {
        for (int i = 0; i < _secondPlayerObjectsActive.Length; i++)
        {
            _secondPlayerObjectsActive[i].SetActive(toggleValue);
        }
    }

    private void SaveInGameSettings()
    {
        for (int i = 0; i < _partsControlFirstPlayer.ButtonParts.Count; i++)
        {
            _gameSettings.SaveGlovesFirstPlayer[i] = _partsControlFirstPlayer.ButtonParts[i].Index;
            _gameSettings.SaveGlovesSecondPlayer[i] = _partsControlSecondPlayer.ButtonParts[i].Index;
        }
        _gameSettings.TwoPlayer = _secondPlayerToggle.isOn;
    }
    [ContextMenu("SaveDef")]
    public void SaveDefalth()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
    [ContextMenu("Save")]
    public void SaveBuysInJSON()
    {
        SaveInGameSettings();
        YandexGame.savesData.CharacterPartsFirst = _gameSettings.SaveGlovesFirstPlayer;
        YandexGame.SaveProgress();
    }

    [ContextMenu("Load")]
    public void LoadBuysFromJSON()
    {

    }
    private void SaveByArray(List<GameObject> gameObjects, int number, int[] gloves)
    {
        for (int j = 0; j < gameObjects.Count; j++)
        {
            if (gameObjects[j].activeSelf == true)
            {
                gloves[number] = gameObjects.IndexOf(gameObjects[j]);
            }
        }
    }
    private void SaveByArrayPlayer(CharacterControl characterControl, int[] gloves)
    {

        SaveByArray(characterControl.CharacterBase.PartsHair, 1, gloves);
        SaveByArray(characterControl.CharacterBase.PartsFace, 2, gloves);
        SaveByArray(characterControl.CharacterBase.PartsHeadGear, 3, gloves);
        SaveByArray(characterControl.CharacterBase.PartsTop, 4, gloves);
        SaveByArray(characterControl.CharacterBase.PartsBottom, 5, gloves);
        SaveByArray(characterControl.CharacterBase.PartsEyewear, 6, gloves);
        SaveByArray(characterControl.CharacterBase.PartsBag, 7, gloves);
        SaveByArray(characterControl.CharacterBase.PartsShoes, 8, gloves);
        SaveByArray(characterControl.CharacterBase.PartsGlove, 9, gloves);
    }
}
