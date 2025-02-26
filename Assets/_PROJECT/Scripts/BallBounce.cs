using System.Collections;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public float PushForce = 5f;
    public Rigidbody Rigidbody;
    [SerializeField] private float BallSpeed = 4f;
    [SerializeField] private float MaxSpeed = 10f;
    [SerializeField] private float DirationUpSpeed = 2f;

    [SerializeField] private Transform _playerGate;
    [SerializeField] private AudioSource _ballAudio;
    [SerializeField] private AudioClip _ballAudioClip;
    [SerializeField] private Transform _respawnBall;

    [SerializeField] private float _randomPosition = 1f;
    [SerializeField] private float _randomValue = 0f;

    private void Update()
    {
        if(transform.position.y < 10)
        {
            RespawnBall();
        }
    }
    void Start()
    {
        Rigidbody.maxLinearVelocity = BallSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        _ballAudio.PlayOneShot(_ballAudioClip);
        if (collision.gameObject.CompareTag("Kick"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 pushDirection = contact.point - transform.position;
            pushDirection.Normalize();
            StopAllCoroutines();
            Rigidbody.maxLinearVelocity = BallSpeed;
            StartCoroutine(UpSpeed());
            Rigidbody.AddForce(pushDirection * PushForce, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("EnemyKick"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 pushDirection = _playerGate.position + new Vector3(
                Random.Range(0, _randomValue), 
                Random.Range(0, _randomValue), 
                Random.Range(0, _randomValue)) - transform.position;
            pushDirection.Normalize();
            StopAllCoroutines();
            Rigidbody.maxLinearVelocity = BallSpeed;
            StartCoroutine(UpSpeed());
            Rigidbody.AddForce(pushDirection * PushForce * 0.2f, ForceMode.Impulse);
        }
    }
    IEnumerator UpSpeed()
    {
        Rigidbody.maxLinearVelocity = MaxSpeed;
        yield return new WaitForSeconds(DirationUpSpeed);

        float counter = 0;
        while (counter < DirationUpSpeed)
        {
            counter += Time.deltaTime;
            Rigidbody.maxLinearVelocity = Mathf.Lerp(MaxSpeed, BallSpeed, counter / DirationUpSpeed);
            yield return null;
        }
    }


    public void RespawnBall()
    {
        transform.position = _respawnBall.transform.position + 
            new Vector3(Random.Range(-_randomPosition, _randomPosition), 0, Random.Range(-_randomPosition, _randomPosition));
    }

}