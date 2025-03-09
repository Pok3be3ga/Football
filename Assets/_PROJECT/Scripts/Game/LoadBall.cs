using UnityEngine;

public class LoadBall : MonoBehaviour
{
    [SerializeField] private GameObject[] _balls;
    [SerializeField] private GameSettings _gameSettings;
    void Start()
    {
        for (int i = 0; i < _balls.Length; i++)
        {
            _balls[i].gameObject.SetActive(false);
        }
        _balls[_gameSettings.BallIndex].SetActive(true);
    }
}
