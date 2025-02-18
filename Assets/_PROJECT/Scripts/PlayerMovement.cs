using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration = 10.0f; // ��������� ���������
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float Speed = 5.0f;
    [SerializeField] private float RotationSpeed = 10f;
    [SerializeField] private float JumpForce = 5.0f;
    [SerializeField] private float _friction;
    [SerializeField] private Animator _animator;

    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.Space;

    //public KeyCode JumpKey = KeyCode.Space;
    public KeyCode KickKey = KeyCode.LeftShift;
    //public KeyCode UpKey = KeyCode.W;
    //public KeyCode DownKey = KeyCode.S;
    //public KeyCode LeftKey = KeyCode.A;
    //public KeyCode RightKey = KeyCode.D;
    private Vector3 _movementInput;
    private Vector3 _currentVelocity;


    private Rigidbody _rigidbody;
    private bool _isGrounded;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxLinearVelocity = maxSpeed;
    }
    void FixedUpdate()
    {
        //Vector3 movement = Vector3.zero;
        HandleMovement();
        ApplyGravity();
        //if (Input.GetKey(RightKey)) movement += Vector3.Lerp(Vector3.zero, Vector3.right, Time.deltaTime);
        //if (Input.GetKey(LeftKey)) movement += Vector3.Lerp(Vector3.zero, Vector3.left, Time.deltaTime);
        //if (Input.GetKey(UpKey)) movement += Vector3.Lerp(Vector3.zero, Vector3.forward, Time.deltaTime);
        //if (Input.GetKey(DownKey)) movement += Vector3.Lerp(Vector3.zero, Vector3.back, Time.deltaTime);

        //if (Input.GetKey(RightKey)) movement += Vector3.right;
        //if (Input.GetKey(LeftKey)) movement += Vector3.left;
        //if (Input.GetKey(UpKey)) movement += Vector3.forward;
        //if (Input.GetKey(DownKey)) movement += Vector3.back;
        //_rigidbody.MovePosition(transform.position + movement * Speed);

        //_rigidbody.AddForce(movement * Speed, ForceMode.VelocityChange);
        //if (!_isGrounded) _rigidbody.AddForce(Vector3.down * _maxSpeed);
        //_rigidbody.AddForce(new Vector3(-movement.x, 0f, -movement.z) * _friction, ForceMode.VelocityChange);



        //if (movement != Vector3.zero)
        //{
        //    _animator.SetBool("Idle", false);

        //}
        //else
        //{
        //    _animator.SetBool("Idle", true);
        //}
    }
    private void ApplyGravity()
    {
        if (!_isGrounded && _rigidbody.linearVelocity.y < 0)
        {
            _rigidbody.AddForce(Vector3.down * Physics.gravity.y * _friction, ForceMode.Acceleration);
        }
    }
    private void Update()
    {
        // Check for jump input
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
            _currentVelocity.x = Mathf.MoveTowards(_currentVelocity.x, _movementInput.x * maxSpeed, acceleration * Time.fixedDeltaTime);
            _currentVelocity.z = Mathf.MoveTowards(_currentVelocity.z, _movementInput.z * maxSpeed, acceleration * Time.fixedDeltaTime);

            Quaternion toRotation = Quaternion.LookRotation(_movementInput);
            _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.deltaTime * 100f));
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
        _rigidbody.MovePosition(transform.position + _currentVelocity * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        _animator.SetTrigger("Jump");
        _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
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
        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f) //&& collision.gameObject.CompareTag("Ground"))
        //    {
        //        _isGrounded = true;
        //        return;
        //    }
        //}
        //_isGrounded = false;
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