using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform Balldribling;
    public float acceleration = 10.0f;
    [SerializeField] private float _maxSpeed = 5;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _gravity;
    [SerializeField] private Animator _animator;

    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode KickKey = KeyCode.LeftShift;

    private Vector3 _movementInput;
    private Vector3 _currentVelocity;


    private Rigidbody _rigidbody;
    private bool _isGrounded;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxLinearVelocity = _maxSpeed;
    }
    void FixedUpdate()
    {
        HandleMovement();
        ApplyGravity();
    }
    private void ApplyGravity()
    {
        if (!_isGrounded && _rigidbody.linearVelocity.y < 0.1f)
        {
            _rigidbody.AddForce(Vector3.down * Physics.gravity.y * _gravity, ForceMode.Impulse);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(jumpKey) && _isGrounded)
        {
            Jump();
        }
    }
    private void HandleMovement()
    {
        float horizontalInput = Input.GetKey(moveRightKey) ? 1 : (Input.GetKey(moveLeftKey) ? -1 : 0);
        float verticalInput = Input.GetKey(moveUpKey) ? 1 : (Input.GetKey(moveDownKey) ? -1 : 0);
        _movementInput = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (_movementInput != Vector3.zero)
        {
            _currentVelocity.x = Mathf.MoveTowards(_currentVelocity.x, _movementInput.x * _maxSpeed, acceleration * Time.fixedDeltaTime);
            _currentVelocity.z = Mathf.MoveTowards(_currentVelocity.z, _movementInput.z * _maxSpeed, acceleration * Time.fixedDeltaTime);

            Quaternion toRotation = Quaternion.LookRotation(_movementInput);
            _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime * 100f));
            _animator.SetBool("Idle", false);
            _animator.SetTrigger("Jump");
        }
        else
        {
            _currentVelocity.x = Mathf.MoveTowards(_currentVelocity.x, 0, acceleration * Time.fixedDeltaTime);
            _currentVelocity.z = Mathf.MoveTowards(_currentVelocity.z, 0, acceleration * Time.fixedDeltaTime);

            _animator.SetBool("Idle", true);
            _animator.SetTrigger("Jump");
        }
        _animator.ResetTrigger("Jump");
        _rigidbody.MovePosition(transform.position + _currentVelocity * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        _animator.SetTrigger("Jump");
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
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
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    public void KickAnimation()
    {
        _animator.SetTrigger("Kick");
    }
}