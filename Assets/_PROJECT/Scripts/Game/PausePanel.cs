using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _resetRound;
    [SerializeField] private Button _resetScene;
    [SerializeField] private Image _panel;


    [SerializeField] private GameManagerOnePlayer _gameManagerOnePlayer;
    void Start()
    {
        _mainMenuButton.onClick.AddListener(Menu);
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
            _panel.gameObject.SetActive(!_panel.gameObject.activeSelf);
        }
    }
}
