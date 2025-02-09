using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerOne : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _startText;
    [SerializeField] private TextMeshProUGUI _pointText;
    [SerializeField] private BallBounce _ball;
    [SerializeField] private Transform _respawnBall;
    [SerializeField] private float _randomPosition = 1f;

    [SerializeField] private PlayerMovement _player;
    [SerializeField] private EnemyMovement _enemy;

    [SerializeField] private Transform _playerRespawn;
    [SerializeField] private Transform _enemyRespawn;

    [SerializeField] private Image _pausePanel;


    private int _playerPoint;
    private int _enemyPoint;
    private void Start()
    {
        StartRound();
    }

    private void StartRound()
    {
        _ball.transform.position = _respawnBall.transform.position + 
            new Vector3(Random.Range(-_randomPosition, _randomPosition), 0, Random.Range(-_randomPosition, _randomPosition));
        _ball.Rigidbody.linearVelocity = Vector3.down;
        _enemy.transform.position = _enemyRespawn.transform.position;
        _player.transform.position = _playerRespawn.transform.position;
    }
    public void Goal(bool _playerGate)
    {
        _startText.gameObject.SetActive(true);
        _startText.text = "GOAL";
        StartCoroutine(Goal());
        if (_playerGate) _enemyPoint++;
        else _playerPoint++;
        if(_enemyPoint == 5 || _playerPoint == 5)
        {
            StopAllCoroutines();
            Time.timeScale = 0.5f;
            _pausePanel.gameObject.SetActive(true);
            _enemy.enabled = false;
        }
    }
    IEnumerator Goal()
    {
        while(Time.timeScale >= 0.5f)
        {
            Time.timeScale -= Time.deltaTime;
            yield return null;
        }
       yield return new WaitForSeconds(1);
        _pointText.text = _playerPoint.ToString() + ":" + _enemyPoint.ToString();
        Time.timeScale = 1f;
        _startText.gameObject.SetActive(false);
        StartRound();
    }
}
