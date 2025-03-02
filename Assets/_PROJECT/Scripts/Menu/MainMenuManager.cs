using Layer_lab._3D_Casual_Character;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private Button _startButton;
    [SerializeField] private Toggle _secondPlayerToggle;
    [SerializeField] private Button[] _saveButtons;
    [SerializeField] private GameObject[] _levelEnviroments;


    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private CharacterControl _characterControl;
    public int[] SaveGloves = new int[10];
    private void OnEnable()
    {
#if UNITY_WEBGL
        YandexGame.GetDataEvent += GetLoad;
#endif
#if UNITY_EDITOR
        GetLoad();
#endif
    }
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }
    public void GetLoad()
    {
        _moneyText.text = YandexGame.savesData.Money.ToString();
        LoadBuysFromJSON();
        if (_saveButtons.Length > 0)
        {
            for (int i = 0; i < _saveButtons.Length; i++)
            {
                _saveButtons[i].onClick.AddListener(SaveBuysInJSON);
            }
        }
    }
    void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetLoad();
        }
        AddMethodsOnButtons();
    }
    private void AddMethodsOnButtons()
    {
        _startButton.onClick.AddListener(() => { SceneManager.LoadScene("Level 1 One Player"); });
    }


    [ContextMenu("Save")]
    public void SaveBuysInJSON()
    {
        //SaveByArray(_characterControl.CharacterBase.PartsBody, 0);
        SaveByArray(_characterControl.CharacterBase.PartsHair, 1);
        SaveByArray(_characterControl.CharacterBase.PartsFace, 2);
        SaveByArray(_characterControl.CharacterBase.PartsHeadGear, 3);
        SaveByArray(_characterControl.CharacterBase.PartsTop, 4);
        SaveByArray(_characterControl.CharacterBase.PartsBottom, 5);
        SaveByArray(_characterControl.CharacterBase.PartsEyewear, 6);
        SaveByArray(_characterControl.CharacterBase.PartsBag, 7);
        SaveByArray(_characterControl.CharacterBase.PartsShoes, 8);
        SaveByArray(_characterControl.CharacterBase.PartsGlove, 9);

        YandexGame.savesData.CharacterPartsFirst = SaveGloves;
        YandexGame.SaveProgress();
    }

    [ContextMenu("Load")]
    public void LoadBuysFromJSON()
    {
        YandexGame.LoadProgress();
        SaveGloves = YandexGame.savesData.CharacterPartsFirst;
        _characterControl.CharacterBase.SetItem(PartsType.Hair, SaveGloves[1]);
        _characterControl.CharacterBase.SetItem(PartsType.Face, SaveGloves[2]);
        _characterControl.CharacterBase.SetItem(PartsType.Headgear, SaveGloves[3]);
        _characterControl.CharacterBase.SetItem(PartsType.Top, SaveGloves[4]);
        _characterControl.CharacterBase.SetItem(PartsType.Bottom, SaveGloves[5]);
        _characterControl.CharacterBase.SetItem(PartsType.Eyewear, SaveGloves[6]);
        _characterControl.CharacterBase.SetItem(PartsType.Bag, SaveGloves[7]);
        _characterControl.CharacterBase.SetItem(PartsType.Shoes, SaveGloves[8]);
        _characterControl.CharacterBase.SetItem(PartsType.Glove, SaveGloves[9]);
    }
    private void SaveByArray(List<GameObject> gameObjects, int number)
    {
        for (int j = 0; j < gameObjects.Count; j++)
        {
            if (gameObjects[j].activeSelf == true)
            {
                SaveGloves[number] = gameObjects.IndexOf(gameObjects[j]);
            }
        }
    }
}
