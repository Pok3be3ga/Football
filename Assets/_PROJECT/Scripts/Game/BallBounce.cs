using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public float PushForce = 5f;
    public Rigidbody Rigidbody;
    [SerializeField] private float MaxSpeed = 10f;
    [SerializeField] private float DirationUpSpeed = 2f;

    [SerializeField] private Transform _playerGate;
    [SerializeField] private Transform _enemyGate;
    [SerializeField] private AudioSource _ballAudio;
    [SerializeField] private AudioClip _ballAudioClip;
    [SerializeField] private Transform _respawnBall;

    [SerializeField] private float _randomPosition = 1f;


    [SerializeField] private float _randomValuePlayer = 0f;
    [SerializeField] private float _randomValueEnemy = 0f;

    private void Update()
    {
        if (transform.position.y < 10)
        {
            RespawnBall();
        }
    }
    void Start()
    {
        Rigidbody.maxLinearVelocity = MaxSpeed;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 pushDirection = transform.position - contact.point;
            pushDirection.Normalize();
            Rigidbody.AddForce(pushDirection * 0.2f, ForceMode.VelocityChange);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        _ballAudio.PlayOneShot(_ballAudioClip);
        if (collision.gameObject.CompareTag("Kick"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 pushDirection = _enemyGate.position + new Vector3(
                Random.Range(0, _randomValuePlayer),
                Random.Range(0, _randomValuePlayer),
                Random.Range(0, _randomValuePlayer)) - transform.position;
            pushDirection.Normalize();
            Rigidbody.AddForce(pushDirection * PushForce, ForceMode.VelocityChange);
        }

        if (collision.gameObject.CompareTag("EnemyKick"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 pushDirection = _playerGate.position + new Vector3(
                Random.Range(0, _randomValueEnemy),
                Random.Range(0, _randomValueEnemy),
                Random.Range(0, _randomValueEnemy)) - transform.position;
            pushDirection.Normalize();
            Rigidbody.AddForce(pushDirection * PushForce, ForceMode.VelocityChange);
        }
    }
    public void RespawnBall()
    {
        Rigidbody.angularVelocity = Vector3.zero;
        Rigidbody.linearVelocity = Vector3.zero;
        transform.position = _respawnBall.transform.position +
            new Vector3(Random.Range(-_randomPosition, _randomPosition), 0, Random.Range(-_randomPosition, _randomPosition));
    }
    public void LevelSettings(Settings settings)
    {
        if (settings == Settings.Easy)
        {
            _randomValueEnemy = 3f;
            _randomValuePlayer = 0f;
        }
        else if (settings == Settings.Normal)
        {
            _randomValueEnemy = 2f;
            _randomValuePlayer = 2f;
        }
        else if (settings == Settings.Hard)
        {
            _randomValueEnemy = 1f;
            _randomValuePlayer = 2f;
        }
        else if (settings == Settings.Empty)
        {
            _randomValueEnemy = 2f;
            _randomValuePlayer = 2f;
        }
    }

}