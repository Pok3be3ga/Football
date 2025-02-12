using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    void Start()
    {
        _mainMenuButton.onClick.AddListener(Menu);
    }
    private void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }
}
