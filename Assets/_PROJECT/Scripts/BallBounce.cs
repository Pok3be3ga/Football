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
            StartCoroutine(UpSpeed());
            Rigidbody.AddForce(pushDirection * PushForce, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("EnemyKick"))
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 pushDirection = _playerGate.position - transform.position;
            pushDirection.Normalize();
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
}