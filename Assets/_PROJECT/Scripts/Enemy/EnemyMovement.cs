using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // The object to chase
    public float moveSpeed = 5.0f; // Speed of the chaser
    public float rotationSpeed = 10.0f; // Rotation speed of the chaser
    public float JumpForce = 5.0f;
    [SerializeField] private float _gravity;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _kickHead;
    [SerializeField] private GameObject _kickFoot;

    private Rigidbody _rigidbody;
    private float _timerJump = 0f;
    private bool _isGrounded;
    private void OnEnable()
    {
        if (_rigidbody != null)
            _rigidbody.linearVelocity = Vector3.zero;
    }
    void Start()
    {

        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on this GameObject.");
        }
        _rigidbody.maxLinearVelocity = 8f;
        _animator.SetBool("Idle", false);
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
        ApplyGravity();
    }
    private void ApplyGravity()
    {
        if (!_isGrounded && _rigidbody.linearVelocity.y < 0)
        {
            _rigidbody.AddForce(Vector3.down * Physics.gravity.y * _gravity, ForceMode.Acceleration);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            float angle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);
            if (angle < 45f)
            {
                _isGrounded = true;
            }
        }
        if (collision.gameObject.CompareTag("Enviroment") && _isGrounded == true)
        {
            if (_timerJump > 1f)
            {
                Jump();
                _timerJump = 0f;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }
    private void ChaseTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Vector3 moveDirection = new Vector3(directionToTarget.x, 0, directionToTarget.z);
        _rigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
    private void LookAtTarget()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        directionToTarget.y = 0;
        if (directionToTarget.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(newRotation);
        }
    }
    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
    }

    public void LevelSettings(Settings settings)
    {
        if (settings == Settings.Easy)
        {
            moveSpeed = 4.5f;
            _kickHead.SetActive(false);
        }
        else if (settings == Settings.Normal)
        {
            moveSpeed = 5f;
        }
        else if (settings == Settings.Hard)
        {
            moveSpeed = 5.5f;
            _kickHead.SetActive(true);
        }
    }
}