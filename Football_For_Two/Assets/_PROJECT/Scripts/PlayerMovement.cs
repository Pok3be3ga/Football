using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5.0f; 
    public float RotationSpeed = 10f;
    public float JumpForce = 5.0f;
    [SerializeField] private Animator _animator;

    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode KickKey = KeyCode.LeftShift;
    public KeyCode UpKey = KeyCode.W;
    public KeyCode DownKey = KeyCode.S;
    public KeyCode LeftKey = KeyCode.A;
    public KeyCode RightKey = KeyCode.D;


    private Rigidbody _rigidbody;
    private bool _isGrounded;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(RightKey)) movement += Vector3.right;
        if (Input.GetKey(LeftKey)) movement += Vector3.left;
        if (Input.GetKey(UpKey)) movement += Vector3.forward;
        if (Input.GetKey(DownKey)) movement += Vector3.back;
        movement.Normalize();
        movement *= Speed * Time.deltaTime;

        _rigidbody.MovePosition(transform.position + movement);
        _animator.SetFloat("Speed", _rigidbody.solverVelocityIterations);
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement);
            _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.deltaTime * 100f));
        }
    }
    private void Update()
    {
        // Check for jump input
        if (Input.GetKeyDown(JumpKey) && _isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f) //&& collision.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;
                return;
            }
        }
        _isGrounded = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }
}