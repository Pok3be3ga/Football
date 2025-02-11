using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // The object to chase
    public float moveSpeed = 5.0f; // Speed of the chaser
    public float rotationSpeed = 10.0f; // Rotation speed of the chaser
    public float JumpForce = 5.0f;

    private Rigidbody _rigidbody;
    private float _timerJump;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on this GameObject.");
        }
        StartCoroutine(JumpCorutine());
    }
    private void Update()
    {
        _timerJump += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            ChaseTarget();
            LookAtTarget();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enviroment"))
        {
            _timerJump = 0f;
            if (_timerJump > 1f)
            {
                StopAllCoroutines();
                Jump();
                StartCoroutine(JumpCorutine());
            }
        }
    }
    private void ChaseTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Vector3 moveDirection = new Vector3(directionToTarget.x, 0, directionToTarget.z);

        // Move towards the target
        _rigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void LookAtTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        directionToTarget.y = 0; // Ignore vertical axis for looking

        if (directionToTarget.sqrMagnitude > 0.01f) // Avoid unnecessary rotation when very close
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(newRotation);
        }
    }
    IEnumerator JumpCorutine()
    {
        float random = Random.Range(1, 6);
        yield return new WaitForSeconds(random);
        Jump();
        StartCoroutine(JumpCorutine());
    }
    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }
}