using Layer_lab._3D_Casual_Character;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    [SerializeField] private Button _startButton;
    [SerializeField] private Toggle _secondPlayerToggle;
    [SerializeField] private GameObject[] _levelEnviroments;
    [SerializeField] private Button[] _levelChoseButtons;
    [SerializeField] private GameObject[] _secondPlayerObjectsActive;


    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private CharacterControl _characterControlFirstPlayer;
    [SerializeField] private CharacterControl _characterControlSecondPlayer;

    private int[] _saveGlovesFirstPlayer = new int[10];
    private int[] _saveGlovesSecondPlayer = new int[10];
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
        _startButton.onClick.AddListener(StartLevel);
        _levelChoseButtons[1].onClick.AddListener(NextLevel);
        _levelChoseButtons[0].onClick.AddListener(LastLevel);
        _secondPlayerToggle.onValueChanged.AddListener(AddSecondPlayer);
    }
    private void StartLevel()
    {
        SaveInGameSettings();
        SceneManager.LoadScene(_levelEnviroments[_currentLevel].name);
    }
    private void NextLevel()
    {
        _levelEnviroments[_currentLevel].SetActive(false);
        if (_currentLevel < _levelEnviroments.Length - 1) _currentLevel++;
        else _currentLevel = 0;
        _levelEnviroments[_currentLevel].SetActive(true);
    }
    private void LastLevel()
    {
        _levelEnviroments[_currentLevel].SetActive(false);
        if (_currentLevel > 0) _currentLevel--;
        else _currentLevel = _levelEnviroments.Length - 1;
        _levelEnviroments[_currentLevel].SetActive(true);
    }
    private void AddSecondPlayer(bool toggleValue)
    {
        for (int i = 0; i < _secondPlayerObjectsActive.Length; i++)
        {
            _secondPlayerObjectsActive[i].SetActive(toggleValue);
        }
    }


    private void SaveInGameSettings()
    {
        SaveByArrayPlayer(_characterControlFirstPlayer, _saveGlovesFirstPlayer);
        SaveByArrayPlayer(_characterControlSecondPlayer, _saveGlovesSecondPlayer);
        _gameSettings.SaveGlovesFirstPlayer = _saveGlovesFirstPlayer;
        _gameSettings.SaveGlovesSecondPlayer = _saveGlovesSecondPlayer;
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
        SaveByArrayPlayer(_characterControlFirstPlayer, _saveGlovesFirstPlayer);

        YandexGame.savesData.CharacterPartsFirst = _saveGlovesFirstPlayer;
        YandexGame.SaveProgress();
    }

    [ContextMenu("Load")]
    public void LoadBuysFromJSON()
    {
        YandexGame.LoadProgress();
        _saveGlovesFirstPlayer = YandexGame.savesData.CharacterPartsFirst;
        _characterControlFirstPlayer.CharacterBase.SetItem(PartsType.Hair, _saveGlovesFirstPlayer[1]);
        _characterControlFirstPlayer.CharacterBase.SetItem(PartsType.Face, _saveGlovesFirstPlayer[2]);
        _characterControlFirstPlayer.CharacterBase.SetItem(PartsType.Headgear, _saveGlovesFirstPlayer[3]);
        _characterControlFirstPlayer.CharacterBase.SetItem(PartsType.Top, _saveGlovesFirstPlayer[4]);
        _characterControlFirstPlayer.CharacterBase.SetItem(PartsType.Bottom, _saveGlovesFirstPlayer[5]);
        _characterControlFirstPlayer.CharacterBase.SetItem(PartsType.Eyewear, _saveGlovesFirstPlayer[6]);
        _characterControlFirstPlayer.CharacterBase.SetItem(PartsType.Bag, _saveGlovesFirstPlayer[7]);
        _characterControlFirstPlayer.CharacterBase.SetItem(PartsType.Shoes, _saveGlovesFirstPlayer[8]);
        _characterControlFirstPlayer.CharacterBase.SetItem(PartsType.Glove, _saveGlovesFirstPlayer[9]);
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
