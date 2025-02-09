using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Button _restart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _restart.onClick.AddListener(RestartScene);
    }
    private void RestartScene()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1.0f;
    }
}
