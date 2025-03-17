using UnityEngine;

public class BallBounce : MonoBehaviour
{
    [SerializeField] private float _pushForce = 5f;
    [SerializeField] private float _maxSpeed = 10f;
    [SerializeField] private float _randomPositionRespawn = 1f;
    [SerializeField] private float _coefPushForse = 0.1f;
    [SerializeField] private float _distanceDribling = 0.5f;


    [SerializeField] private Transform _playerGate;
    [SerializeField] private Transform _enemyGate;
    [SerializeField] private Transform _respawnBall;

    [SerializeField] private AudioSource _ballAudio;
    [SerializeField] private AudioClip _ballAudioClip;

    private Rigidbody _rb;
    private Transform _ballDriblingTransform;
    private float _randomValuePlayer = 0f;
    private float _randomValueEnemy = 0f;

    private void Update()
    {
        if (transform.position.y < 10)
        {
            RespawnBall();
        }
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.maxLinearVelocity = _maxSpeed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(_ballDriblingTransform != null)
            {
                float distance = Vector3.Distance(transform.position, _ballDriblingTransform.position);
                if (distance < _distanceDribling)
                {

                    _rb.MovePosition(_ballDriblingTransform.position);
                }
            }

        }
        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    ContactPoint contact = collision.contacts[0];
        //    Vector3 pushDirection = transform.position - contact.point;
        //    pushDirection.Normalize();
        //    _rb.AddForce(pushDirection * _coefPushForse, ForceMode.VelocityChange);
        //}
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _ballDriblingTransform = collision.gameObject.GetComponentInParent<PlayerMovement>().Balldribling;
        }
        if (collision.gameObject.CompareTag("Kick"))
        {
            _ballAudio.Play();
            _ballDriblingTransform = null;
            ContactPoint contact = collision.contacts[0];
            Vector3 pushDirection = collision.transform.forward;
                //+ new Vector3(
                //Random.Range(-_randomValuePlayer, _randomValuePlayer),
                //Random.Range(-_randomValuePlayer, _randomValuePlayer),
                //Random.Range(-_randomValuePlayer, _randomValuePlayer)) - transform.position;
            pushDirection.Normalize();
            _rb.AddForce(pushDirection * _pushForce, ForceMode.VelocityChange);
        }

        if (collision.gameObject.CompareTag("EnemyKick"))
        {
            _ballAudio.Play();
            _ballDriblingTransform = null;
            ContactPoint contact = collision.contacts[0];
            Vector3 pushDirection = _playerGate.position + new Vector3(
                Random.Range(-_randomValueEnemy, _randomValueEnemy),
                Random.Range(-_randomValueEnemy, _randomValueEnemy),
                Random.Range(-_randomValueEnemy, _randomValueEnemy)) - transform.position;
            pushDirection.Normalize();
            _rb.AddForce(pushDirection * _pushForce, ForceMode.VelocityChange);
        }
    }
    public void RespawnBall()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        _rb.angularVelocity = Vector3.zero;
        _rb.linearVelocity = Vector3.zero;
        transform.position = _respawnBall.transform.position +
            new Vector3(Random.Range(-_randomPositionRespawn, _randomPositionRespawn), 0, Random.Range(-_randomPositionRespawn, _randomPositionRespawn));
        _rb.linearVelocity = Vector3.down;
    }
    public void LevelSettings(Settings settings)
    {
        if (settings == Settings.Easy)
        {
            _randomValueEnemy = 5f;
            _randomValuePlayer = 0f;
        }
        else if (settings == Settings.Normal)
        {
            _randomValueEnemy = 3f;
            _randomValuePlayer = 2f;
        }
        else if (settings == Settings.Hard)
        {
            _randomValueEnemy = 1f;
            _randomValuePlayer = 1f;
        }
        else if (settings == Settings.Empty)
        {
            _randomValueEnemy = 2f;
            _randomValuePlayer = 2f;
        }
    }

}