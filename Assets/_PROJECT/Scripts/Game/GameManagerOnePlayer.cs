using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameManagerOnePlayer : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private TextMeshProUGUI _startText;
    [SerializeField] private TextMeshProUGUI _pointText;
    [SerializeField] private BallBounce _ball;

    [SerializeField] private PlayerMovement[] _players;
    [SerializeField] private EnemyMovement _enemy;

    [SerializeField] private Transform _playerRespawn;
    [SerializeField] private Transform _enemyRespawn;

    [SerializeField] private Image _pausePanel;
    [SerializeField] private TriggerGate[] _gates;

    private bool _twoPlayer;


    private int _playerPoint;
    private int _enemyPoint;
    private void Awake()
    {
        _twoPlayer = _gameSettings.TwoPlayer;
        if (_twoPlayer)
        {
            _enemy.gameObject.SetActive(false);
            _players[1].gameObject.SetActive(true);
        }
        else
        {
            _enemy.gameObject.SetActive(true);
            _players[1].gameObject.SetActive(false);
        }
        StartRound();
    }

    public void StartRound()
    {
        _ball.RespawnBall();
        _ball.Rigidbody.linearVelocity = Vector3.down;

        if (_twoPlayer)
        {
            _players[1].transform.position = _enemyRespawn.position;
            _players[1].transform.rotation = _enemyRespawn.rotation;
        }
        else
        {
            _enemy.enabled = false;
            _enemy.transform.position = _enemyRespawn.position;
            _enemy.transform.rotation = _enemyRespawn.rotation;
            _enemy.enabled = true;
        }
        _players[0].transform.position = _playerRespawn.position;
        _players[0].transform.rotation = _playerRespawn.rotation;
    }
    public void Goal(bool _playerGate)
    {
        StartCoroutine(TimeScale());
        if (_playerGate) _enemyPoint++;
        else _playerPoint++;
        UpdateDisplay();

        //Победа
        if (_enemyPoint == 5 || _playerPoint == 5)
        {
            WinGame();
        }
    }
    private IEnumerator TimeScale()
    {
        while (Time.timeScale >= 0.5f)
        {
            Time.timeScale -= Time.deltaTime;
        }
       yield return new WaitForSeconds(1);
        Time.timeScale = 1f;
        _startText.gameObject.SetActive(false);
        StartRound();
        yield return null;
    }
    private void WinGame()
    {
        StopAllCoroutines();
        Time.timeScale = 0.5f;
        _pausePanel.gameObject.SetActive(true);
        _enemy.enabled = false;
        AddMoney();
    }
    private void UpdateDisplay()
    {
        _startText.gameObject.SetActive(true);
        _startText.text = "GOAL";
        _pointText.text = _playerPoint.ToString() + ":" + _enemyPoint.ToString();
    }
    [ContextMenu("AddMoney")]
    private void AddMoney()
    {
        if (_playerPoint == 5 && _twoPlayer == false)
        {
            YandexGame.savesData.Money += 100;
            YandexGame.SaveProgress();
        }
    }
}
