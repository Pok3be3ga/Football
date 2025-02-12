using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class GameManagerOnePlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _startText;
    [SerializeField] private TextMeshProUGUI _pointText;
    [SerializeField] private BallBounce _ball;

    [SerializeField] private PlayerMovement _player;
    [SerializeField] private EnemyMovement _enemy;

    [SerializeField] private Transform _playerRespawn;
    [SerializeField] private Transform _enemyRespawn;

    [SerializeField] private Image _pausePanel;
    [SerializeField] private TriggerGate[] _gates;


    private int _playerPoint;
    private int _enemyPoint;
    private void Start()
    {
        StartRound();
    }

    private void StartRound()
    {
        _ball.RespawnBall();
        _ball.Rigidbody.linearVelocity = Vector3.down;
        _enemy.transform.position = _enemyRespawn.transform.position;
        _player.transform.position = _playerRespawn.transform.position;
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
            yield return null;
        }
       yield return new WaitForSeconds(1);
        Time.timeScale = 1f;
        _startText.gameObject.SetActive(false);
        StartRound();
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
        if (_playerPoint == 5)
        {
            YandexGame.savesData.Money += 100;
            YandexGame.SaveProgress();
        }
    }
}
