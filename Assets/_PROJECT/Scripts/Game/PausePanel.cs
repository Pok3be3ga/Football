using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button[] _mainMenuButton;
    [SerializeField] private Button _resetRound;
    [SerializeField] private Button _resetScene;
    [SerializeField] private Image _panel;


    [SerializeField] private GameManagerOnePlayer _gameManagerOnePlayer;
    void Start()
    {
        for (int i = 0; i < _mainMenuButton.Length; i++)
        {
            _mainMenuButton[i].onClick.AddListener(Menu);
        }
        _resetRound.onClick.AddListener(ResetRound);
        _resetScene.onClick.AddListener(ResetScene);
    }
    private void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
    private void ResetRound()
    {
        _gameManagerOnePlayer.StartRound();
        _panel.gameObject.SetActive(false);
    }
    private void ResetScene()
    {
        string sceneName;
        sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    private void PauseGame()
    {
        if (Time.timeScale == 1f)
        {
            _panel.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (Time.timeScale == 0f)
        {
            _panel.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }

    }
}
