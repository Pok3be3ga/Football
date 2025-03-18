using Layer_lab._3D_Casual_Character;
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
    [SerializeField] private Button _buttonADV;
    [SerializeField] private Button _startButton;
    [SerializeField] private Toggle _secondPlayerToggle;
    [SerializeField] private GameObject[] _levelEnviroments;
    [SerializeField] private Button[] _levelChoseButtons;
    [SerializeField] private GameObject[] _secondPlayerObjectsActive;
    [SerializeField] private TMP_Dropdown _levelSettingsDropDown;
    [SerializeField] private TextMeshProUGUI _roomName;

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
        LoadBuysFromJSON();
        _gameSettings.Money = YandexGame.savesData.Money;
    }
    
    private void FixedUpdate()
    {
        UpdateDisplay();
    }
    void Start()
    {
        for (int i = 0; i < _secondPlayerObjectsActive.Length; i++)
        {
            _secondPlayerObjectsActive[i].SetActive(false);
        }
        for (int i = 0; i < _levelEnviroments.Length; i++)
        {
            if (_levelEnviroments[i].activeSelf == true) _currentLevel = i;
            _roomName.text = _levelEnviroments[_currentLevel].name;
        }
        AddMethodsOnButtons();
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
        _buttonADV.onClick.AddListener(ADV);
    }
    private void StartLevelClick()
    {
        SaveBuysInJSON();
        SceneManager.LoadScene(_levelEnviroments[_currentLevel].name);
    }
    private void NextLevelClick()
    {
        _levelEnviroments[_currentLevel].SetActive(false);
        if (_currentLevel < _levelEnviroments.Length - 1) _currentLevel++;
        else _currentLevel = 0;
        _levelEnviroments[_currentLevel].SetActive(true);
        _roomName.text = _levelEnviroments[_currentLevel].name;
    }
    private void LastLevelClick()
    {
        _levelEnviroments[_currentLevel].SetActive(false);
        if (_currentLevel > 0) _currentLevel--;
        else _currentLevel = _levelEnviroments.Length - 1;
        _levelEnviroments[_currentLevel].SetActive(true);
        _roomName.text = _levelEnviroments[_currentLevel].name;
    }
    private void AddSecondPlayerClick(bool toggleValue)
    {
        _levelSettingsDropDown.gameObject.SetActive(!toggleValue);
        for (int i = 0; i < _secondPlayerObjectsActive.Length; i++)
        {
            _secondPlayerObjectsActive[i].SetActive(toggleValue);
        }
    }
    public void UpdateDisplay()
    {
        _moneyText.text = _gameSettings.Money.ToString();
    }
    private void ADV()
    {
        YandexGame.RewVideoShow(0);
       
        _gameSettings.Money += 500;
        UpdateDisplay();
    }
    #region Save

    private void SaveInGameSettings()
    {
        for (int i = 0; i < _partsControlFirstPlayer.ButtonParts.Count; i++)
        {
            _gameSettings.SaveGlovesFirstPlayer[i] = _partsControlFirstPlayer.ButtonParts[i].Index;
            _gameSettings.SaveGlovesSecondPlayer[i] = _partsControlSecondPlayer.ButtonParts[i].Index;
        }
        _gameSettings.TwoPlayer = _secondPlayerToggle.isOn;
        if (_levelSettingsDropDown.gameObject.activeSelf == true)
        {
            int index = _levelSettingsDropDown.value;
            _gameSettings.GameSettingOnePlayer = (Settings)index;
        }
        else _gameSettings.GameSettingOnePlayer = Settings.Empty;
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
        YandexGame.savesData.Money = _gameSettings.Money;

        YandexGame.savesData.Hair = _gameSettings.Hair;
        YandexGame.savesData.Face = _gameSettings.Face;
        YandexGame.savesData.Headgear = _gameSettings.Headgear;
        YandexGame.savesData.Top = _gameSettings.Top;
        YandexGame.savesData.Bottom = _gameSettings.Bottom;
        YandexGame.savesData.Bag = _gameSettings.Bag;
        YandexGame.savesData.Shoes = _gameSettings.Shoes;
        YandexGame.savesData.Glove = _gameSettings.Glove;
        YandexGame.savesData.Eyewear = _gameSettings.Eyewear;

        YandexGame.SaveProgress();
    }

    [ContextMenu("Load")]
    public void LoadBuysFromJSON()
    {

        if(YandexGame.savesData.Eyewear.Count != 0)
        {
            _gameSettings.SaveGlovesFirstPlayer = YandexGame.savesData.CharacterPartsFirst;
            _gameSettings.Hair = YandexGame.savesData.Hair;
            _gameSettings.Face = YandexGame.savesData.Face;
            _gameSettings.Headgear = YandexGame.savesData.Headgear;
            _gameSettings.Top = YandexGame.savesData.Top;
            _gameSettings.Bottom = YandexGame.savesData.Bottom;
            _gameSettings.Bag = YandexGame.savesData.Bag;
            _gameSettings.Shoes = YandexGame.savesData.Shoes;
            _gameSettings.Glove = YandexGame.savesData.Glove;
            _gameSettings.Eyewear = YandexGame.savesData.Eyewear;
        }
    }
    #endregion
}
