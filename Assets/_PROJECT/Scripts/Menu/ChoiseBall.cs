using UnityEngine;
using UnityEngine.UI;

public class ChoiseBall : MonoBehaviour
{
    [SerializeField] private GameObject[] _balls;
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameSettings _gameSettings;
    private int _currentBall;
    private void Start()
    {
        buttons[0].onClick.AddListener(LastLevelClick);
        buttons[1].onClick.AddListener(NextLevelClick);
    }

    private void NextLevelClick()
    {
        _balls[_currentBall].SetActive(false);
        if (_currentBall < _balls.Length - 1) _currentBall++;
        else _currentBall = 0;
        _balls[_currentBall].SetActive(true);
        _gameSettings.BallIndex = _currentBall;
    }
    private void LastLevelClick()
    {
        _balls[_currentBall].SetActive(false);
        if (_currentBall > 0) _currentBall--;
        else _currentBall = _balls.Length - 1;
        _balls[_currentBall].SetActive(true);
        _gameSettings.BallIndex = _currentBall;
    }
}
