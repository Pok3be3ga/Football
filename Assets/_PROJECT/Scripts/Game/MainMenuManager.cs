using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Button _startButton;
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
}
