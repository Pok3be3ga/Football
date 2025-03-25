using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // The object to chase
    public float moveSpeed = 5.0f; // Speed of the chaser
    public float rotationSpeed = 10.0f; // Rotation speed of the chaser
    //public float JumpForce = 5.0f;
    [SerializeField] private float _gravity;
    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject _kickHead;
    [SerializeField] private GameObject _kickFoot;

    private Rigidbody _rigidbody;
    private bool _dribling = true;
    //private float _timer = 0f;
    private bool _isGrounded;
    private bool _staticState = true;
    void Start()
    {

        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody component not found on this GameObject.");
        }
        _rigidbody.maxLinearVelocity = 8f;
        StateGame();
    }
    void FixedUpdate()
    {
        if (target != null && _staticState == false)
        {
            MoveEnemy();
            LookAtTarget();
        }
        ApplyGravity();
    }
    private void ApplyGravity()
    {
        if (_rigidbody.linearVelocity.y < 0)
        {
            _rigidbody.AddForce(Vector3.down * Physics.gravity.y * _gravity, ForceMode.Acceleration);
        }
        if(_rigidbody.linearVelocity.x < 0 || _rigidbody.linearVelocity.z < 0)
        {
            _rigidbody.linearVelocity = Vector3.zero;
        }
    }
    public void OnContact()
    {
        _dribling = false;
        _animator.SetTrigger("Back");
        StopAllCoroutines();
        StartCoroutine(StopDribling());
    }

    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            float angle = Vector3.Angle(collision.contacts[i].normal, Vector3.up);
            if (angle < 45f)
            {
                _isGrounded = true;
                _animator.ResetTrigger("Jump");
                _rigidbody.linearVelocity = Vector3.zero;
            }
        }
        //if (collision.gameObject.CompareTag("Enviroment") && _isGrounded == true)
        //{
        //    if (_timer > 1f)
        //    {
        //        Jump();
        //        _timer = 0f;
        //    }
        //}
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    _isGrounded = false;
    //}
    private void MoveEnemy()
    {
        if (_dribling)
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            Vector3 moveDirection = new Vector3(directionToTarget.x, 0, directionToTarget.z);
            _rigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            Vector3 directionToTarget = (transform.position - target.position).normalized;
            Vector3 moveDirection = new Vector3(directionToTarget.x + 1f, 0, directionToTarget.z - 1f);
            _rigidbody.MovePosition(transform.position + moveDirection * moveSpeed / 4f * Time.fixedDeltaTime);
        }
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
    public void LevelSettings(Settings settings)
    {
        if (settings == Settings.Easy)
        {
            moveSpeed = 6f;
            _kickHead.SetActive(false);
        }
        else if (settings == Settings.Normal)
        {
            moveSpeed = 6.5f;
        }
        else if (settings == Settings.Hard)
        {
            moveSpeed = 7f;
            _kickHead.SetActive(true);
        }
    }
    public void KickAnimation()
    {
        _animator.SetTrigger("Kick");
    }
    private void StateDelay()
    {
        _staticState = false;
        _animator.SetBool("Idle", false);
    }
    public void StateGame()
    {
        _animator.SetBool("Idle", true);
        Invoke("StateDelay", Random.Range(1f, 2f));
    }
    public void StateWin()
    {
        _staticState = true;
        _animator.SetTrigger("Win");
        transform.rotation = Quaternion.Euler(0f, 180f, 0);
    }
    public void StateLoose()
    {
        _staticState = true;
        _animator.SetTrigger("Lose");
        transform.rotation = Quaternion.Euler(0f, 180f, 0);
    }
    private IEnumerator StopDribling()
    {
        yield return new WaitForSeconds(1);
        _dribling = true;
    }
}